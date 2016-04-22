using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using _163music;

namespace _163lyric
{
    public partial class FormSearchResult : Form
    {
        private NetEaseMusic nMusic = new NetEaseMusic();
        private List<MusicItem> mItems  = new List<MusicItem>();
        private List<ListViewItem> Items = new List<ListViewItem>();
        private Dictionary<string, int> colWidth = new Dictionary<string, int>();

        public string resultID="";
        public string mId = "";

        private void queryMusics( string query )
        {
            //NetEaseMusic nMusic = new NetEaseMusic();

            int mCount = 0;
            UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;
            do
            {
                MusicItem[] mResult = nMusic.getMusicByTitle( mId, mCount );
                mCount += mResult.Length;
                foreach ( MusicItem item in mResult )
                {
                    mItems.Add( item );
                }
            } while ( mCount < nMusic.ResultTotal && nMusic.ResultCount > 0 && mCount < 100 );

            Cursor = Cursors.Default;
            UseWaitCursor = false;

            int max_id = 2, max_title = 5, max_artist = 6, max_album = 5, max_company = 7;
            lvResult.VirtualListSize = mItems.Count;
            int itemNo = 0;
            foreach ( MusicItem mItem in mItems )
            {
                itemNo++;

                string title = string.IsNullOrEmpty(mItem.title_alias) ? mItem.title : $"{mItem.title} [{mItem.title_alias}]";
                string album = string.IsNullOrEmpty(mItem.album_alias) ? mItem.album : $"{mItem.album} [{mItem.album_alias}]";

                string[] item = { itemNo.ToString(), mItem.id, title, mItem.artist, album, mItem.company, mItem.cover, mItem.picture };
                Items.Add( new ListViewItem( item ) );

                if ( mItem.id.Length > max_id ) max_id = mItem.id.Length;
                if ( mItem.title.Length > max_title ) max_title = mItem.title.Length;
                if ( mItem.artist.Length > max_artist ) max_artist = mItem.artist.Length;
                if ( mItem.album.Length > max_album ) max_album = mItem.album.Length;
                if ( mItem.company.Length > max_company ) max_company = mItem.company.Length;
            }

            if ( mItems.Count > 0 )
            {
                string[] colAuto = { "#", "ID", "Title", "Artist", "Album", "Publisher" };
                foreach ( ColumnHeader col in lvResult.Columns )
                {
                    if ( colAuto.Contains( col.Text ) )
                    {
                        //if( colWidth[col.Text] > col.in)
                        col.Width = -1;
                        col.AutoResize( ColumnHeaderAutoResizeStyle.ColumnContent );
                    }
                }
            }

            lblResultState.Text = $"Total {nMusic.ResultTotal} results. Current display {mItems.Count} results.";
        }

        public FormSearchResult()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );
        }

        private void FormSearchResult_Load( object sender, EventArgs e )
        {
            lvResult.Items.Clear();
            colWidth.Clear();
            mItems.Clear();

            foreach ( ColumnHeader col in lvResult.Columns )
            {
                col.Width = -2;
                col.AutoResize( ColumnHeaderAutoResizeStyle.HeaderSize );
                colWidth.Add( col.Text, col.Text.Length );
            }

            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            queryWorker.RunWorkerAsync();
            //queryMusics( mId );
        }

        private void lvResult_RetrieveVirtualItem( object sender, RetrieveVirtualItemEventArgs e )
        {
            //check to see if the requested item is currently in the cache
            if ( e.ItemIndex >= 0 && e.ItemIndex < mItems.Count && e.ItemIndex < Items.Count && Items.Count >0 )
            {
                e.Item = Items[e.ItemIndex];
                e.Item.BackColor = ( e.ItemIndex % 2 == 1 ) ? Color.AliceBlue : e.Item.BackColor;
            }
        }

        private void btnOk_Click( object sender, EventArgs e )
        {
            DialogResult = DialogResult.Cancel;
            if ( lvResult.SelectedIndices.Count > 0 )
            {
                DialogResult = DialogResult.OK;
                resultID = mItems[lvResult.SelectedIndices[0]].id;
            }
        }

        private void lvResult_MouseDoubleClick( object sender, MouseEventArgs e )
        {
            if ( lvResult.SelectedIndices.Count > 0 )
            {
                btnOk.PerformClick();
            }
        }

        private void queryWorker_DoWork( object sender, DoWorkEventArgs e )
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            int mCount = 0;
            do
            {
                MusicItem[] mResult = nMusic.getMusicByTitle( mId, mCount );
                mCount += mResult.Length;
                foreach ( MusicItem item in mResult )
                {
                    mItems.Add( item );
                }
                worker.ReportProgress( mItems.Count );
            } while ( mCount < nMusic.ResultTotal && nMusic.ResultCount > 0 && mCount < 100 );
        }

        private void queryWorker_ProgressChanged( object sender, ProgressChangedEventArgs e )
        {
            if ( nMusic.ResultTotal < progressBar.Maximum )
            {
                progressBar.Maximum = nMusic.ResultTotal;
            }
            progressBar.Value = mItems.Count;
            lvResult.BeginUpdate();
            lvResult.VirtualListSize = mItems.Count;
            lvResult.Update();
            lvResult.EndUpdate();
            lblResultState.Text = $"Total {nMusic.ResultTotal} results. Current display {mItems.Count} results.";
        }

        private void queryWorker_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
        {
            int max_id = 2, max_title = 5, max_artist = 6, max_album = 5, max_company = 7;
            lvResult.VirtualListSize = mItems.Count;
            int itemNo = 0;
            foreach ( MusicItem mItem in mItems )
            {
                itemNo++;

                string title = string.IsNullOrEmpty(mItem.title_alias) ? mItem.title : $"{mItem.title} [{mItem.title_alias}]";
                string album = string.IsNullOrEmpty(mItem.album_alias) ? mItem.album : $"{mItem.album} [{mItem.album_alias}]";

                string[] item = { itemNo.ToString(), mItem.id, title, mItem.artist, album, mItem.company, mItem.cover, mItem.picture };
                Items.Add( new ListViewItem( item ) );

                if ( mItem.id.Length > max_id ) max_id = mItem.id.Length;
                if ( mItem.title.Length > max_title ) max_title = mItem.title.Length;
                if ( mItem.artist.Length > max_artist ) max_artist = mItem.artist.Length;
                if ( mItem.album.Length > max_album ) max_album = mItem.album.Length;
                if ( mItem.company.Length > max_company ) max_company = mItem.company.Length;
            }

            lvResult.BeginUpdate();
            if ( mItems.Count > 0 )
            {
                string[] colAuto = { "#", "ID", "Title", "Artist", "Album", "Publisher" };
                foreach ( ColumnHeader col in lvResult.Columns )
                {
                    if ( colAuto.Contains( col.Text ) )
                    {
                        //if( colWidth[col.Text] > col.in)
                        col.Width = -1;
                        col.AutoResize( ColumnHeaderAutoResizeStyle.ColumnContent );
                    }
                }
            }

            lvResult.EndUpdate();
            progressBar.Value = progressBar.Maximum;
            lblResultState.Text = $"Total {nMusic.ResultTotal} results. Current display {mItems.Count} results.";
        }

        private void FormSearchResult_FormClosing( object sender, FormClosingEventArgs e )
        {
            if ( queryWorker.WorkerSupportsCancellation == true )
            {
                // Cancel the asynchronous operation.
                queryWorker.CancelAsync();
            }
        }
    }
}
