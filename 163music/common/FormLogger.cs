using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _163music.Properties;

namespace netcharm.common
{
    public partial class FormLogger : Form
    {
        public RichTextBox Logger
        {
            get { return ( edLog ); }
        }

        public FormLogger()
        {
            InitializeComponent();
        }

        private void FormLogger_Load( object sender, EventArgs e )
        {
            //Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );
            Icon = Icon.FromHandle( Resources.Log_32x.GetHicon() );
        }

        private void cmiLoggerClear_Click( object sender, EventArgs e )
        {
            edLog.Clear();
        }

        private void FormLogger_Shown( object sender, EventArgs e )
        {
            Location = new Point( Owner.Location.X + Owner.Width + 16, Owner.Location.Y + Owner.Height - Height );
        }

        private void FormLogger_FormClosing( object sender, FormClosingEventArgs e )
        {
            if ( e.CloseReason == CloseReason.UserClosing )
            {
                Hide();
                e.Cancel = true;
            }
        }
    }
}
