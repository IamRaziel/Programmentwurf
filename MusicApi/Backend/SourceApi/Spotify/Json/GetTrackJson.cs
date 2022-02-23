namespace MusicApi.Backend.SourceApi.Spotify.Json
{
    public class GetTrackJson
    {
        public AlbumJson album { get; set; }
        public ArtistJson[] artists { get; set; }
        public int disc_number { get; set; }
        public int duration_ms { get; set; }
        public bool _explicit { get; set; }
        public ExternalIdsJson external_ids { get; set; }
        public ExternalUrlsJson external_urls { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public bool is_local { get; set; }
        public bool is_playable { get; set; }
        public LinkedFromJson linked_from { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public object preview_url { get; set; }
        public int track_number { get; set; }
        public string type { get; set; }
        public string uri { get; set; }

    }

}
