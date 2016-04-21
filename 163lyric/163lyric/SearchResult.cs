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
        private List<MusicItem> mItems  = new List<MusicItem>();
        private List<ListViewItem> Items = new List<ListViewItem>();
        public string resultID="";
        public string mId = "";

        public FormSearchResult()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );
        }

        private void FormSearchResult_Load( object sender, EventArgs e )
        {
            NetEaseMusic nMusic = new NetEaseMusic();

            UseWaitCursor = true;
            Cursor = Cursors.WaitCursor;
            foreach( MusicItem item in nMusic.getMusicByTitle( mId ) )
            {
                mItems.Add( item );
            }
            Cursor = Cursors.Default;
            UseWaitCursor = false;

            lvResult.VirtualListSize = mItems.Count;
            foreach ( MusicItem mItem in mItems )
            {
                string[] item = { mItem.id, mItem.name, mItem.artist, mItem.album, mItem.picture, mItem.cover, mItem.company };
                Items.Add( new ListViewItem( item ) );
            }

            lblResultState.Text = $"Total {nMusic.ResultTotal} results. Current display {mItems.Count} results.";

        }

        private void lvResult_RetrieveVirtualItem( object sender, RetrieveVirtualItemEventArgs e )
        {
            //check to see if the requested item is currently in the cache
            if ( e.ItemIndex >= 0 && e.ItemIndex < mItems.Count )
            {
                //MusicItem mItem = mItems[e.ItemIndex];
                //string[] item = { mItem.id, mItem.name, mItem.artist, mItem.album, mItem.picture, mItem.cover, mItem.company };
                //e.Item = new ListViewItem( item );
                e.Item = Items[e.ItemIndex];
            }
        }

        private void btnOk_Click( object sender, EventArgs e )
        {
            btnOk.DialogResult = DialogResult.Cancel;
            if ( lvResult.SelectedIndices.Count>0)
            {
                btnOk.DialogResult =  DialogResult.OK;
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
    }
}
