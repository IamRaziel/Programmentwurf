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
        public static readonly string DEFAULT_DIRECTORY = "Q:/Dateien/Music/";

        private static IApi spotify = new SpotifyApi();
        private static DBApi dbApi = new DBApi(DEFAULT_DIRECTORY);
        private static IDBConnection db = dbApi;
        private static IFileWriter fileWriter = dbApi;
        private static IDictionary<string/*ID*/, IPlaylist> playlists = new Dictionary<string, IPlaylist>(); 
        private static IDictionary<string/*ID*/, ITrack> tracks = new Dictionary<string, ITrack>();
        private static IDictionary<string/*ID*/, IAlbum> albums = new Dictionary<string, IAlbum>();
        private static QueueMusicPlayer player = new QueueMusicPlayer();

        public static void LoadMusicFromDB()
        {
            foreach (var each in db.GetAlbums())
            {
                albums.Add(each.ID, each);
            }
            foreach (var each in db.GetTracks())
            {
                tracks.Add(each.ID, each);
            }
            foreach (var each in db.GetPlaylists())
            {
                playlists.Add(each.ID, each);
            }
        }

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
            string url = "Q:/Dateien/Music/Hardstyle/Coone/Less Is More/01 Titelnummer 1.mp3";

            var player = new MusicPlayer();
            player.PlayMp3FromUrl(url);

            return true;
        }

        public static IList<ITrack> GetTracksOfQueue()
        {
            return player.Tracks;
        }

        public static void AddTracksToQueue(IList<string> trackIDs)
        {
            player.AddTracks(GetTracksFromID(trackIDs));
        }

        public static void AddAlbumsToQueue(IList<string> albumIDs)
        {
            
            player.AddAlbums(GetAlbumsFromID(albumIDs));
        }

        public static void AddPlaylistsToQueue(IList<string> playlistIDs)
        {
            
            player.AddPlaylists(GetPlaylistsFromID(playlistIDs));
        }

        public static void RemoveTracksFromQueue(IList<string> trackIDs)
        {
            player.RemoveTracks(GetTracksFromID(trackIDs));
        }

        public static bool MoveTracksInQueue(string trackID, int position)
        {
            return player.MoveTrack(GetTrackFromID(trackID), position);
        }

        public static QueueMusicPlayMode SwitchModeOfMusicPlayer()
        {
            if (player.Mode == QueueMusicPlayMode.RANDOM)
            {
                player.Mode = QueueMusicPlayMode.QUEUE;
            }
            else
            {
                player.Mode = QueueMusicPlayMode.RANDOM;
            }
            return player.Mode;
        }

        public static void NextTrackInQueue()
        {
            player.PlayNext();
        }

        public static void StopMusicPlayer()
        {
            player.Stop();
        }

        public static void ResumeMusicPlayer()
        {
            player.Resume();
        }

        public static IList<IAlbum> GetAlbumsFromID(IList<string> ids)
        {
            IList<IAlbum> albumsFromID = new List<IAlbum>();
            foreach (var each in ids)
            {
                var albumFromID = GetAlbumFromID(each);
                if (albumFromID != null)
                {
                    albumsFromID.Add(albumFromID);
                }
            }
            return albumsFromID;
        }

        public static IList<IPlaylist> GetPlaylistsFromID(IList<string> ids)
        {
            IList<IPlaylist> playlistsFromID = new List<IPlaylist>();
            foreach (var each in ids)
            {
                var playlistFromID = GetPlaylistFromID(each);
                if (playlistFromID != null)
                {
                    playlistsFromID.Add(playlistFromID);
                }
            }
            return playlistsFromID;
        }

        public static IList<ITrack> GetTracksFromID(IList<string> ids)
        {
            IList<ITrack> tracksFromID = new List<ITrack>();
            foreach (var each in ids)
            {
                var trackFromID = GetTrackFromID(each);
                if (trackFromID != null)
                {
                    tracksFromID.Add(trackFromID);
                }
            }
            return tracksFromID;
        }

        public static ITrack GetTrackFromID(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            ITrack track = null;
            if (!tracks.TryGetValue(id, out track)) return null;
            return track;
        }

        public static IPlaylist GetPlaylistFromID(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            IPlaylist playlist = null;
            if (!playlists.TryGetValue(id, out playlist)) return null;
            return playlist;
        }

        public static IAlbum GetAlbumFromID(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            IAlbum album = null;
            if (!albums.TryGetValue(id, out album)) return null;
            return album;
        }
    }
}
