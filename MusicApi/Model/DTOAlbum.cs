using System;
using System.Collections.Generic;
using MusicApi.Backend.Music;

namespace MusicApi.Model
{
    public class DTOAlbum : DTOPlaylist, IAlbum
    {
        private IList<string> genres;
        private IList<string> artists;

        public DTOAlbum(string id, string name, string image, IList<ITrack> tracks, IList<string> genre, IList<string> artists) : base(id, name, image, tracks)
        {
            Genres = genre;
            Artists = artists;
        }

        public IList<string> Genres
        {
            get { return genres; }
            set { genres = value; }
        }

        public IList<string> Artists
        {
            get { return artists; }
            set { artists = value; }
        }
    }
}
