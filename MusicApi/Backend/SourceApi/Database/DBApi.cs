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
        private IDictionary<string, IPlaylist> playlists;
     
        public DBApi()
        {
            BuildPlaylists();
            BuildTracks();
        }

        public IAlbum GetAlbum(string id)
        {
            return null;
        }

        public bool WriteAlbum(IAlbum album)
        {
            return false;
        }

        public bool UpdateAlbum(IAlbum album)
        {
            return false;
        }

        public bool RemoveAlbum(IAlbum album)
        {
            return false;
        }

        public ITrack GetTrack(string id)
        {
            return null;
        }

        public bool WriteTrack(ITrack track)
        {
            return false;
        }

        public bool UpdateTrack(ITrack track)
        {
            return false;
        }

        public bool RemoveTrack(ITrack track)
        {
            return false;
        }

        public IPlaylist GetPlaylist(string id)
        {
            return null;
        }

        public bool WritePlaylist(IPlaylist playlist)
        {
            return false;
        }

        public bool UpdatePlaylist(IPlaylist playlist)
        {
            return false;
        }

        public bool RemovePlaylist(IPlaylist playlist)
        {
            return false;
        }

        private void BuildTracks()
        {
            ITrack CUT_TO_THE_FEELING = ModelFactory.BuildTrack("11dFghVXANMlKmJXsNCbNl", "Cut To The Feeling", null, 0, null, null);

            tracks = new Dictionary<string, ITrack>(
                new KeyValuePair<string, ITrack>[]
                {
                    new KeyValuePair<string, ITrack>(CUT_TO_THE_FEELING.ID, CUT_TO_THE_FEELING)
                }
            );
        }

        private void BuildPlaylists()
        {
            playlists = new Dictionary<string, IPlaylist>();
        }
    }
}
