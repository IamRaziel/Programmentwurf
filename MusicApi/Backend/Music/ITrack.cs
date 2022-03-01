using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace MusicApi.Backend.Music
{
    public interface ITrack
    {
        public string ID { get; }

        public string Title { get; }

        public string Image { get; }

        public int Duration { get; }

        public IList<string> Artists { get; }

        public string AlbumID { get; }

        public string FilePath { get; }
    }
}
