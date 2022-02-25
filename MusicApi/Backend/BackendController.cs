using MusicApi.Backend.Music;
using MusicApi.Backend.SourceApi;
using MusicApi.Backend.SourceApi.Database;
using MusicApi.Backend.SourceApi.Spotify;
using MusicApi.Model;
using System.Collections.Generic;

namespace MusicApi.Backend
{
    public static class BackendController
    {
        private static IApi spotify = new SpotifyApi();
        private static IDBConnection db = new DBApi();
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
                return tracks.Remove(id);
            }
            return false;
        }

        public static IPlaylist GetPlaylist(string id)
        {
            if (!playlists.ContainsKey(id))
                return null;
            return playlists[id];
        }

        public static IPlaylist BuildPlaylist(string name)
        {
            var playlist = ModelFactory.BuildPlaylist(name);
            playlists.Add(playlist.ID, playlist);
            return playlist;
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

        public static bool RemoveAlbum(string id)
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

        public static bool DonwloadAlbum(string id)
        {
            if (albums.ContainsKey(id))
            {
                return false;
            }
            var album = spotify.GetAlbum(id);
            if (album == null)
            {
                return false;
            }
            albums.Add(album.ID, album);
            db.WriteAlbum(album);
            return true;
        }
    }
}
