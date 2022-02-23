using MusicApi.Backend.Music;
using MusicApi.Backend.SourceApi.Json;
using MusicApi.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MusicApi.Backend.SourceApi
{
    public class SpotifyApi
    {
        private HttpWebRequest webRequest;

        public ITrack GetTrack(string id)
        {
            string endpoint = SpotifyEndpoints.GET_TRACK;
            endpoint = endpoint.Replace("{id}", id);
            string response = Get(endpoint); 
            if (response == null)
            {
                return null;
            }
            var trackJson = JsonConvert.DeserializeObject<GetTrackJson>(response);
            return BuildTrack(trackJson);
        }

        private string Get(string endpoint)
        {
            webRequest = (HttpWebRequest)HttpWebRequest.Create(endpoint);
            webRequest.Headers.Add("Authorization: Bearer BQBHix0B8wrBhV-bCFmXUrYjijSD85ycT11rvCoywhiqY5-dBMV-JPLMp5CfPnhqSywuijvtMOYnCqcI23A5AXCOpqgxNlX2iS-D2PObZG4mBYRkcW1WKJDAnywVmyezAoBUWPn8GEoABIHx448H4aLqONInMd-Tnj1fjqdTGItfHc7w");

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

        private ITrack BuildTrack(GetTrackJson trackJson)
        {
            if (trackJson == null)
            {
                return null;
            }

            string id = trackJson.id;
            string title = trackJson.name;
            IList<string> images = null;
            string albumID = null;
            if (trackJson.album != null)
            {
                albumID = trackJson.album.id;
                images = new List<string>();
                foreach (var each in trackJson.album.images)
                {
                    images.Add(each.url);
                }
            }
            int duration = trackJson.duration_ms;
            IList<string> artists = new List<string>();
            foreach (var each in trackJson.artists)
            {
                artists.Add(each.name);
            }

            return ModelFactory.BuildTrack(id, title, images, duration, artists, albumID);
        }
    }
}
