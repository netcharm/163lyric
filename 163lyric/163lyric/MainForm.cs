using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using _163lyric;

namespace _163music
{
    public partial class MainForm : Form
    {
        private string lyricTitle = "";
        private string lyricAlias = "";
        private string lyricArtist = "";
        private string lyricAlbum = "";

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

                lyricTitle = sDetail[0];
                lyricAlias = sDetail[1];
                lyricArtist = sDetail[2];
                lyricAlbum = sDetail[3];

                lblTitle.Text = $"{lyricTitle} / {lyricAlias} / {lyricArtist} / {lyricAlias}";

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
                    edLyric.Text += $"[ti:{lyricTitle}]" + Environment.NewLine;
                    edLyric.Text += $"[ar:{lyricArtist}]" + Environment.NewLine;
                    edLyric.Text += $"[al:{lyricAlbum}]" + Environment.NewLine;
                    if ( lyricAlias.Length > 0 )
                    {
                        edLyric.Text += $"[alias:{lyricAlias}]" + Environment.NewLine;
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
                Cursor = Cursors.WaitCursor;
                fetchLyric( edID.Text.Trim() );
                Cursor = Cursors.Default;
                UseWaitCursor = false;
            }
            else
            {
                FormSearchResult form = new FormSearchResult();
                form.mId = edID.Text.Trim();
                if ( form.ShowDialog(this) == DialogResult.OK )
                {
                    UseWaitCursor = true;
                    Cursor = Cursors.WaitCursor;
                    fetchLyric( form.resultID );
                    Cursor = Cursors.Default;
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

        private void btnSave_Click( object sender, EventArgs e )
        {
            if ( string.IsNullOrEmpty( lyricTitle ) ) return;
            if ( string.IsNullOrEmpty( edLyric.Text ) ) return;

            dlgSave.FileName = lyricTitle;
            if (dlgSave.ShowDialog(this) == DialogResult.OK)
            {
                if(chkSaveSplit.Checked)
                {
                    var pos = edLyric.Text.LastIndexOf("[ti:", StringComparison.CurrentCultureIgnoreCase);
                    File.WriteAllText( Path.ChangeExtension( dlgSave.FileName, $".any.lrc" ), edLyric.Text.Substring( 0, pos ), Encoding.UTF8 );
                    if(pos>=250)
                    {
                        File.WriteAllText( Path.ChangeExtension( dlgSave.FileName, $".chs.lrc" ), edLyric.Text.Substring( pos, edLyric.Text.Length - pos ), Encoding.UTF8 );
                    }
                }
                else
                {
                    File.WriteAllText( dlgSave.FileName, edLyric.Text, Encoding.UTF8 );
                }
            }
        }

        private void btnExit_Click( object sender, EventArgs e )
        {
            Close();
        }
    }
}
