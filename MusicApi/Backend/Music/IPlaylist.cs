using System.Collections.Generic;

namespace MusicApi.Backend.Music
{
    public interface IPlaylist
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public IList<ITrack> Tracks { get; set; }
    }
}
