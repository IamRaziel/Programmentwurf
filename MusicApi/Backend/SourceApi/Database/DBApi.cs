using MusicApi.Backend.Music;
using MusicApi.Model;
using System.Collections.Generic;

namespace MusicApi.Backend.SourceApi.Database
{
    //
    //DB CONNECTION IS NOT PART OF THIS PROJECT
    //
    public class DBApi : IDBConnection
    {
        private IDictionary<string, ITrack> tracks;
        private IDictionary<string, IAlbum> albums;
        private IDictionary<string, IPlaylist> playlists;
     
        public DBApi()
        {
            BuildAlbums();
            BuildTracks();
            BuildPlaylists();
        }

        public IAlbum GetAlbum(string id)
        {
            return albums.ContainsKey(id) ? albums[id] : null;
        }

        public bool WriteAlbum(IAlbum album)
        {
            if (albums.ContainsKey(album.ID))
            {
                return false;
            }
            albums.Add(album.ID, album);
            return true;
        }

        public bool UpdateAlbum(IAlbum album)
        {
            if (albums.ContainsKey(album.ID))
            {
                albums[album.ID] = album;
                return true;
            }
            return false;
        }

        public bool RemoveAlbum(string id)
        {
            return albums.ContainsKey(id) ? albums.Remove(id) : false;
        }

        public ITrack GetTrack(string id)
        {
            return tracks.ContainsKey(id) ? tracks[id] : null;
        }

        public bool WriteTrack(ITrack track)
        {
            if (tracks.ContainsKey(track.ID))
            {
                return false;
            }
            tracks.Add(track.ID, track);
            return true;
        }

        public bool UpdateTrack(ITrack track)
        {
            if (tracks.ContainsKey(track.ID))
            {
                tracks[track.ID] = track;
                return true;
            }
            return false;
        }

        public bool RemoveTrack(string id)
        {
            return tracks.ContainsKey(id) ? tracks.Remove(id) : false;
        }

        public IPlaylist GetPlaylist(string id)
        {
            return playlists.ContainsKey(id) ? playlists[id] : null;
        }

        public bool WritePlaylist(IPlaylist playlist)
        {
            if (playlists.ContainsKey(playlist.ID))
            {
                return false;
            }
            playlists.Add(playlist.ID, playlist);
            return true;
        }

        public bool UpdatePlaylist(IPlaylist playlist)
        {
            if (playlists.ContainsKey(playlist.ID))
            {
                playlists[playlist.ID] = playlist;
                return true;
            }
            return false;
        }

        public bool RemovePlaylist(string id)
        {
            return playlists.ContainsKey(id) ? playlists.Remove(id) : false;
        }

        private void BuildTracks()
        {
            ITrack CUT_TO_THE_FEELING = ModelFactory.BuildTrack("11dFghVXANMlKmJXsNCbNl", "Cut To The Feeling", null, 0, null, null);
            ITrack d;

            tracks = new Dictionary<string, ITrack>(
                new KeyValuePair<string, ITrack>[]
                {
                    new KeyValuePair<string, ITrack>(CUT_TO_THE_FEELING.ID, CUT_TO_THE_FEELING)
                }
            );
        }

        private void BuildAlbums()
        {
            albums = new Dictionary<string, IAlbum>();

        }

        private void BuildPlaylists()
        {
            playlists = new Dictionary<string, IPlaylist>();
        }
    }
}
