using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace _163lyric
{
    class NetEaseLyric
    {
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
            sLRC.Add( o["lrc"]["lyric"].ToString().Trim( charsToTrim ).Replace( "\\n", Environment.NewLine ).Replace( "\n", Environment.NewLine ) );
            sLRC.Add( o["tlyric"]["lyric"].ToString().Trim( charsToTrim ).Replace( "\\n", Environment.NewLine ).Replace( "\n", Environment.NewLine ) );


            return sLRC.ToArray();
        }
    }
}
