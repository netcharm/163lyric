using System;
using System.Drawing;
using System.Configuration;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using _163lyric;

namespace _163music
{
    public partial class MainForm : Form
    {
        private string lyricTitle = string.Empty;
        private string lyricAlias = string.Empty;
        private string lyricArtist = string.Empty;
        private string lyricAlbum = string.Empty;
        private string lyricTrack = string.Empty;

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
                appSection.Settings.Add("LastDirectory", LastDirectory);
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
                appSection.Settings.Add("LastDirectory", LastDirectory);
            }
            config.Save();
        }

        private string fetchLyric(string id)
        {
            var result = string.Empty;
            NetEaseMusic lyric = new NetEaseMusic();
            try
            {
                int musicID = Convert.ToInt32( id );
                string[] sDetail = lyric.getSongDetail( musicID );

                lblTitle.Text = "";
                edLyric.Text = "";

                if (sDetail.Length <= 0)
                {
                    MessageBox.Show("Music Detail Infomation Not Found！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return(result);
                }

                lyricTitle = sDetail[0];
                lyricAlias = sDetail[1];
                lyricArtist = sDetail[2];
                lyricAlbum = sDetail[3];
                lyricTrack = sDetail[4];

                lblTitle.Text = $"{lyricTitle} / {lyricAlias} / {lyricArtist} / {lyricAlias}";

                string[] mLrc = cbLrcMultiLang.Checked ? lyric.getSongLyricMultiLang(musicID) : lyric.getSongLyric(Convert.ToInt32(edID.Text.Trim())); 

                if (mLrc.Length == 1 && mLrc[0].Length < 40)
                {
                    MessageBox.Show(string.Join(",", mLrc), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return(result);
                }
                StringBuilder sb = new StringBuilder();
                foreach (string lrc in mLrc)
                {
                    if (Regex.Match(lrc, @"\[ti:.*?\]", RegexOptions.IgnoreCase | RegexOptions.Multiline).Length <= 0)
                        sb.AppendLine($"[ti:{lyricTitle}]");
                    if (Regex.Match(lrc, @"\[ar:.*?\]", RegexOptions.IgnoreCase | RegexOptions.Multiline).Length <= 0)
                        sb.AppendLine($"[ar:{lyricArtist}]");
                    if (Regex.Match(lrc, @"\[al:.*?\]", RegexOptions.IgnoreCase | RegexOptions.Multiline).Length <= 0)
                        sb.AppendLine($"[al:{lyricAlbum}]");
                    if (lyricAlias.Length > 0)
                    {
                        if (Regex.Match(lrc, @"\[alias:.*?\]", RegexOptions.IgnoreCase | RegexOptions.Multiline).Length <= 0)
                            sb.AppendLine($"[alias:{lyricAlias}]");
                    }
                    sb.AppendLine(lrc);
                    sb.AppendLine();
                    sb.AppendLine();
                }
                result = sb.ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入正确的数字或歌曲名称！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return (result);
        }

        private string[] makeLyric(Song song, int trk_no, string trk_name, List<string> artist, string lyric)
        {
            var result = new string[] { };

            List<string> sb = new List<string>();

            string[] songlyric = lyric.Split();
            if (songlyric.Length > 0)
            {
                var lyrics = songlyric[0].Split( new char[] { '\n', '\r' } );
                var lyric_name = $"{trk_no}_{trk_name}.lrc";
                var lyric_fullname = Path.Combine(LastDirectory, lyric_name);

                if (lyrics.Where(o => o.StartsWith("[ti:", StringComparison.CurrentCultureIgnoreCase)).Count() <= 0)
                    sb.Add($"[ti:{song.Title}]");
                if (lyrics.Where(o => o.StartsWith("[ar:", StringComparison.CurrentCultureIgnoreCase)).Count() <= 0)
                    sb.Add($"[ar:{string.Join(" / ", artist.ToArray())}]");
                if (lyrics.Where(o => o.StartsWith("[al:", StringComparison.CurrentCultureIgnoreCase)).Count() <= 0)
                    sb.Add($"[al:{song.Album.Title}]");

                sb.AddRange(lyrics.Select(o => o.Trim()).Where(o => !string.IsNullOrEmpty(o.Trim())));
                result = sb.ToArray();
            }
            return (result);
        }

        public MainForm()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            progress.Visible = false;
            loadSettings();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!LastDirectory.Equals(LastDirectory_old, StringComparison.CurrentCultureIgnoreCase))
                saveSetting();
        }

        private void edID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                btnGet.PerformClick();
            }
        }

        private void edLyric_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                btnCopy.PerformClick();
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            var t = edID.Text.Trim();
            t = Regex.Replace(t.ToLower(), @"http(s)*://music\.163\.com/(#/)*", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);

            //t.Replace( "https://music.163.com/song?id=", "" );
            NetEaseMusic lyric = new NetEaseMusic();
            int n;
            string tid = t.Replace("song?id=", "");
            bool isNumeric = int.TryParse(tid, out n);
            if (isNumeric)
            {
                UseWaitCursor = true;
                Cursor = Cursors.WaitCursor;
                edLyric.Text = fetchLyric(tid);
                Cursor = Cursors.Default;
                UseWaitCursor = false;
            }
            else if (t.StartsWith("album?id=") || t.StartsWith("album/"))
            {
                var lyric_path = Directory.GetCurrentDirectory();
                var match = Regex.Match(t, @"album((\?id=)|(/))(\d+)");
                if (match.Length > 0)
                {
                    var aid = Convert.ToInt32(match.Groups[4].Value);

                    edLyric.Clear();
                    progress.Value = 0;
                    progress.Visible = true;
                    progress.Size = btnCopy.Size;
                    progress.Location = btnCopy.Location;
                    progress.BringToFront();
                    if (bgwGetAlbumLyrics.IsBusy)
                    {
                        bgwGetAlbumLyrics.CancelAsync();
                    }
                    else
                    {
                        btnGet.Text = "DL...";
                        bgwGetAlbumLyrics.RunWorkerAsync(aid);
                    }
                }
            }
            else if (t.StartsWith("http://"))
            {
                return;
            }
            else
            {
                FormSearchResult form = new FormSearchResult();
                form.mId = t;
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    UseWaitCursor = true;
                    Cursor = Cursors.WaitCursor;
                    edLyric.Text = fetchLyric(form.resultID);
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
            if (edLyric.SelectionLength <= 0)
            {
                content = edLyric.Text.Replace(Environment.NewLine, "\r");
            }
            else
            {
                content = edLyric.SelectedText.Replace(Environment.NewLine, "\r");
            }
            //Clipboard.SetDataObject(content);
            Clipboard.SetText(content, TextDataFormat.UnicodeText);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lyricTitle)) return;
            if (string.IsNullOrEmpty(edLyric.Text)) return;

            dlgSave.FileName = $"{lyricTrack}_{lyricTitle}";
            if (dlgSave.ShowDialog(this) == DialogResult.OK)
            {
                var head = edLyric.Text.Substring(200);
                var pos = edLyric.Text.Length;
                var match = Regex.Match(head, @"\[((ti)|(al)|(ar)|(by)|(offset)):.*?\]", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                if (chkSaveSplit.Checked && match.Length > 1)
                {
                    pos = match.Groups[1].Index + 200 - 1;
                    File.WriteAllText(Path.ChangeExtension(dlgSave.FileName, $".lrc"), edLyric.Text.Substring(0, pos), Encoding.UTF8);
                    var chs = edLyric.Text.Substring(pos, edLyric.Text.Length - pos);
                    if (pos >= 250) File.WriteAllText(Path.ChangeExtension(dlgSave.FileName, $".chs.lrc"), chs, Encoding.UTF8);
                }
                else
                {
                    File.WriteAllText(Path.ChangeExtension(dlgSave.FileName, $".lrc"), edLyric.Text, Encoding.UTF8);
                }
                LastDirectory = Path.GetDirectoryName(dlgSave.FileName);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bgwGetAlbumLyrics_DoWork(object sender, DoWorkEventArgs e)
        {
            NetEaseMusic lyric = new NetEaseMusic();
            var aid = Convert.ToInt32(e.Argument);
            var album = lyric.getAlbumDetail(aid);
            edLyric.Tag = $"Target directory : {LastDirectory} ...";
            bgwGetAlbumLyrics.ReportProgress(10);
            int count = 0;
            foreach (var song in album.Songs)
            {
                if (bgwGetAlbumLyrics.CancellationPending) break;

                try
                {
                    List<string> artist = new List<string>();
                    foreach (var a in song.Artists)
                    {
                        artist.Add(a.Name);
                    }
                    var trk_no = song.Track.ToString("00");
                    if (album.Songs.Count < 100)
                        trk_no = song.Track.ToString("00");
                    else if (album.Songs.Count < 1000)
                        trk_no = song.Track.ToString("000");
                    else if (album.Songs.Count < 10000)
                        trk_no = song.Track.ToString("0000");
                    var trk_name = song.Title;
                    var trk_id = song.ID;

                    var lyric_name = $"{trk_no}_{trk_name}.lrc";
                    var lyric_fullname = Path.Combine(LastDirectory, lyric_name);

                    string[] mlc = cbLrcMultiLang.Checked ? lyric.getSongLyricMultiLang(trk_id) : lyric.getSongLyric(trk_id);

                    if (chkSaveSplit.Checked && mlc.Length > 1)
                    {
                        for (int i = 0; i < mlc.Length; i++)
                        {
                            StringBuilder sb = new StringBuilder();
                            string songlyric = mlc[i];
                            if (songlyric.Length <= 0) continue;
                            if (Regex.Match(songlyric, @"\[ti:.*?\]", RegexOptions.IgnoreCase | RegexOptions.Multiline).Length <= 0)
                                sb.AppendLine($"[ti:{song.Title}]");
                            if (Regex.Match(songlyric, @"\[ar:.*?\]", RegexOptions.IgnoreCase | RegexOptions.Multiline).Length <= 0)
                                sb.AppendLine($"[ar:{string.Join(" / ", artist.ToArray())}]");
                            if (Regex.Match(songlyric, @"\[al:.*?\]", RegexOptions.IgnoreCase | RegexOptions.Multiline).Length <= 0)
                                sb.AppendLine($"[al:{song.Album.Title}]");
                            if (Regex.Match(songlyric, @"\[alias:.*?\]", RegexOptions.IgnoreCase | RegexOptions.Multiline).Length <= 0)
                                sb.AppendLine($"[alias:{song.Alias}]");
                            sb.AppendLine(songlyric);

                            if (i == 0)
                                File.WriteAllText(lyric_fullname, sb.ToString(), Encoding.UTF8);
                            else if (i == 1)
                                File.WriteAllText(Path.ChangeExtension(lyric_fullname, $".chs.lrc"), sb.ToString(), Encoding.UTF8);
                            else
                                File.WriteAllText(Path.ChangeExtension(lyric_fullname, $".{i}.lrc"), sb.ToString(), Encoding.UTF8);
                        }
                    }
                    else
                    {
                        List<string> sb = new List<string>();

                        var songlyric = lyric.getSongLyric( trk_id );
                        if (songlyric.Length <= 0) continue;
                        var lyrics = songlyric[0].Split( new char[] { '\n', '\r' } );

                        if (lyrics.Where(o => o.StartsWith("[ti:", StringComparison.CurrentCultureIgnoreCase)).Count() <= 0)
                            sb.Add($"[ti:{song.Title}]");
                        if (lyrics.Where(o => o.StartsWith("[ar:", StringComparison.CurrentCultureIgnoreCase)).Count() <= 0)
                            sb.Add($"[ar:{string.Join(" / ", artist.ToArray())}]");
                        if (lyrics.Where(o => o.StartsWith("[al:", StringComparison.CurrentCultureIgnoreCase)).Count() <= 0)
                            sb.Add($"[al:{song.Album.Title}]");
                        if (lyrics.Where(o => o.StartsWith("[alias:", StringComparison.CurrentCultureIgnoreCase)).Count() <= 0)
                            sb.Add($"[alias:{song.Alias}]");

                        sb.AddRange(lyrics.Select(o => o.Trim()).Where(o => !string.IsNullOrEmpty(o.Trim())));
                        File.WriteAllLines(lyric_fullname, sb.ToArray(), Encoding.UTF8);
                    }

                    count++;
                    edLyric.Tag = $"> {lyric_name} downloaded.";
                    bgwGetAlbumLyrics.ReportProgress(10 + (int)Math.Floor(count * 100F / album.Songs.Count));
                }
                catch (Exception) { }
            }
        }

        private void bgwGetAlbumLyrics_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage <= 100)
                progress.Value = e.ProgressPercentage;
            if (edLyric.Tag != null)
                edLyric.AppendText($"{(string)edLyric.Tag}{Environment.NewLine}");
        }

        private void bgwGetAlbumLyrics_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progress.Visible = false;
            btnGet.Text = "GET";
            edLyric.Tag = null;
            System.Media.SystemSounds.Beep.Play();
        }

    }
}
