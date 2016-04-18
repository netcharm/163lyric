using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace _163lyric
{
    class HttpRequest
    {
        public string getContent(string sURL) {

            string sContent = ""; //Content
            string sLine = "";

            try
            {
                //WebRequest wrGETURL = WebRequest.Create(sURL);
                HttpWebRequest wrGETURL = (HttpWebRequest)WebRequest.Create(sURL);
                //wrGETURL.Headers.Add( HttpRequestHeader.Referer );
                wrGETURL.Referer = "http://music.163.com/";

                Stream objStream = wrGETURL.GetResponse().GetResponseStream();
                StreamReader objReader = new StreamReader(objStream);
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        sContent += sLine;
                }
            }

            catch (Exception e) {
                sContent = "ERR!"+e.ToString();
            }
                     
            return sContent;
        }
    }
}
