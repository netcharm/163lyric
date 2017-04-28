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
            var x = Owner.Location.X + Owner.Width + 16;
            var y = Owner.Location.Y + Owner.Height - Height;

            var ox = (Screen.GetWorkingArea(this).Width - Screen.GetWorkingArea(this).Left) /2;
            var oy = (Screen.GetWorkingArea(this).Height - Screen.GetWorkingArea(this).Top) /2;

            if ( Owner.Location.X + Owner.Width < ox)
                x = Owner.Location.X + Owner.Width + 16;
            else if ( Owner.Location.X > ox )
                x = Owner.Location.X - Width - 16;

            if ( Owner.Location.Y > oy - Owner.Height / 2 && Owner.Location.Y < oy + Owner.Height / 2 )
                y = Owner.Location.Y;
            else if ( Owner.Location.Y > oy )
                y = Owner.Location.Y + Owner.Height - Height;
            else if ( Owner.Location.Y < oy )
                y = Owner.Location.Y;

            Location = new Point( x, y );
        }

        private void FormLogger_FormClosing( object sender, FormClosingEventArgs e )
        {
            if ( e.CloseReason == CloseReason.UserClosing )
            {
                Hide();
                e.Cancel = true;
            }
        }

        private void FormLogger_SizeChanged( object sender, EventArgs e )
        {
            edLog.Width = ClientSize.Width;
            edLog.Invalidate();
        }
    }
}
