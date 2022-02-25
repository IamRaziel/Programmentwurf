﻿using MusicApi.Backend.Music;
using MusicApi.Backend.SourceApi.Spotify.Json;
using MusicApi.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace MusicApi.Backend.SourceApi.Spotify
{
    public class SpotifyApi : IApi
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

        public IPlaylist GetPlaylist(string id)
        {
            //TODO
            return null;
        }

        public IAlbum GetAlbum(string id)
        {
            string endpoint = SpotifyEndpoints.GET_ALBUM;
            endpoint = endpoint.Replace("{id}", id);
            string response = Get(endpoint);
            if (response == null)
            {
                return null;
            }
            var albumTracksJson = JsonConvert.DeserializeObject<GetAlbumTracksJson>(response);
            return BuildAlbum(albumTracksJson);
        }

        private string Get(string endpoint)
        {
            webRequest = (HttpWebRequest)HttpWebRequest.Create(endpoint);
            webRequest.Headers.Add("Authorization: Bearer BQAM_zYMUea6qeuJYzbqChiZ021HY0c58G92NcTHv2Y767W_RQfDjOU8EovU3n5lHN6xKhLlhgsOIm2Qdw7rIwd4Og178yojfsmziq1e73QGO2JAflVeaTHVjlhwdSN62xP4k8HxLTMV75Bzl9hzqgVNrQI2XIinUggfuSzVhWj0hQNG");

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
            string image = null;
            string albumID = null;
            if (trackJson.album != null)
            {
                int i = trackJson.album.images.Length - 1;
                image = trackJson.album.images[i].url;
                albumID = trackJson.album.id;
            }
            int duration = trackJson.duration_ms;
            IList<string> artists = new List<string>();
            foreach (var each in trackJson.artists)
            {
                artists.Add(each.name);
            }

            return ModelFactory.BuildTrack(id, title, image, duration, artists, albumID);
        }

        private IAlbum BuildAlbum(GetAlbumTracksJson albumTracksJson)
        {
            if (albumTracksJson == null)
            {
                return null;
            }          

            string image = null;

            if (albumTracksJson.images != null && albumTracksJson.images.Length > 0)
            {
                int i = albumTracksJson.images.Length - 1;
                image = albumTracksJson.images[i].url;
            }

            string id = albumTracksJson.id;
            string name = albumTracksJson.name;
            IList<ITrack> tracks = BuildTracksFromAlbum(albumTracksJson.tracks, image, id);
            IList<string> genres = BuildGenres(albumTracksJson.genres);
            IList<string> artists = BuildArtists(albumTracksJson.artists);

            return ModelFactory.BuildAlbum(id, name, image, tracks, genres, artists);
        }

        private IList<ITrack> BuildTracksFromAlbum(TracksJson tracksJson, string image, string albumID)
        {
            IList <ITrack> tracks = new List<ITrack>();
            if (tracksJson != null)
            {

                var items = tracksJson.items;
                if (items != null && items.Length > 0)
                {
                    foreach (var each in items)
                    {
                        string id = each.id;
                        string title = each.name;
                        int duration = each.duration_ms;
                        IList<string> artists = BuildArtists(each.artists);

                        var track = ModelFactory.BuildTrack(id, title, image, duration, artists, albumID);
                        tracks.Add(track);
                    }
                }
            }
            return tracks;
        }

        private IList<string> BuildGenres(object[] genreJsons)
        {
            IList<string> genres = new List<string>();
            if (genreJsons != null && genreJsons.Length > 0)
            {
                //TODO
                //foreach (var each in genreJsons)
                //{
                //
                //}
            }
            return genres;
        }

        private IList<string> BuildArtists(ArtistJson[] artistJsons)
        {
            IList<string> artists = new List<string>();
            if (artistJsons != null && artistJsons.Length > 0)
            {
                foreach (var each in artistJsons)
                {
                    artists.Add(each.name);
                }
            }
            return artists;
        }
    }
}
