namespace MusicApi.Backend.SourceApi.Spotify.Json
{
    public class TracksJson
    {
        public string href { get; set; }
        public ItemJson[] items { get; set; }
        public int limit { get; set; }
        public object next { get; set; }
        public int offset { get; set; }
        public object previous { get; set; }
        public int total { get; set; }
    }
}
