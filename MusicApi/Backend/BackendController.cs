using Microsoft.AspNetCore.Http;
using MusicApi.Backend.Music;
using MusicApi.Backend.SourceApi;
using MusicApi.Backend.SourceApi.Database;
using MusicApi.Backend.SourceApi.Spotify;
using MusicApi.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace MusicApi.Backend
{
    public static class BackendController
    {
        private static IApi spotify = new SpotifyApi();
        private static DBApi dbApi = new DBApi("Q:/Dateien/Music/");
        private static IDBConnection db = dbApi;
        private static IFileWriter fileWriter = dbApi;
        private static IDictionary<string/*ID*/, IPlaylist> playlists = new Dictionary<string, IPlaylist>(); 
        private static IDictionary<string/*ID*/, ITrack> tracks = new Dictionary<string, ITrack>();
        private static IDictionary<string/*ID*/, IAlbum> albums = new Dictionary<string, IAlbum>();

        public static ITrack GetTrack(string id)
        {
            if (tracks.ContainsKey(id))
            {
                return tracks[id];
            }
            var track = spotify.GetTrack(id);
            if (track != null)
            {
                tracks.Add(track.ID, track);
            }
            return track;
        }

        public static bool DeleteTrack(string id)
        {
            if (tracks.ContainsKey(id))
            {
                var track = tracks[id];
                if (tracks.Remove(id))
                {
                    if (db.RemoveTrack(id))
                    {
                        return true;
                    }
                    tracks.Add(id, track);
                }
            }
            return false;
        }

        public static bool UploadTrack(IFormFile file)
        {
            try
            {
                string name = file.FileName.Replace(@"\\\\", @"\\");

                if (file.Length > 0)
                {
                    var memoryStream = new MemoryStream();

                    try
                    {
                        file.CopyTo(memoryStream);

                        // Upload check if less than 2mb!
                        if (memoryStream.Length < 2097152)
                        {
                            fileWriter.Write(memoryStream.ToArray(), Path.GetFileName(name));
                        }
                    }
                    finally
                    {
                        memoryStream.Close();
                        memoryStream.Dispose();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }

        public static IPlaylist GetPlaylist(string id)
        {
            if (playlists.ContainsKey(id))
            {
                return playlists[id];
            }
            var playlist = spotify.GetPlaylist(id);
            if (playlist != null)
            {
                playlists.Add(playlist.ID, playlist);
            }
            return playlist;
        }

        public static bool DeletePlaylist(string id)
        {
            if (playlists.ContainsKey(id))
            {
                var playlist = playlists[id];
                if (playlists.Remove(id))
                {
                    if (db.RemovePlaylist(id))
                    {
                        return true;
                    }
                    playlists.Add(id, playlist);
                }
            }
            return false;
        }

        public static IPlaylist BuildPlaylist(string name)
        {
            var playlist = ModelFactory.BuildPlaylist(name);
            playlists.Add(playlist.ID, playlist);
            return playlist;
        }


        public static bool UpdatePlaylist(string id, string name, string image)
        {
            if (!playlists.ContainsKey(id))
            {
                return false;
            }
            var playlist = playlists[id];
            playlist.Name = name;
            playlist.Image = image;
            return true;
        }

        public static IList<ITrack> GetTracksOfPlaylist(string id)
        {
            return playlists.ContainsKey(id) ? playlists[id].Tracks : null;
        }

        public static bool AddTrackToPlaylist(string playlistID, string trackID)
        {
            if (!playlists.ContainsKey(playlistID) || !tracks.ContainsKey(trackID))
            {
                return false;
            }
            playlists[playlistID].Tracks.Add(tracks[trackID]);
            return true;
        }

        public static bool RemoveTrackFromPlaylist(string playlistID, string trackID)
        {
            if (!playlists.ContainsKey(playlistID) || !tracks.ContainsKey(trackID))
            {
                return false;
            }
            playlists[playlistID].Tracks.Remove(tracks[trackID]);
            return true;
        }

        public static IAlbum GetAlbum(string id)
        {
            if (albums.ContainsKey(id))
            {
                return albums[id];
            }
            var album = spotify.GetAlbum(id);
            if (album != null)
            {
                albums.Add(album.ID, album);
            }
            return album;
        }

        public static bool DeleteAlbum(string id)
        {
            if (albums.ContainsKey(id))
            {
                var album = albums[id];
                if (albums.Remove(id))
                {
                    if (db.RemoveAlbum(id))
                    {
                        return true;
                    }
                    albums.Add(id, album);
                }
            }
            return false;
        }

        public static bool Download(string url)
        {
            

            return true;
        }

        public static bool play()
        {
            string url = "";

            var player = new MusicPlayer();
            player.PlayMp3FromUrl(url);

            return true;
        }
    }
}
