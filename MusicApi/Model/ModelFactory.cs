using MusicApi.Backend.Music;
using System;
using System.Collections.Generic;

namespace MusicApi.Model
{
    public static class ModelFactory
    {
        private static ISet<string> playlistIDs = new HashSet<string>();

        public static ITrack BuildTrack(string id, string title, IList<string> images, int duration, IList<string> artists, string albumID)
        {
            return new DTOTrack(id, title, images, duration, artists, albumID);
        }

        //USED TO BUILD NEW PLAYLISTS
        public static IPlaylist BuildPlaylist(string name)
        {
            string id = Guid.NewGuid().ToString();
            while (playlistIDs.Contains(id))
            {
                id = Guid.NewGuid().ToString();
            }
            return new DTOPlaylist(id, name);
        }

        //USED TO BUILD IMPORTED PLAYLISTS
        public static IPlaylist BuildPlaylist(string id, string name, string image, IList<ITrack> tracks)
        {
            return new DTOPlaylist(id, name, image, tracks);
        }

        public static IAlbum BuildAlbum(string id, string name, string image, IList<ITrack> tracks, IList<string> genres, IList<string> artists)
        {
            return new DTOAlbum(id, name, image, tracks, genres, artists);
        }
    }
}
