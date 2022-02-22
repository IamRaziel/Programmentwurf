using System;
using System.IO;
using System.Net;
using System.Text;

namespace MusicApi.Backend.SourceApi
{
    public class SpotifyApi
    {
        private HttpWebRequest webRequest;

        public string GetTrack(string id)
        {
            string endpoint = SpotifyEndpoints.GET_TRACK;
            endpoint = endpoint.Replace("{id}", id);
            return Get(endpoint);
        }

        private string Get(string endpoint)
        {
            webRequest = (HttpWebRequest)HttpWebRequest.Create(endpoint);
            webRequest.Headers.Add("Authorization: Bearer BQDpx3f0KggFvn5nufrfCRwlOZb0cCkQ8X5ANXtIqFZGrqQ3B56gvghdeitXJxkLkSMwKW9pKMzZ5F1r-g0vHOTHt0HzDqNW_pO9iwKfw8zuN-YIYNmhMFkbqsMaBkdpdqdrkwTpCzrBTBKoL6HkBjeK-3vqo_t8iC-HIBHJ842eUtrN");

            Console.WriteLine(webRequest.Connection);

            string content = null;
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponseAsync().Result)
                {
                    var stream = response.GetResponseStream();
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        content = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return content;
        }
    }
}
