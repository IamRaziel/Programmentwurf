using MusicApi.Backend.Music;
using MusicApi.Backend.SourceApi.Spotify;

namespace MusicApi.Backend
{
    public static class BackendController
    {
        private static SpotifyApi api = new SpotifyApi();
         
        public static ITrack GetTrack()
        {
            return api.GetTrack("11dFghVXANMlKmJXsNCbNl");
        }
    }
}
