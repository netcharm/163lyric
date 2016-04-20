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
        public MusicItem[] mItems = { };
        private List<ListViewItem> Items = new List<ListViewItem>();
        public string resultID="";

        public FormSearchResult()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );
        }

        private void FormSearchResult_Load( object sender, EventArgs e )
        {
            lvResult.VirtualListSize = mItems.Length;
            foreach ( MusicItem mItem in mItems )
            {
                string[] item = { mItem.id, mItem.name, mItem.artist, mItem.album, mItem.picture, mItem.cover, mItem.company };
                Items.Add( new ListViewItem( item ) );
                //lvResult.Items.Add( mItem.id, mItem.name );
            }

            lblResultState.Text = $"Total {mItems.Length} Results.";

        }

        private void lvResult_RetrieveVirtualItem( object sender, RetrieveVirtualItemEventArgs e )
        {
            //check to see if the requested item is currently in the cache
            if ( mItems.Length >= 0 && e.ItemIndex >= 0 && e.ItemIndex < mItems.Length )
            {
                e.Item = Items[e.ItemIndex];
            }
        }

        private void btnOk_Click( object sender, EventArgs e )
        {
            if (lvResult.SelectedIndices.Count>0)
            {
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
