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
        public string name = "";
        public string id = "";
        public string artist = "";
        public string picture = "";
        public string album = "";
        public string cover = "";
        public string company = "";
    }

    class NetEaseMusic
    {
        //反序列化JSON数据  
        char[] charsToTrim = { '*', ' ', '\'', '\"', '\r', '\n' };

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

            if ( o.Property( "uncollected" ) != null && o["uncollected"].ToString() == "True")
            {
                sLRC.Add( "No Lyric Found!" );
                return ( sLRC.ToArray() );
            }

            string lyric = strip( o["lrc"]["lyric"].ToString(), true );
            if ( lyric.Length > 0 )
            {
                sLRC.Add( lyric );
            }

            string tlyric = strip( o["tlyric"]["lyric"].ToString(), true );
            if ( tlyric.Length > 0 )
            {
                sLRC.Add(tlyric);
            }

            if ( sLRC.Count <= 0 )
            {
                sLRC.Add( "No Lyric Found!" );
            }
            return sLRC.ToArray();
        }

        // search music by title
        public MusicItem[] getMusicByTitle(string title)
        {
            List<MusicItem> sMusic = new List<MusicItem>();
            string sContent;

            
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            postParams.Add( new KeyValuePair<string, string>( "offset", "0" ) );
            postParams.Add( new KeyValuePair<string, string>( "limit", "100" ) );
            postParams.Add( new KeyValuePair<string, string>( "type", "1" ) );
            //postParams.Add( new KeyValuePair<string, string>( "s", Uri.EscapeDataString( Encoding.UTF8.GetString( Encoding.Default.GetBytes( title ) ) ) ) );
            postParams.Add( new KeyValuePair<string, string>( "s", Uri.EscapeDataString( title ).Replace( "%20", "+" ) ) );

            HttpRequest hr = new HttpRequest();
            sContent = hr.postContent( "http://music.163.com/api/search/pc" , postParams );

            if ( sContent.Substring( 0, 4 ).Equals( "ERR!" ) )
            {
                //sMusic.Add( "Search Music failed! \r\n EER: \r\n" + sContent.Substring( 4 ) );
                return sMusic.ToArray();
            }

            JObject o = (JObject)JsonConvert.DeserializeObject(sContent);

            foreach(JObject m in o["result"]["songs"])
            {
                MusicItem mItem = new MusicItem();

                mItem.name = m["name"].ToString();
                mItem.id = m["id"].ToString();
                List<string> arts = new List<string>();
                List<string> photos = new List<string>();
                foreach (JObject art in m["artists"] )
                {
                    arts.Add( art["name"].ToString() );
                    photos.Add( art["picUrl"].ToString() );
                }
                mItem.artist = string.Join( " ; ", arts.ToArray() );
                mItem.picture = string.Join( " ; ", photos.ToArray() );
                //mItem.picture = m["album"]["artist"]["picUrl"].ToString();
                mItem.album = m["album"]["name"].ToString();
                mItem.cover = m["album"]["picUrl"].ToString();
                mItem.company = m["album"]["company"].ToString();

                sMusic.Add( mItem );
            }

            return sMusic.ToArray();
        }
    }
}
