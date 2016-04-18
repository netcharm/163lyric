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
                HttpWebRequest wrGETURL = (HttpWebRequest)WebRequest.Create(sURL);
                // Add Referer for API call
                wrGETURL.Referer = "http://music.163.com/";
                // Add Cookie value for API call
                wrGETURL.CookieContainer = new CookieContainer();
                wrGETURL.CookieContainer.Add(new Uri( "http://music.163.com/" ), new Cookie( "appver", "1.5.0.75771" ) );

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
