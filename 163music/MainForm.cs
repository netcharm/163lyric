using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using netcharm.common;

namespace _163music
{
    public partial class MainForm : Form
    {
        private string APPPATH = Path.GetDirectoryName(Application.ExecutablePath);

        private static Configuration config = ConfigurationManager.OpenExeConfiguration( Application.ExecutablePath );
        private AppSettingsSection appSection = config.AppSettings;

        private Pandoc pandoc;
        private Form logger;
        private int retcode = -1;
        string[] txts = { ".md", ".rst", ".html", ".htm", ".tex", ".txt" };

        private void loadSettings()
        {
            try
            {
                var X = appSection.Settings["LastLocationX"].Value;
                var Y = appSection.Settings["LastLocationY"].Value;
                Location = new Point( Convert.ToInt32(X), Convert.ToInt32(Y) );
            }
            catch
            {
                appSection.Settings.Add( "LastLocationX", Location.X.ToString() );
                appSection.Settings.Add( "LastLocationY", Location.Y.ToString() );
            }
        }

        private void saveSetting()
        {
            try
            {
                appSection.Settings["LastLocationX"].Value = Location.X.ToString();
                appSection.Settings["LastLocationY"].Value = Location.Y.ToString();
            }
            catch
            {
                appSection.Settings.Add( "LastLocationX", Location.X.ToString() );
                appSection.Settings.Add( "LastLocationY", Location.Y.ToString() );
            }
            config.Save();
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load( object sender, EventArgs e )
        {
            Application.EnableVisualStyles();
            Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );

            loadSettings();

            pandoc = new Pandoc();
            //pandoc.Logger = edOut;
            logger = new FormLogger();
            pandoc.Logger = ( logger as FormLogger ).Logger;
        }

        private void MainForm_FormClosed( object sender, FormClosedEventArgs e )
        {
            saveSetting();
        }

        #region DrapDrop Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragEnter( object sender, DragEventArgs e )
        {
            string[] exts = new string[] { ".md" };
            var formats = e.Data.GetFormats();
            if ( formats.Contains( "FileDrop" ) )
            {
                var ismd = true;
                foreach (var f in (string[]) e.Data.GetData( DataFormats.FileDrop, true ))
                {
                    if ( !txts.Contains( Path.GetExtension( f ).ToLower() ) )
                    {
                        ismd = false;
                        break;
                    }
                }
                if ( ismd )
                    e.Effect = DragDropEffects.Link;
                else
                    e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        ///         
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_DragDrop( object sender, DragEventArgs e )
        {
            //string[] txts = { "System.String", "UnicodeText", "Text", "OemText", "Html", "RTF" };
            //string[] files = { "FileDrop", "FileName", "FileNameW", "Shell IDList Array" };

            var formats = e.Data.GetFormats();
            if ( formats.Contains( "FileContents" ) && formats.Contains( "text/html" ) )
            {

            }
            else if ( formats.Contains( "Text" ) || formats.Contains( "UnicodeText" ) || formats.Contains( "System.String" ) )
            {

            }
            else if ( formats.Contains( "FileDrop" ) )
            {
                string[] flist = (string[])e.Data.GetData( DataFormats.FileDrop, true );
                ConvertMarkdown( flist );
            }
        }
        #endregion DragDrop Events

        private void chkTopmost_CheckedChanged( object sender, EventArgs e )
        {
            TopMost = chkTopmost.Checked;
        }

        private void btnConvert_Click( object sender, EventArgs e )
        {
            dlgOpen.FileName = "*.md";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                ConvertMarkdown( dlgOpen.FileNames );
            }            
        }

        private void btnLogger_Click( object sender, EventArgs e )
        {
            if( logger == null || logger.IsDisposed)
            {
                logger = new FormLogger();
                pandoc.Logger = ( logger as FormLogger ).Logger;
            }
            if ( !logger.Visible )
                logger.Show( this );
            else if ( logger.WindowState == FormWindowState.Minimized )
                logger.WindowState = FormWindowState.Normal;
            if ( !logger.IsAccessible )
            {
                logger.Activate();
            }

        }

        private void btnPandocHelp_Click( object sender, EventArgs e )
        {
            pandoc.Run(new string[] { "--help" } );
        }

        private void ConvertMarkdown( string[] files, string args=default(string))
        {
            string[] txts = { ".md", ".rst", ".html", ".htm", ".tex", ".txt" };

            if ( files.Length > 0 )
            {
                //PANDOCOPT =-S --include-in-header="%INCLUDE%" --include-before-body="%BODY_BEFORE%" --include-after-body="%BODY_AFTER%" --columns=100 --toc -t html5
                //param.Add( "--include-in-header=\"%INCLUDE%\"" );
                //param.Add( "--include-before-body=\"%BODY_BEFORE%\"" );
                //param.Add( "--include-after-body=\"%BODY_AFTER%\"" );
                //param.Add( "--columns=100" );
                //param.Add( "--toc" );
                //param.Add( "-t html5" );

                pandoc.TOC = true;
                pandoc.ToFormat = "html5";
                pandoc.IncludeInHeader = Path.Combine( APPPATH, "markdown-header.html" );

                foreach ( var f in files )
                {
                    if ( File.Exists( f ) && txts.Contains( Path.GetExtension( f ).ToLower() ) )
                    {
                        if(chkFixFilename.Checked)
                        {
                            FixMarkdown( f );
                        }

                        pandoc.InputFile = f;
                        pandoc.OutputFile = Path.ChangeExtension( f, ".html" );

                        List<string> param = new List<string>();
                        param.Add( args );

                        retcode = pandoc.Run( param.ToArray() );
                        if ( retcode == 0 )
                            pandoc.Log( LogLevel.Tip, $"Converting {f} to {pandoc.ToFormat} OK" );
                        else
                            pandoc.Log( LogLevel.Warning, $"Converting {f} to {pandoc.ToFormat} FAIL" );
                    }
                }
            }
        }

        private void FixMarkdown( string mdf )
        {
            string[] mvts = new string[] { ".mp4", ".webm", ".ogg" };
            string[] ats = new string[] { ".mp3", ".ogg", ".aac", ".flac", ".wav" };

            string mdd = Path.GetDirectoryName(mdf);

            var cwd = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory( mdd );

            var lines = File.ReadAllLines(mdf).ToList();

            var mdtext = string.Join("\n", lines); //File.ReadAllText(mdf);

            bool lf_fixed = false;
            #region fix cover & audio local filename
            Regex rgx_audio = new Regex(@"(.*?)src( *)=( *)""\.\/(.*?)""(.*?)", RegexOptions.IgnoreCase);
            Regex rgx_cover = new Regex(@"^!\[.*?\]\((.*?\.((jpg)|(png)))", RegexOptions.IgnoreCase);

            var allfiles = Directory.GetFiles(mdd, "*.*", SearchOption.AllDirectories);
            List<string> files = new List<string>();
            for(int i=0;i< allfiles.Length;i++ )
            {
                var fx = allfiles[i].Substring( mdd.Length + 1 );
                if ( ats.Contains( Path.GetExtension( fx ).ToLower() ) )
                {
                    files.Add( fx.Replace( "\\", "/" ) );
                }
            }
            for ( int i = 0; i < lines.Count; i++ )
            {
                #region Fix cover filename
                if(lines[i].StartsWith( "![Front Cover](", StringComparison.CurrentCultureIgnoreCase ) )
                {
                    var match = rgx_cover.Match( lines[i] );
                    if ( match.Length > 0 )
                    {
                        var fn = match.Groups[1].Value;
                        if(!File.Exists(fn))
                        {
                            var ext = Path.GetExtension(fn);
                            if ( ext.Equals( ".jpg", StringComparison.CurrentCultureIgnoreCase ) )
                            {
                                var fpng = Path.ChangeExtension( fn, ".png" );
                                if ( File.Exists( fpng ) )
                                {
                                    lines[i] = lines[i].Replace( fn, fpng );
                                    pandoc.Log( LogLevel.Warning, $"> Cover changed from [{fn}] to [{fpng}]." );
                                    lf_fixed = true;
                                }
                            }
                            else if ( ext.Equals( ".png", StringComparison.CurrentCultureIgnoreCase ) )
                            {
                                var fjpg = Path.ChangeExtension( fn, ".jpg" );
                                if ( File.Exists( fjpg ) )
                                {
                                    lines[i] = lines[i].Replace( fn, fjpg );
                                    pandoc.Log( LogLevel.Warning, $"> Cover changed from [{fn}] to [{fjpg}]." );
                                    lf_fixed = true;
                                }
                            }
                        }
                    }
                }
                #endregion

                #region fix audio filename
                MatchCollection matches = rgx_audio.Matches(lines[i]);
                if ( matches.Count > 0 )
                {
                    var fn = matches[0].Groups[4].Value;
                    if ( File.Exists( fn ) ) continue;
                    else
                    {
                        //var trk = fn.Substring(0, fn.IndexOf("_")+1);
                        var trk_path = Path.GetDirectoryName(fn);
                        var trk_name = Path.GetFileName(fn);
                        var trk_num = trk_name.Substring(0, trk_name.IndexOf("_")+1);
                        var trk_title = Path.GetFileNameWithoutExtension(trk_name.Substring(trk_name.IndexOf("_")+1));
                        var trk = Path.Combine(trk_path, trk_num).Replace("\\", "/");
                        var trks = files.Where( o => o.StartsWith( trk, StringComparison.CurrentCultureIgnoreCase )==true );

                        for ( int x = 0; x < files.Count; x++ )
                        {
                            var fx = files[x];
                            if ( files[x].Contains( fn ) )
                            {
                                lines[i] = lines[i].Replace( fn, fx );
                                pandoc.Log( LogLevel.Warning, $"> Audio changed from [{fn}] to [{fx}]." );
                                lf_fixed = true;
                                break;
                            }
                            else if ( Path.GetFileNameWithoutExtension( fn ).Contains( Path.GetFileNameWithoutExtension( fx ) ) )
                            {
                                lines[i] = lines[i].Replace( fn, fx );
                                pandoc.Log( LogLevel.Warning, $"> Audio changed from [{fn}] to [{fx}]." );
                                lf_fixed = true;
                                break;
                            }
                            else if ( trks.Count() == 1 )
                            {
                                lines[i] = lines[i].Replace( fn, trks.First() );
                                pandoc.Log( LogLevel.Warning, $"> Audio changed from [{fn}] to [{trks.First()}]." );
                                lf_fixed = true;
                                break;
                            }
                            else if ( Path.GetFileNameWithoutExtension(fx).Contains(trk_title) )
                            {
                                lines[i] = lines[i].Replace( fn, fx );
                                pandoc.Log( LogLevel.Warning, $"> Audio changed from [{fn}] to [{fx}]." );
                                lf_fixed = true;
                                break;
                            }
                        }
                    }
                }
                #endregion
            }
            #endregion

            #region Add videos & subtitles
            var dir = new DirectoryInfo(mdd);
            var mvf = dir.EnumerateFiles().Where( f => mvts.Contains( f.Extension ) );

            int mvcount = 0;
            var idx = mdtext.IndexOf( "### MV" );
            StringBuilder sb = new StringBuilder();
            foreach ( var f in mvf )
            {
                var fr = f.FullName.Substring( mdd.Length + 1 );
                if ( mdtext.ToString().IndexOf( fr ) >= 0 ) continue;

                mvcount++;
                sb.Clear();
                sb.AppendLine( $"<div class=\"video\">" );
                sb.AppendLine( $"  <video id=\"mv{mvcount.ToString( "00" )}\" class=\"video\" controls src=\"./{fr.Replace( "\\", "/" )}\" preload=\"metadata\">" );

                #region Add video subtitles
                var vtts = Directory.GetFiles( mdd, $"{Path.GetFileNameWithoutExtension( fr )}*.vtt" );
                if ( vtts.Length > 0 )
                {
                    if ( vtts.Length == 1 )
                        sb.AppendLine( $"    <track label=\"English\" kind=\"subtitles\" srclang=\"en\" />" );
                    foreach ( string vtt in vtts )
                    {
                        var vttn = vtt.Substring( mdd.Length + 1 ).Replace( "\\", "/" );
                        var lang = "cn";
                        var langl = "Chinese";
                        var isdefault = string.Empty;
                        if ( vttn.ToLower().Contains( "chs" ) || vttn.ToLower().Contains( "cht" ) || vtts.Length == 1 )
                        {
                            isdefault = " default";
                        }
                        else if ( vttn.ToLower().Contains( "jpn" ) || vttn.ToLower().Contains( "jp" ) )
                        {
                            lang = "jp";
                            langl = "Japanese";
                        }
                        else if ( vttn.ToLower().Contains( "eng" ) || vttn.ToLower().Contains( "enu" ) || vttn.ToLower().Contains( "en" ) || vttn.ToLower().Contains( "us" ) )
                        {
                            lang = "en";
                            langl = "English";
                        }
                        sb.AppendLine( $"    <track label=\"{langl}\" kind=\"subtitles\" srclang=\"{lang}\" src=\"./{vttn}\"{isdefault} />" );
                    }
                }
                #endregion

                sb.AppendLine( $"  </video>" );
                sb.AppendLine( $"</div>" );
                sb.AppendLine( "" );
                pandoc.Log( LogLevel.Warning, $"> Add video [{fr}]." );
            }
            #endregion

            #region insert html video tags to markdown
            if ( mvcount > 0 )
            {
                var ix = -1;
                for(int i = 0; i< lines.Count; i++)
                {
                    if ( lines[i].Trim().StartsWith( "### MV" ) )
                    {
                        ix = i;
                        break;
                    }
                }
                if ( ix >= 0 )
                {
                    lines.InsertRange( ix + 1, string.Join( "", sb ).Replace("\r", "").Trim().Split( '\n' ) );
                    lines.Insert( ix + 1, "" );
                }
                else
                {
                    lines.Add( "" );
                    lines.Add( "### MV视频{.mv-section}" );
                    lines.Add( "" );
                    lines.AddRange( string.Join( "", sb ).Replace( "\r", "" ).Trim().Split('\n') );
                }
            }
            #endregion

            if ( mvcount>0 || lf_fixed )
            {
                File.WriteAllLines( mdf, lines, Encoding.UTF8 );
                //File.WriteAllText( mdf, mdtext );
            }
            Directory.SetCurrentDirectory( cwd );
        }

    }
}
