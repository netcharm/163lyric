using System;
using System.Drawing;
using System.Windows.Forms;

namespace _163lyric
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            this.Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );
        }

        private void Getbtn_Click(object sender, EventArgs e)
        {
            NetEaseLyric lyric = new NetEaseLyric();
            try
            {
                int musicID = Convert.ToInt32( IDtb.Text.Trim() );
                string[] sDetail = lyric.getDetail( musicID );

                lblTitle.Text = "";
                edLyric.Text = "";

                if ( sDetail.Length<=0)
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
                    mLrc = lyric.getLyric( Convert.ToInt32( IDtb.Text.Trim() ) );

                }
                if ( mLrc.Length == 1 && mLrc[0].Length < 40)
                {
                    MessageBox.Show( string.Join(",", mLrc), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }
                foreach ( string lrc in mLrc)
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
            catch( FormatException ) {
                MessageBox.Show("请输入数字！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
        }

        private void Copybtn_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(edLyric.Text);
        }
    }
}
