using System;
using System.Collections.Generic;

namespace MusicApi.Backend.Music
{
    public interface IAlbum : IPlaylist
    {
        IList<string> Genres { get; set; }

        IList<string> Artists { get; set; }
    }
}
