using System;
using System.Collections.Generic;

namespace MusicApi.Backend.Music
{
    public interface IAlbum : IPlaylist
    {
        string Genre { get; set; }

        IList<string> Artists { get; set; }
    }
}
