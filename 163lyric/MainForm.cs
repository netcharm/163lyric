using System;
using System.Drawing;
using System.Configuration;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using _163lyric;
using System.Collections.Generic;
using System.Linq;

namespace _163music
{
    public partial class MainForm : Form
    {
        private string lyricTitle = "";
        private string lyricAlias = "";
        private string lyricArtist = "";
        private string lyricAlbum = "";

        private static Configuration config = ConfigurationManager.OpenExeConfiguration( Application.ExecutablePath );
        private AppSettingsSection appSection = config.AppSettings;
        private string LastDirectory = "";
        private string LastDirectory_old = "";

        private void loadSettings()
        {
            try
            {
                LastDirectory = appSection.Settings["LastDirectory"].Value;
            }
            catch
            {
                appSection.Settings.Add( "LastDirectory", LastDirectory );
            }
            LastDirectory_old = LastDirectory;
        }

        private void saveSetting()
        {
            try
            {
                    appSection.Settings["LastDirectory"].Value = LastDirectory;
            }
            catch
            {
                appSection.Settings.Add( "LastDirectory", LastDirectory );
            }
            config.Save();
        }

        private void fetchLyric( string id )
        {
            NetEaseMusic lyric = new NetEaseMusic();
            try
            {
                int musicID = Convert.ToInt32( id );
                string[] sDetail = lyric.getSongDetail( musicID );

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
                    mLrc = lyric.getSongLyricMultiLang( musicID );
                }
                else
                {
                    mLrc = lyric.getSongLyric( Convert.ToInt32( edID.Text.Trim() ) );

                }
                if ( mLrc.Length == 1 && mLrc[0].Length < 40 )
                {
                    MessageBox.Show( string.Join( ",", mLrc ), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    return;
                }
                foreach ( string lrc in mLrc )
                {
                    if ( Regex.Match(lrc, @"\[ti:.*?\]", RegexOptions.IgnoreCase | RegexOptions.Multiline).Length <=0 )
                        edLyric.Text += $"[ti:{lyricTitle}]" + Environment.NewLine;
                    if ( Regex.Match( lrc, @"\[ar:.*?\]", RegexOptions.IgnoreCase | RegexOptions.Multiline ).Length <= 0 )
                        edLyric.Text += $"[ar:{lyricArtist}]" + Environment.NewLine;
                    if ( Regex.Match( lrc, @"\[al:.*?\]", RegexOptions.IgnoreCase | RegexOptions.Multiline ).Length <= 0 )
                        edLyric.Text += $"[al:{lyricAlbum}]" + Environment.NewLine;
                    if ( lyricAlias.Length > 0 )
                    {
                        if ( Regex.Match( lrc, @"\[alias:.*?\]", RegexOptions.IgnoreCase | RegexOptions.Multiline ).Length <= 0 )
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

        public MainForm()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );
        }

        private void MainForm_Load( object sender, EventArgs e )
        {
            progress.Visible = false;
            loadSettings();
        }

        private void btnGet_Click( object sender, EventArgs e )
        {
            var t = edID.Text.Trim();
            NetEaseMusic lyric = new NetEaseMusic();
            int n;
            bool isNumeric = int.TryParse(t, out n);
            if ( isNumeric )
            {
                UseWaitCursor = true;
                Cursor = Cursors.WaitCursor;
                fetchLyric( t );
                Cursor = Cursors.Default;
                UseWaitCursor = false;
            }
            else if(t.StartsWith( "http://music.163.com/#/album?id=" ) || t.StartsWith( "http://music.163.com/album?id=") )
            {
                var lyric_path = Directory.GetCurrentDirectory();
                var match = Regex.Match(t, @"http://.*?album\?id=(\d+)");
                if(match.Length>0)
                {
                    var aid = Convert.ToInt32(match.Groups[1].Value);

                    progress.Value = 0;
                    progress.Visible = true;
                    progress.Size = btnCopy.Size;
                    progress.Location = btnCopy.Location;
                    progress.BringToFront();
                    if( bgwGetAlbumLyrics.IsBusy )
                    {
                        bgwGetAlbumLyrics.CancelAsync();
                    }
                    else
                    {
                        bgwGetAlbumLyrics.RunWorkerAsync( aid );
                        btnGet.Text = "Downloading";
                    }
                }
            }
            else if(t.StartsWith("http://"))
            {
                return;
            }
            else
            {
                FormSearchResult form = new FormSearchResult();
                form.mId = t;
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
                if ( chkSaveSplit.Checked )
                {
                    var match = Regex.Match( edLyric.Text.Substring(50), @"\[((ti)|(al)|(ar)|(by)|(offset)):.*?\]", RegexOptions.IgnoreCase | RegexOptions.Multiline );
                    var pos = edLyric.Text.Length;
                    if ( match.Length > 0 )
                    {
                        pos = match.Groups[1].Index + 50 - 1;
                        File.WriteAllText( Path.ChangeExtension( dlgSave.FileName, $".any.lrc" ), edLyric.Text.Substring( 0, pos ), Encoding.UTF8 );
                        if ( pos >= 250 )
                        {
                            File.WriteAllText( Path.ChangeExtension( dlgSave.FileName, $".chs.lrc" ), edLyric.Text.Substring( pos, edLyric.Text.Length - pos ), Encoding.UTF8 );
                        }
                    }
                }
                else
                {
                    File.WriteAllText( dlgSave.FileName, edLyric.Text, Encoding.UTF8 );
                }
                LastDirectory = Path.GetDirectoryName( dlgSave.FileName );
            }
        }

        private void btnExit_Click( object sender, EventArgs e )
        {
            Close();
        }

        private void bgwGetAlbumLyrics_DoWork( object sender, System.ComponentModel.DoWorkEventArgs e )
        {
            NetEaseMusic lyric = new NetEaseMusic();
            var aid = Convert.ToInt32(e.Argument);
            var album = lyric.getAlbumDetail(aid);
            bgwGetAlbumLyrics.ReportProgress( 10 );
            int count = 0;
            foreach ( var song in album.Songs )
            {
                if ( bgwGetAlbumLyrics.CancellationPending ) break;

                try
                {
                    List<string> sb = new List<string>();

                    List<string> artist = new List<string>();
                    foreach ( var a in song.Artists )
                    {
                        artist.Add( a.Name );
                    }
                    var trk_no = song.Track.ToString("00");
                    if ( album.Songs.Count < 100 )
                        song.Track.ToString( "00" );
                    else if ( album.Songs.Count < 1000 )
                        song.Track.ToString( "000" );
                    else if ( album.Songs.Count < 10000 )
                        song.Track.ToString( "0000" );
                    var trk_name = song.Title;
                    var trk_id = song.ID;
                    var songlyric = lyric.getSongLyric( trk_id );
                    if ( songlyric.Length <= 0 ) continue;
                    var lyrics = songlyric[0].Split(new char[] { '\n', '\r' } );
                    var lyric_name = $"{trk_no}_{trk_name}.lrc";
                    var lyric_fullname = Path.Combine(LastDirectory, lyric_name);

                    if ( lyrics.Where( o => o.StartsWith( "[ti:", StringComparison.CurrentCultureIgnoreCase ) ).Count() <= 0 )
                        sb.Add( $"[ti:{song.Title}]" );
                    if ( lyrics.Where( o => o.StartsWith( "[ar:", StringComparison.CurrentCultureIgnoreCase ) ).Count() <= 0 )
                        sb.Add( $"[ar:{string.Join( " / ", artist.ToArray() )}]" );
                    if ( lyrics.Where( o => o.StartsWith( "[al:", StringComparison.CurrentCultureIgnoreCase ) ).Count() <= 0 )
                        sb.Add( $"[al:{song.Album.Title}]" );

                    sb.AddRange( lyrics.Select( o => o.Trim() ).Where( o => !string.IsNullOrEmpty( o.Trim() ) ) );
                    File.WriteAllLines( lyric_fullname, sb.ToArray(), Encoding.UTF8 );
                    count++;
                    bgwGetAlbumLyrics.ReportProgress( 10 + (int) Math.Floor( count * 100F / album.Songs.Count ) );
                }
                catch ( Exception ) { }
            }
        }

        private void bgwGetAlbumLyrics_ProgressChanged( object sender, System.ComponentModel.ProgressChangedEventArgs e )
        {
            if( e.ProgressPercentage <=100)
                progress.Value = e.ProgressPercentage;
        }

        private void bgwGetAlbumLyrics_RunWorkerCompleted( object sender, System.ComponentModel.RunWorkerCompletedEventArgs e )
        {
            progress.Visible = false;
            btnGet.Text = "GET";
            System.Media.SystemSounds.Beep.Play();
        }

        private void MainForm_FormClosed( object sender, FormClosedEventArgs e )
        {
            if ( !LastDirectory.Equals( LastDirectory_old, StringComparison.CurrentCultureIgnoreCase ) )
                saveSetting();
        }
    }
}
