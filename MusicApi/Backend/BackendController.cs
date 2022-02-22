using MusicApi.Backend.SourceApi;

namespace MusicApi.Backend
{
    public static class BackendController
    {
        public static string GetTrack()
        {
            var api = new SpotifyApi();
            return api.GetTrack("11dFghVXANMlKmJXsNCbNl");
        }
    }
}
