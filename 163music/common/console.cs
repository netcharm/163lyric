using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace netcharm.common
{
    public enum LogLevel { Info=0, Tip=1, Warning=2, Error=3 };

    public class ConsoleApp
    {
        public class InfoEventArgs : EventArgs
        {
            public string Text { get; set; }
        }

        private StringBuilder stdout = new StringBuilder();
        public string Stdout
        {
            get
            {
                var so = stdout.ToString();
                return ( so );
            }
        }

        private StringBuilder stderr = new StringBuilder();
        public string Stderr
        {
            get
            {
                var se = stderr.ToString();
                return ( se );
            }
        }

        private List<string> stdline = new List<string>();
        private int LastStdLine = -1;

        public event EventHandler<InfoEventArgs> OnStderr;
        public event EventHandler<InfoEventArgs> OnStdout;

        protected internal BackgroundWorker bgwProcess = new BackgroundWorker();
        private Process p = new Process();
        public Process Process
        {
            get { return ( p ); }
        }

        private Color backColor = Color.FromArgb(200, 32, 32, 32);// Color.DimGray;
        public Color BackgroundColor
        {
            get { return ( backColor ); }
            set
            {
                backColor = value;
                if ( logger != null )
                    logger.BackColor = backColor;
            }
        }

        private Color colorInfo = Color.Silver;
        public Color ColorInfo
        {
            get { return ( colorInfo ); }
            set { colorInfo = value; }
        }

        private Color colorTip = Color.LightGreen;
        public Color ColorTip
        {
            get { return ( colorTip ); }
            set { colorTip = value; }
        }

        private Color colorWarning = Color.Yellow;
        public Color ColorWarning
        {
            get { return ( colorWarning ); }
            set { colorWarning = value; }
        }

        private Color colorError = Color.OrangeRed;
        public Color ColorError
        {
            get { return ( colorError ); }
            set { colorError = value; }
        }

        private RichTextBox logger=null;
        public RichTextBox Logger
        {
            get { return ( logger ); }
            set { logger = value; }
        }

        private bool exited = false;
        public bool Exited
        {
            get { return ( exited ); }
        }


        public ConsoleApp()
        {
            if ( logger != null )
                logger.BackColor = backColor;

            exited = false;

            OnStdout += process_OnStdout;
            OnStderr += process_OnStderr;

            p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            p.StartInfo.RedirectStandardOutput = true;
            //p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardError = true;

            #region stdout & stderr redirect handle
            p.OutputDataReceived += new DataReceivedEventHandler
            (
                delegate ( object sender, DataReceivedEventArgs e )
                {
                    if ( !string.IsNullOrEmpty( e.Data ) )
                    {
                        //stdline.Add( "O> " + e.Data );
                        stdout.AppendLine( e.Data );
                        OnStdout( this, new InfoEventArgs() { Text = e.Data } );
                    }
                }
            );

            p.ErrorDataReceived += new DataReceivedEventHandler
            (
                delegate ( object sender, DataReceivedEventArgs e )
                {
                    if ( !string.IsNullOrEmpty( e.Data ) )
                    {
                        //stdline.Add( "E> " + e.Data );
                        stderr.AppendLine( e.Data );
                        OnStderr( this, new InfoEventArgs() { Text = e.Data } );
                    }
                }
            );
            #endregion

            if ( bgwProcess != null )
            {
                bgwProcess.DoWork += bgwProcess_DoWork;
                bgwProcess.ProgressChanged += bgwProcess_ProgressChanged;
                bgwProcess.RunWorkerCompleted += bgwProcess_RunWorkerCompleted;
            }
        }

        public virtual int Run( string cmd, string[] args )
        {
            stdout.Clear();
            stderr.Clear();

            List<string> param = new List<string>();
            param.AddRange( args );

            p.StartInfo.Arguments = string.Join( " ", param );
            p.StartInfo.FileName = cmd;

            bgwProcess.RunWorkerAsync();

            p.Start();
            p.BeginOutputReadLine();
            p.BeginErrorReadLine();
            p.WaitForExit();

            exited = p.HasExited;

            int exitCode = 0;
            if ( p.HasExited ) exitCode = p.ExitCode;
            p.CancelOutputRead();
            p.CancelErrorRead();
            p.Close();

            return ( exitCode );
        }

        private void bgwProcess_DoWork( object sender, DoWorkEventArgs e )
        {
            while ( true )
            {
                Task.Delay( 20 );
                //Thread.Sleep( 1 );
                bgwProcess.ReportProgress( 0 );
                if ( exited )
                {
                    bgwProcess.CancelAsync();
                    break;
                }
            }
        }

        private void bgwProcess_ProgressChanged( object sender, ProgressChangedEventArgs e )
        {
            if ( exited ) return;
            if ( LastStdLine >= stdline.Count ) return;

            UpdateStdLine();
        }

        private void bgwProcess_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
        {
            UpdateStdLine();
            //Log( LogLevel.Info, "\n" );
        }

        private void process_OnStdout( object sender, ConsoleApp.InfoEventArgs e )
        {
            Log( LogLevel.Info, e.Text );
        }

        private void process_OnStderr( object sender, ConsoleApp.InfoEventArgs e )
        {
            Log( LogLevel.Error, e.Text );
        }

        private void UpdateStdLine()
        {
            if ( logger == null || logger.GetType() != typeof( RichTextBox ) || stdline.Count == 0 ) return;

            if ( LastStdLine < 0 ) LastStdLine = 0;
            for ( int i = LastStdLine; i < stdline.Count; i++ )
            {
                var text = stdline[i];

                int start = logger.TextLength;
                int len = text.Length - 3;
                logger.AppendText( text.Substring( 3 ) + "\r\n" );
                logger.Select( start, len );
                if ( text.StartsWith( "O> ", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    logger.SelectionColor = colorInfo;
                }
                else if ( text.StartsWith( "I> ", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    logger.SelectionColor = colorTip;
                }
                else if ( text.StartsWith( "W> ", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    logger.SelectionColor = colorWarning;
                }
                else if ( text.StartsWith( "E> ", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    logger.SelectionColor = colorError;
                }
                logger.SelectionStart = logger.TextLength;
            }
            LastStdLine = stdline.Count;
        }

        public void Log(LogLevel level, string text)
        {
            switch(level)
            {
                case LogLevel.Info:
                    stdline.Add( "O> " + text );
                    break;
                case LogLevel.Tip:
                    stdline.Add( "I> " + text );
                    break;
                case LogLevel.Warning:
                    stdline.Add( "W> " + text );
                    break;
                case LogLevel.Error:
                    stdline.Add( "E> " + text );
                    break;
                default:
                    stdline.Add( "O> " + text );
                    break;
            }
        }
    }
}
