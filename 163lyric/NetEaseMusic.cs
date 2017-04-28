using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace _163music
{
    public class MusicItem
    {
        public string id = "";
        public string title = "";
        public string title_alias = "";
        public string artist = "";
        public string picture = "";
        public string album = "";
        public string album_alias = "";
        public string cover = "";
        public string company = "";
    }

    public class MusicList
    {
        private List<MusicItem> musicItems;
        private int total = 0;
        private int offset = 0;

        public MusicItem[] Items
        {
            get { return musicItems.ToArray(); }
        }

        public int Offset
        {
            get { return offset; }
        }

        public int Total
        {
            get { return total; }
        }

        public MusicList()
        {
            musicItems = new List<MusicItem>();
        }

        public void Query( string term )
        {
            NetEaseMusic nease = new NetEaseMusic();
            foreach ( MusicItem music in nease.getMusicByTitle( term ) )
            {
                musicItems.Add( music );
            }
            offset = nease.ResultOffset;
            total = nease.ResultTotal;
        }
    }

    class NetEaseMusic
    {
        //反序列化JSON数据  
        char[] charsToTrim = { '*', ' ', '\'', '\"', '\r', '\n' };

        private int queryCount = 0;
        private int queryTotal = 0;
        private int queryOffset = 0;

        public int ResultOffset
        {
            get { return queryOffset; }
        }

        public int ResultCount
        {
            get { return queryCount; }
        }

        public int ResultTotal
        {
            get { return queryTotal; }
        }

        // Common strip routine
        private string strip(string text, bool keepCRLF=false)
        {
            string result = text.Trim( charsToTrim );
            result = result.Replace( "\\r\\n", Environment.NewLine ).Replace( "\r\n", Environment.NewLine );
            result = result.Replace( "\\n\\r", Environment.NewLine ).Replace( "\n\r", Environment.NewLine );
            result = result.Replace( "\\n ", Environment.NewLine ).Replace( "\n ", Environment.NewLine );
            result = result.Replace( "\\r ", Environment.NewLine ).Replace( "\r ", Environment.NewLine );
            result = result.Replace( "\\n", Environment.NewLine ).Replace( "\n", Environment.NewLine );
            result = result.Replace( "\\r", Environment.NewLine ).Replace( "\r", Environment.NewLine );
            result = result.Replace( "\\r\\n\\r\\n", Environment.NewLine ).Replace( "\r\n\r\n", Environment.NewLine );
            result = result.Replace( Environment.NewLine, "\r" );
            if ( !keepCRLF )
            {
                result = result.Replace( "\\n", "" ).Replace( "\n", "" );
                result = result.Replace( "\\r", "" ).Replace( "\r", "" );
            }
            return ( result );
        }
        
        // Get Detail infomation
        public string[] getDetail( int iID )
        {

            List<string> sDetail = new List<string>();
            string sTitle ="", sAlbum = "";
            List<string> sAlias = new List<string>();
            List<string> sArtist = new List<string>();
            string sContent;

            HttpRequest hr = new HttpRequest();

            sContent = hr.getContent( $"http://music.163.com/api/song/detail/?id={iID}&ids=[{iID}]" );

            if ( sContent.Substring( 0, 4 ).Equals( "ERR!" ) )
            {
                sDetail.Add( "Get title failed! \r\n EER: \r\n" + sContent.Substring( 4 ) );
                return sDetail.ToArray();
            }

            JObject o = (JObject)JsonConvert.DeserializeObject(sContent);
            if(o["songs"].HasValues)
            {
                sTitle = strip( o["songs"][0]["name"].ToString() );

                foreach ( string alias in o["songs"][0]["alias"] )
                {
                    sAlias.Add( strip( alias.ToString() ) );
                }

                foreach ( JObject artist in o["songs"][0]["artists"] )
                {
                    sArtist.Add( strip( artist["name"].ToString() ) );
                }

                sAlbum = strip( o["songs"][0]["album"]["name"].ToString() );

                sDetail.Add( sTitle );
                sDetail.Add( string.Join( " ; ", sAlias.ToArray() ) );
                sDetail.Add( string.Join( " ; ", sArtist.ToArray() ) );
                sDetail.Add( sAlbum );
            }
            return sDetail.ToArray();
        }

        // Get Lyric for multi-langiages 
        public string[] getLyric(int iID) {

            List<string> sLRC = new List<string>();
            string sContent;

            HttpRequest hr = new HttpRequest();

            sContent = hr.getContent("http://music.163.com/api/song/media?id=" + iID);

            if ( sContent.Substring( 0, 4 ).Equals( "ERR!" ) )
            {
                sLRC.Add( "Get lyric failed! \r\n EER: \r\n" + sContent.Substring( 4 ) );
                return sLRC.ToArray();
            }

            JObject o = (JObject)JsonConvert.DeserializeObject(sContent);
            sLRC.Add( strip( o["lyric"].ToString(), true ) );

            return sLRC.ToArray();
        }

        //public List<string> getLyricNew(int iID)
        public string[] getLyricMultiLang( int iID )
        {
            List<string> sLRC = new List<string>();
            string sContent;

            HttpRequest hr = new HttpRequest();
            
            sContent = hr.getContent( "http://music.163.com/api/song/lyric?os=pc&lv=-1&kv=-1&tv=-1&id=" + iID );

            if ( sContent.Substring( 0, 4 ).Equals( "ERR!" ) )
            {
                sLRC.Add( "Get lyric failed! \r\n EER: \r\n" + sContent.Substring( 4 ) );
                return sLRC.ToArray();
            }

            JObject o = (JObject)JsonConvert.DeserializeObject(sContent);

            if ( ( o.Property( "uncollected" ) != null && (bool) o["uncollected"] ) ||
                 ( o.Property( "nolyric" ) != null && (bool) o["nolyric"] ) )
            {
                sLRC.Add( "No Lyric Found!" );
                return ( sLRC.ToArray() );
            }

            // Original Language Lyric
            if ( o["lrc"]["lyric"] != null )
            {
                string lyric = strip( o["lrc"]["lyric"].ToString(), true );
                if ( lyric.Length > 0 )
                {
                    sLRC.Add( lyric );
                }
            }
            // Translated Lyric
            if ( o["tlyric"]["lyric"] != null )
            {
                string tlyric = strip( o["tlyric"]["lyric"].ToString(), true );
                if ( tlyric.Length > 0 )
                {
                    sLRC.Add( tlyric );
                }
            }
            // KaraOk Lyric ?
            if ( o["klyric"]["lyric"] != null )
            {
                string klyric = strip( o["klyric"]["lyric"].ToString(), true );
                if ( klyric.Length > 0 )
                {
                    //sLRC.Add( klyric );
                }
            }

            if ( sLRC.Count <= 0 )
            {
                sLRC.Add( "No Lyric Found!" );
            }
            return sLRC.ToArray();
        }

        // search music by title
        public MusicItem[] getMusicByTitle(string query, int offset=0, int limit=100, int type=1)
        {
            List<MusicItem> sMusic = new List<MusicItem>();
            string sContent;
            
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            postParams.Add( new KeyValuePair<string, string>( "offset", $"{offset}" ) );
            postParams.Add( new KeyValuePair<string, string>( "limit", $"{limit}" ) );
            ///postParams.Add( new KeyValuePair<string, string>( "type", $"{type}" ) );
            postParams.Add( new KeyValuePair<string, string>( "type", "1" ) );
            postParams.Add( new KeyValuePair<string, string>( "s", Uri.EscapeDataString( query ).Replace( "%20", "+" ) ) );

            HttpRequest hr = new HttpRequest();
            sContent = hr.postContent( "http://music.163.com/api/search/pc" , postParams );

            if ( sContent.Substring( 0, 4 ).Equals( "ERR!" ) )
            {
                //sMusic.Add( "Search Music failed! \r\n EER: \r\n" + sContent.Substring( 4 ) );
                return sMusic.ToArray();
            }

            JObject o = (JObject)JsonConvert.DeserializeObject(sContent);
            if ( (int) o["code"] == 200 )
            {
                queryTotal = (int) o["result"]["songCount"];
                queryOffset = offset;
                queryCount = 0;

                if ( o["result"]["songs"] != null )
                {
                    foreach ( JObject m in o["result"]["songs"] )
                    {
                        MusicItem mItem = new MusicItem();

                        mItem.title = m["name"].ToString();
                        if ( m["alias"] != null )
                        {
                            List<string> aliasList = new List<string>();
                            foreach ( JValue alias in m["alias"] )
                            {
                                aliasList.Add( alias.ToString() );
                            }
                            mItem.title_alias = string.Join( " ; ", aliasList.ToArray() );
                        }
                        mItem.id = m["id"].ToString();
                        List<string> arts = new List<string>();
                        List<string> photos = new List<string>();
                        foreach ( JObject art in m["artists"] )
                        {
                            arts.Add( art["name"].ToString() );
                            photos.Add( art["picUrl"].ToString() );
                        }
                        mItem.artist = string.Join( " ; ", arts.ToArray() );
                        mItem.picture = string.Join( " ; ", photos.ToArray() );
                        //mItem.picture = m["album"]["artist"]["picUrl"].ToString();
                        mItem.album = m["album"]["name"].ToString();
                        if ( m["album"]["alias"] != null)
                        {
                            List<string> aliasList = new List<string>();
                            foreach( JValue alias in m["album"]["alias"] )
                            {
                                aliasList.Add( alias.ToString() );
                            }
                            mItem.album_alias = string.Join( " ; ", aliasList.ToArray() );
                        }
                        mItem.cover = m["album"]["picUrl"].ToString();
                        mItem.company = m["album"]["company"].ToString();

                        sMusic.Add( mItem );

                    }
                }
            }
            queryCount += sMusic.Count;
            o.RemoveAll();
            return sMusic.ToArray();
        }
    }
}
