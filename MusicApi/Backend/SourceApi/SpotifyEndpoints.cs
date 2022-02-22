namespace MusicApi.Backend.SourceApi
{
    public static class SpotifyEndpoints
    {
        private static readonly string BASE = "https://api.spotify.com";

        //ALBUMS
        public static readonly string GET_ALBUM_TRACKS = BASE + "/v1/albums/{id}/tracks";
        public static readonly string GET_ALBUM = BASE + "/v1/albums/{id}";
        public static readonly string GET_ALBUMS = BASE + "/v1/albums";

        //ARTISTS
        public static readonly string GET_ARTISTS_ALBUMS = BASE + "/v1/artists/{id}/albums";
        public static readonly string GET_ARTISTS_RELATED_ARTISTS = BASE + "/v1/artists/{id}/related-artists";
        public static readonly string GET_ARTISTS_TOP_TRACKS = BASE + "/v1/artists/{id}/top-tracks";
        public static readonly string GET_ARTIST = BASE + "/v1/artists/{id}";
        public static readonly string GET_SEVERAL_ARTISTS = BASE + "/v1/artists";

        //BROWSE
        public static readonly string GET_AVAILABLE_GENRE_SEEDS = BASE + "/v1/recommendations/available-genre-seeds";
        public static readonly string GET_SEVERAL_BROWSE_CATEGORIES = BASE + "/v1/browse/categories";
        public static readonly string GET_SINGLE_BROWSE_CATEGORY = BASE + "/v1/browse/categories/{category_id}";
        public static readonly string GET_CATEGORYS_PLAYLISTS = BASE + "/v1/browse/categories/{category_id}/playlists";
        public static readonly string GET_FEATURED_PLAYLISTS = BASE + "/v1/browse/featured-playlists";
        public static readonly string GET_NEW_RELEASES = BASE + "/v1/browse/new-releases";
        public static readonly string GET_RECOMMENDATIONS = BASE + "/v1/recommendations";

        //EPISODES

        //FOLLOW

        //LIBRARY

        //MARKETS

        //PERSONALIZATION

        //PLAYER

        //PLAYLISTS

        //SEARCH

        //TRACKS
        public static readonly string GET_TRACK = BASE + "/v1/tracks/{id}";

        //SHOWS

        //USERS PROFILE

        //public static readonly string  = BASE + "";
        //public static readonly string  = BASE + "";
        //public static readonly string  = BASE + "";
        //public static readonly string  = BASE + "";
        //public static readonly string  = BASE + "";
        //public static readonly string  = BASE + "";
        //public static readonly string  = BASE + "";
        //public static readonly string  = BASE + "";
        //public static readonly string  = BASE + "";
        //public static readonly string  = BASE + "";


        public static readonly string GET_NEW_RELEASE = BASE + "/v1/browse/new-releases";

    }
}
