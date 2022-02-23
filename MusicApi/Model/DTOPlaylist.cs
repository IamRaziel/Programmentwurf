using MusicApi.Backend.Music;
using System.Collections.Generic;

namespace MusicApi.Model
{
    public class DTOPlaylist : IPlaylist
    {
        private string name;
        private string image;
        private IList<ITrack> tracks;
        private string id;

        //USED FOR GENERATING NEW PLAYLIST
        public DTOPlaylist(string id, string name)
        {
            ID = id;
            Name = name;
        }

        //USED TO BUILD IMPORTED PLAYLISTS
        public DTOPlaylist(string id, string name, string image, IList<ITrack> tracks) : this(id, name)
        {
            Image = image;
            Tracks = tracks;
        }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name 
        {
            get { return name; } 
            set { name = value; } 
        }

        public string Image 
        {
            get { return image; } 
            set { image = value; }
        }

        public IList<ITrack> Tracks 
        { 
            get { return tracks;} 
            set { tracks = value; }
        }
    }
}
