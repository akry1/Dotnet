using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Configuration;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;

namespace MovieProphecy
{
    public class CallJava
    {
        public string CallApi(String movieName)
        {
            try
            {
                Uri address = new Uri("http://localhost:8080/MovieProphecy/webresources/mp?name=" + movieName);
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
                request.Method = "GET";
                request.ContentType = "application/text";
                request.KeepAlive = true;
                request.Timeout = Timeout.Infinite;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    // Get the response stream
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    // Console application output
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return "-1";
            }
        }
    }
}