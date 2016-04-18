using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace _163lyric
{
    class NetEaseLyric
    {
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

            //反序列化JSON数据  
            char[] charsToTrim = { '*', ' ', '\'', '\"', '\r', '\n' };

            JObject o = (JObject)JsonConvert.DeserializeObject(sContent);
            if(o["songs"].HasValues)
            {
                sTitle = o["songs"][0]["name"].ToString().Trim( charsToTrim ).Replace( "\\n", "" ).Replace( "\n", "" ).Replace( "\\r", "" ).Replace( "\r", "" );

                foreach ( string alias in o["songs"][0]["alias"] )
                {
                    sAlias.Add( alias.ToString().Trim( charsToTrim ).Replace( "\\n", "" ).Replace( "\n", "" ).Replace( "\\r", "" ).Replace( "\r", "" ) );
                }
                //sAlias = o["songs"][0]["alias"].ToString().Trim( charsToTrim ).Replace( "\\n", "" ).Replace( "\n", "" ).Replace( "\\r", "" ).Replace( "\r", "" );
                foreach ( JObject artist in o["songs"][0]["artists"] )
                {
                    sArtist.Add( artist["name"].ToString().Trim( charsToTrim ).Replace( "\\n", "" ).Replace( "\n", "" ).Replace( "\\r", "" ).Replace( "\r", "" ) );
                }

                sAlbum = o["songs"][0]["album"]["name"].ToString().Trim( charsToTrim ).Replace( "\\n", "" ).Replace( "\n", "" ).Replace( "\\r", "" ).Replace( "\r", "" );

                sDetail.Add( sTitle );
                sDetail.Add( String.Join( " ; ", sAlias.ToArray() ) );
                sDetail.Add( String.Join( " ; ", sArtist.ToArray() ) );
                sDetail.Add( sAlbum );
            }
            return sDetail.ToArray();
        }

        public string getLyric(int iID) {

            string sLRC="";
            string sContent;

            HttpRequest hr = new HttpRequest();

            sContent = hr.getContent("http://music.163.com/api/song/media?id=" + iID);

            if ( sContent.Substring( 0, 4 ).Equals( "ERR!" ) ) return "Get lyric failed! \r\n EER: \r\n" + sContent.Substring( 4 );

            //反序列化JSON数据  
            char[] charsToTrim = { '*', ' ', '\'', '\"'};

            JObject o = (JObject)JsonConvert.DeserializeObject(sContent);
            sLRC = o["lyric"].ToString().Trim( charsToTrim ).Replace( "\\n", Environment.NewLine ).Replace( "\n", Environment.NewLine );

            return sLRC;
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

            //反序列化JSON数据  
            char[] charsToTrim = { '*', ' ', '\'', '\"'};

            JObject o = (JObject)JsonConvert.DeserializeObject(sContent);

            JObject value = new JObject();

            if ( (o.Property( "uncollected" ) != null) && o["uncollected"].ToString() == "True")
            {
                sLRC.Add( "No Lyric Found!" );
                return ( sLRC.ToArray() );
            }
            string lyric = o["lrc"]["lyric"].ToString().Trim( charsToTrim ).Replace( "\\n", Environment.NewLine ).Replace( "\n", Environment.NewLine );
            if ( lyric.Length > 0 )
            {
                sLRC.Add( lyric );
            }
            string tlyric = o["tlyric"]["lyric"].ToString().Trim( charsToTrim ).Replace( "\\n", Environment.NewLine ).Replace( "\n", Environment.NewLine );
            if ( tlyric.Length > 0 )
            {
                sLRC.Add(tlyric);
            }

            return sLRC.ToArray();
        }
    }
}
