using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace _163music
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

                WebResponse objResponse = wrGETURL.GetResponse();
                Stream objStream = objResponse.GetResponseStream();
                StreamReader objReader = new StreamReader(objStream);
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        sContent += sLine;
                }
                objReader.Close();
                objStream.Close();
                objResponse.Close();
            }

            catch (Exception e) {
                sContent = "ERR!"+e.ToString();
            }
                     
            return sContent;
        }

        public string postContent( string sURL, List<KeyValuePair<string, string>> kwargs )
        {
            string sContent = ""; //Content
            string sLine = "";

            try
            {
                List<string> postData = new List<string>();
                foreach ( KeyValuePair<string, string> kvp in kwargs )
                {
                    postData.Add( $"{kvp.Key}={kvp.Value}" );
                }
                ASCIIEncoding encoding = new ASCIIEncoding (); // UTF8Encoding
                byte[] byteData = encoding.GetBytes (string.Join("&", postData.ToArray()));


                HttpWebRequest wrGETURL = (HttpWebRequest)WebRequest.Create(sURL);
                // Add Referer for API call
                wrGETURL.Referer = "http://music.163.com/";
                // Add Cookie value for API call
                wrGETURL.CookieContainer = new CookieContainer();
                wrGETURL.CookieContainer.Add( new Uri( "http://music.163.com/" ), new Cookie( "appver", "1.5.0.75771" ) );
                wrGETURL.Method = "POST";
                wrGETURL.ContentType = "application/x-www-form-urlencoded";
                wrGETURL.ContentLength = byteData.Length;

                Stream dataStream = wrGETURL.GetRequestStream();
                dataStream.Write( byteData, 0, byteData.Length );
                dataStream.Close();

                WebResponse objResponse = wrGETURL.GetResponse();
                Stream objStream = objResponse.GetResponseStream();
                StreamReader objReader = new StreamReader(objStream);
                while ( sLine != null )
                {
                    sLine = objReader.ReadLine();
                    if ( sLine != null )
                        sContent += sLine;
                }
                objReader.Close();
                objStream.Close();
                objResponse.Close();
            }

            catch ( Exception e )
            {
                sContent = "ERR!" + e.ToString();
            }

            return ( sContent );
        }
    }
}
