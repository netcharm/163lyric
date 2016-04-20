using System;
using System.Drawing;
using System.Windows.Forms;
using _163lyric;

namespace _163music
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );
        }

        private void fetchLyric( string id )
        {
            NetEaseMusic lyric = new NetEaseMusic();
            try
            {
                int musicID = Convert.ToInt32( id );
                string[] sDetail = lyric.getDetail( musicID );

                lblTitle.Text = "";
                edLyric.Text = "";

                if ( sDetail.Length <= 0 )
                {
                    MessageBox.Show( "Music Detail Infomation Not Found！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }

                lblTitle.Text = $"{sDetail[0]} / {sDetail[1]} / {sDetail[2]} / {sDetail[3]}";

                string[] mLrc =  { };
                if ( cbLrcMultiLang.Checked )
                {
                    mLrc = lyric.getLyricMultiLang( musicID );
                }
                else
                {
                    mLrc = lyric.getLyric( Convert.ToInt32( edID.Text.Trim() ) );

                }
                if ( mLrc.Length == 1 && mLrc[0].Length < 40 )
                {
                    MessageBox.Show( string.Join( ",", mLrc ), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }
                foreach ( string lrc in mLrc )
                {
                    edLyric.Text += $"[ti:{sDetail[0]}]" + Environment.NewLine;
                    edLyric.Text += $"[ar:{sDetail[2]}]" + Environment.NewLine;
                    edLyric.Text += $"[al:{sDetail[3]}]" + Environment.NewLine;
                    if ( sDetail[1].Length > 0 )
                    {
                        edLyric.Text += $"[alias:{sDetail[1]}]" + Environment.NewLine;
                    }
                    edLyric.Text += lrc;
                    edLyric.Text += Environment.NewLine + Environment.NewLine;
                }
            }
            catch ( FormatException )
            {
                MessageBox.Show( "请输入正确的数字或歌曲名称！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void btnGet_Click( object sender, EventArgs e )
        {
            NetEaseMusic lyric = new NetEaseMusic();
            int n;
            bool isNumeric = Int32.TryParse(edID.Text.Trim(), out n);
            if ( isNumeric )
            {
                UseWaitCursor = true;
                fetchLyric( edID.Text.Trim() );
                UseWaitCursor = false;
            }
            else
            {
                UseWaitCursor = true;
                MusicItem[] musics = lyric.getMusicByTitle(  edID.Text.Trim() );
                UseWaitCursor = false;

                FormSearchResult form = new FormSearchResult();
                form.mItems = musics;
                if ( form.ShowDialog() == DialogResult.OK )
                {
                    UseWaitCursor = true;
                    fetchLyric( form.resultID );
                    UseWaitCursor = false;
                }
                form.Close();
                form.Dispose();
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            string content = "";
            if(edLyric.SelectionLength <= 0)
            {
                content = edLyric.Text.Replace( Environment.NewLine, "\r" );
            }
            else
            {
                content = edLyric.SelectedText.Replace( Environment.NewLine, "\r" );
            }
            //Clipboard.SetDataObject(content);
            Clipboard.SetText( content, TextDataFormat.UnicodeText );
        }

        private void edID_KeyPress( object sender, KeyPressEventArgs e )
        {
            if(e.KeyChar == (char) Keys.Return )
            {
                btnGet.PerformClick();
            }
        }

        private void edLyric_KeyUp( object sender, KeyEventArgs e )
        {
            if ( e.Control && e.KeyCode == Keys.C )
            {
                btnCopy.PerformClick();
            }
        }
    }
}
