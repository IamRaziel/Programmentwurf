using MusicApi.Backend.Music;
using System.Collections.Generic;

namespace MusicApi.Model
{
    public class DTOTrack : ITrack
    {
        public DTOTrack(string id, string title, IList<string> images, int duration, IList<string> artists, string albumID)
        {
            ID = id;
            Title = title;
            Images = images;
            Duration = duration;
            Artists = artists;
            AlbumID = albumID;
        }

        public string ID { get; set; }

        public string Title { get; set; }

        public IList<string> Images { get; set; }

        public int Duration { get; set; }

        public IList<string> Artists { get; set; }

        public string AlbumID { get; set; }
    }
}
