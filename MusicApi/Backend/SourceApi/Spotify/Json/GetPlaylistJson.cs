namespace MusicApi.Backend.SourceApi.Spotify.Json
{
    public class GetPlaylistJson
    {
        public bool collaborative { get; set; }
        public string description { get; set; }
        public ExternalUrlsJson external_urls { get; set; }
        public FollowersJson followers { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public ImageJson[] images { get; set; }
        public string name { get; set; }
        public OwnerJson owner { get; set; }
        public object primary_color { get; set; }
        public bool _public { get; set; }
        public string snapshot_id { get; set; }
        public TracksJson tracks { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }
}

