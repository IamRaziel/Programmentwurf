using System;
using System.Collections.Generic;
using MusicApi.Backend.Music;

namespace MusicApi.Model
{
    public class DTOAlbum : DTOPlaylist, IAlbum
    {
        private string genre;
        private IList<string> artists;

        public DTOAlbum(string id, string name, string image, IList<ITrack> tracks, string genre, IList<string> artists) : base(id, name, image, tracks)
        {
            Genre = genre;
            Artists = artists;
        }

        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }

        public IList<string> Artists
        {
            get { return artists; }
            set { artists = value; }
        }
    }
}
