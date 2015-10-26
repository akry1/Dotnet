using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Configuration;
using System.Threading;

namespace MovieProphecy
{
    public class RottenTomato
    {
        string url1 = "http://api.rottentomatoes.com/api/public/v1.0/lists/movies/upcoming.json?page_limit=5&page=1&country=us&apikey=" + ConfigurationManager.AppSettings["ApiKey"];
        string url2 = "http://api.rottentomatoes.com/api/public/v1.0/lists/movies/in_theaters.json?page_limit=5&page=1&country=us&apikey=" + ConfigurationManager.AppSettings["ApiKey"];

        public string CallApi(string url)
        {
            Uri address = new Uri(url);
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
            request.Method = "GET";
            request.ContentType = "application/json";
            request.KeepAlive = true;
            request.Timeout = Timeout.Infinite;
            try
            {
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    // Get the response stream
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    // Console application output
                    return reader.ReadToEnd();
                }
            }
            catch(Exception e)
            {
                throw;
            }
        }

        public List<string> FindMoviesInTheaterList()
        {
            try
            {
                string jsonResponse = CallApi(url2);
                List<string> results = ParseMovieSearchResults(jsonResponse);
                return results;
            }
            catch(Exception e)
            {
                throw ;
            }
        }

        public List<string> FindOpeningMoviesList()
        {
            try
            {
                string jsonResponse = CallApi(url1);
                List<string> results = ParseMovieSearchResults(jsonResponse);
                return results;
            }
            catch(Exception e)
            {
                throw;
            }
        }

        public static List<string> ParseMovieSearchResults(string json)
        {
            try
            {
                JObject jObject = JObject.Parse(json);
                //var result = new MovieSearchResults();
                List<string> movieList = new List<string>();
                if (jObject["total"] != null)
                    movieList.Capacity = (int)jObject["total"];
                var movies = (JArray)jObject["movies"];
                if (movies != null)
                {
                    foreach (var movie in movies)
                    {
                        movieList.Add(ParseMovie(movie.ToString()));
                    }
                }
                return movieList;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static String ParseMovie(string json)
        {
            try
            {
                JObject jObject = JObject.Parse(json);
                //Movie movie = new Movie();
                return ParseTitle(jObject["title"]);
                //return movie;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static string ParseTitle(JToken jToken)
        {
            try
            {
                return jToken.Value<string>();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}