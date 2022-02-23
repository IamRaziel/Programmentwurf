using MusicApi.Backend.Music;
using System.Collections.Generic;

namespace MusicApi.Model
{
    public static class ModelFactory
    {
        public static ITrack BuildTrack(string id, string title, IList<string> images, int duration, IList<string> artists, string albumID)
        {
            return new DTOTrack(id, title, images, duration, artists, albumID);
        }
    }
}
