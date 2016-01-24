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
                Lyrictb.Text = lyric.getLyric(Convert.ToInt32(IDtb.Text.Trim()));
                //Clipboard.SetDataObject(Lyrictb.Text);
            }
            catch {
                MessageBox.Show("请输入数字！");
            }
        }

        private void IDtb_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Copybtn_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(Lyrictb.Text);
        }
    }
}
