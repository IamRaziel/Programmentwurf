using MusicApi.Backend.Music;
using System.Collections.Generic;

namespace MusicApi.Model
{
    public class DTOTrack : ITrack
    {
        public DTOTrack(string id, string title, string image, int duration, IList<string> artists, string albumID, string filePath)
        {
            ID = id;
            Title = title;
            Image = image;
            Duration = duration;
            Artists = artists;
            AlbumID = albumID;
            FilePath = filePath;
        }

        public string ID { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public int Duration { get; set; }

        public IList<string> Artists { get; set; }

        public string AlbumID { get; set; }

        public string FilePath { get; set; }
    }
}
