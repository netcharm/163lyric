using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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

                if (cbLrcMultiLang.Checked)
                {
                    string[] mLrc = lyric.getLyricMultiLang( musicID );
                    if ( mLrc.Length == 1 && mLrc[0].Length < 40)
                    {
                        MessageBox.Show( String.Join(",", mLrc), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                        return;
                    }
                    foreach ( string lrc in mLrc)
                    {
                        edLyric.Text += $"[ti:{sDetail[0]}]" + Environment.NewLine;
                        edLyric.Text += $"[alias:{sDetail[1]}]" + Environment.NewLine;
                        edLyric.Text += $"[ar:{sDetail[2]}]" + Environment.NewLine;
                        edLyric.Text += $"[al:{sDetail[3]}]" + Environment.NewLine;
                        edLyric.Text += lrc;
                        edLyric.Text += "\r\n";
                    }
                }
                else
                {
                    string lrc = lyric.getLyric( Convert.ToInt32( IDtb.Text.Trim() ) );
                    if ( lrc.Length < 40 )
                    {
                        MessageBox.Show( lrc, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                        return;
                    }
                    edLyric.Text  = $"[ti:{sDetail[0]}]" + Environment.NewLine;
                    edLyric.Text += $"[alias:{sDetail[1]}]" + Environment.NewLine;
                    edLyric.Text += $"[ar:{sDetail[2]}]" + Environment.NewLine;
                    edLyric.Text += $"[al:{sDetail[3]}]" + Environment.NewLine;
                    edLyric.Text += lrc;
                    //Clipboard.SetDataObject(Lyrictb.Text);
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
