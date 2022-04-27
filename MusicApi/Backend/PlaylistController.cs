using System;
using System.Collections.Generic;
using System.Linq;
using MusicApi.Backend.Music;
using MusicApi.Backend.SourceApi.Database;
using MusicApi.Model;

namespace MusicApi.Backend
{
    public class PlaylistController
    {
        private IDictionary<string, IPlaylist> playlists = new Dictionary<string, IPlaylist>();
        private IDBConnection db;

        public PlaylistController(IDBConnection dbConnection)
        {
            db = dbConnection;
        }

        public IList<IPlaylist> Playlists { get { return playlists.Values.ToList(); } }

        public void AddPlaylists(IList<IPlaylist> playlists)
        {
            if (playlists != null)
            {
                foreach (var each in playlists)
                {
                    if (each != null)
                    {
                        this.playlists.Add(each.ID, each);
                    }
                }
            }
        }

        public bool AddTrackToPlaylist(string playlistID, string trackID)
        {
            var track = BackendController.TrackController.GetTrackFromID(trackID);
            if (!playlists.ContainsKey(playlistID) || track == null)
            {
                return false;
            }
            playlists[playlistID].Tracks.Add(track);
            return true;
        }

        public IPlaylist BuildPlaylist(string name)
        {
            var playlist = ModelFactory.BuildPlaylist(name);
            playlists.Add(playlist.ID, playlist);
            return playlist;
        }

        public bool DeletePlaylist(string id)
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

        public IPlaylist GetPlaylistFromID(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            IPlaylist playlist = null;
            if (!playlists.TryGetValue(id, out playlist)) return null;
            return playlist;
        }

        //Needs to be changed to GetPlaylistFromUrl(string url)
        public IPlaylist GetPlaylistFromUrl(string id)
        {
            //TODO
            if (playlists.ContainsKey(id))
            {
                return playlists[id];
            }
            var api = BackendController.GetApiFromUrl("");
            var playlist = api.GetPlaylist(id);
            if (playlist != null)
            {
                playlists.Add(playlist.ID, playlist);
            }
            return playlist;
        }

        public IList<IPlaylist> GetPlaylistsFromID(IList<string> ids)
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

        public IList<ITrack> GetTracksOfPlaylist(string id)
        {
            return playlists.ContainsKey(id) ? playlists[id].Tracks : null;
        }

        public void LoadPlaylistsFromDB()
        {
            if (db != null)
            {
                AddPlaylists(db.GetPlaylists());
            }
        }

        public bool RemoveTrackFromPlaylist(string playlistID, string trackID)
        {
            var track = BackendController.TrackController.GetTrackFromID(trackID);
            if (!playlists.ContainsKey(playlistID) || track == null)
            {
                return false;
            }
            playlists[playlistID].Tracks.Remove(track);
            return true;
        }

        public bool UpdatePlaylist(string id, string name, string image)
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
    }
}
