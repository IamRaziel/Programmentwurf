namespace MusicApi.Backend.SourceApi.Spotify.Json
{
    public class GetAlbumTracksJson
    {
        public string album_type { get; set; }
        public ArtistJson[] artists { get; set; }
        public CopyrightJson[] copyrights { get; set; }
        public ExternalIdsJson external_ids { get; set; }
        public ExternalUrlsJson external_urls { get; set; }
        public object[] genres { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public ImageJson[] images { get; set; }
        public string label { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string release_date { get; set; }
        public string release_date_precision { get; set; }
        public int total_tracks { get; set; }
        public TracksJson tracks { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }


}
