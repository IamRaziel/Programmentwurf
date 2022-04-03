using MusicApi.Backend.Music;
using MusicApi.Model;
using System.Collections.Generic;

namespace MusicApi.Backend.SourceApi.Database
{
    //
    //DB CONNECTION IS NOT PART OF THIS PROJECT
    //
    //ANYWAY THIS IMPLEMENTATION WILL HAVE THE SAME FUNCTIONALITY IF IT CONNECTED TO AN DB
    //
    public class DBApi : IDBConnection, IFileWriter
    {
        private string defaultDirectory;

        // THIS VARIABLES WONT BE NEEDED FOR REAL DB
        private IDictionary<string, ITrack> tracks;
        private IDictionary<string, IAlbum> albums;
        private IDictionary<string, IPlaylist> playlists;
        private readonly string DEFAULT_FILE_PATH = "Q:/Dateien/Music/TitleMusik/Nyan Cat.mp3";

        public string DefaultDirectory
        {
            get { return defaultDirectory; }
            set { defaultDirectory = value; }
        }

        public DBApi(string defaultDirectory)
        {
            DefaultDirectory = defaultDirectory;

            //CONNECTION TO DB WOULD BE ESTABLISHED HERE

            BuildAlbums();
            BuildPlaylists();
            BuildTracks();
        }

        public IAlbum GetAlbum(string id)
        {
            return albums.ContainsKey(id) ? albums[id] : null;
        }

        public bool WriteAlbum(IAlbum album)
        {
            if (albums.ContainsKey(album.ID))
            {
                return false;
            }
            albums.Add(album.ID, album);
            return true;
        }

        public bool UpdateAlbum(IAlbum album)
        {
            if (albums.ContainsKey(album.ID))
            {
                albums[album.ID] = album;
                return true;
            }
            return false;
        }

        public bool RemoveAlbum(string id)
        {
            return albums.ContainsKey(id) ? albums.Remove(id) : false;
        }

        public ITrack GetTrack(string id)
        {
            return tracks.ContainsKey(id) ? tracks[id] : null;
        }

        public bool WriteTrack(ITrack track)
        {
            if (tracks.ContainsKey(track.ID))
            {
                return false;
            }
            tracks.Add(track.ID, track);
            return true;
        }

        public bool UpdateTrack(ITrack track)
        {
            if (tracks.ContainsKey(track.ID))
            {
                tracks[track.ID] = track;
                return true;
            }
            return false;
        }

        public bool RemoveTrack(string id)
        {
            return tracks.ContainsKey(id) ? tracks.Remove(id) : false;
        }

        public IPlaylist GetPlaylist(string id)
        {
            return playlists.ContainsKey(id) ? playlists[id] : null;
        }

        public bool WritePlaylist(IPlaylist playlist)
        {
            if (playlists.ContainsKey(playlist.ID))
            {
                return false;
            }
            playlists.Add(playlist.ID, playlist);
            return true;
        }

        public bool UpdatePlaylist(IPlaylist playlist)
        {
            if (playlists.ContainsKey(playlist.ID))
            {
                playlists[playlist.ID] = playlist;
                return true;
            }
            return false;
        }

        public bool RemovePlaylist(string id)
        {
            return playlists.ContainsKey(id) ? playlists.Remove(id) : false;
        }

        //THIS FUNCTIONS WONT BE NEEDED FOR REAL DB
        private void BuildTracks()
        {
            var artists = new string[] { };
            ITrack CUT_TO_THE_FEELING = ModelFactory.BuildTrack("11dFghVXANMlKmJXsNCbNl", "Cut To The Feeling", null, 0, artists, null, DEFAULT_FILE_PATH);

            tracks = new Dictionary<string, ITrack>(
                new KeyValuePair<string, ITrack>[]
                {
                    new KeyValuePair<string, ITrack>(CUT_TO_THE_FEELING.ID, CUT_TO_THE_FEELING)
                }
            );

            foreach (var each in albums)
            {
                foreach (var eachTrack in each.Value.Tracks)
                {
                    tracks.Add(eachTrack.ID, eachTrack);
                }
            }
            foreach (var each in playlists)
            {
                foreach (var eachTrack in each.Value.Tracks)
                {
                    tracks.Add(eachTrack.ID, eachTrack);
                }
            }
        }

        //REAL ALBUM FROM SPOTIFY "https://api.spotify.com/v1/albums/4aawyAB9vmqN3uQ7FjRGTy"
        private void BuildAlbums()
        {
            string albumID = "4aawyAB9vmqN3uQ7FjRGTy";
            var artists = new string[] { "Pitbull", "Sensato" };
            ITrack GLOBAL_WARMING_TRACK = ModelFactory.BuildTrack("6OmhkSOpvYBokMKQxpIGx2", "Global Warming (feat. Sensato)", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", 85400, artists, albumID, DEFAULT_FILE_PATH);

            artists = new string[] { "Pitbull", "TJR" };
            ITrack DONT_STOP_THE_PARTY = ModelFactory.BuildTrack("2iblMMIgSznA464mNov7A8", "Don't Stop the Party (feat. TJR)", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", 206120, artists, albumID, DEFAULT_FILE_PATH);

            artists = new string[] { "Pitbull", "Christina Aguilera" };
            ITrack FEEL_THIS_MOMENT = ModelFactory.BuildTrack("4yOn1TEcfsKHUJCL2h1r8I", "Feel This Moment (feat. Christina Aguilera)", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", 229506, artists, albumID, DEFAULT_FILE_PATH);

            artists = new string[] { "Pitbull" };
            ITrack BACK_IN_TIME = ModelFactory.BuildTrack("7fmpKF0rLGPnP7kcQ5ZMm7", "Back in Time - featured in \"Men In Black 3\"", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", 207440, artists, albumID, DEFAULT_FILE_PATH);

            artists = new string[] { "Pitbull", "Chris Brown" };
            ITrack HOPE_WE_MEET_AGAIN = ModelFactory.BuildTrack("3jStb2imKd6oUoBT1zq5lp", "Hope We Meet Again (feat. Chris Brown)", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", 221133, artists, albumID, DEFAULT_FILE_PATH);

            artists = new string[] { "Pitbull", "Usher", "Afrojack" };
            ITrack PARTY_AINT_OVER = ModelFactory.BuildTrack("6Q4PYJtrq8CBx7YCY5IyRN", "Party Ain't Over (feat. Usher & Afrojack)", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", 243160, artists, albumID, DEFAULT_FILE_PATH);

            artists = new string[] { "Pitbull", "Jennifer Lopez" };
            ITrack DRINKS_FOR_YOU = ModelFactory.BuildTrack("0QTVwqcOsYd73AOkYkk0Hg", "Drinks for You (Ladies Anthem) (feat. J. Lo)", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", 196920, artists, albumID, DEFAULT_FILE_PATH);

            artists = new string[] { "Pitbull", "The Wanted", "Afrojack" };
            ITrack HAVE_SOME_FUN = ModelFactory.BuildTrack("10Sydb6AAFPdgCzCKOSZuI", "Have Some Fun (feat. The Wanted & Afrojack", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", 244920, artists, albumID, DEFAULT_FILE_PATH);

            artists = new string[] { "Pitbull", "Danny Mercer" };
            ITrack OUTTA_NOWHERE = ModelFactory.BuildTrack("4k61iDqmtX9nI7RfLmp9aq", "Outta Nowhere (feat. Danny Mercer)", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", 206800, artists, albumID, DEFAULT_FILE_PATH);

            artists = new string[] { "Pitbull", "Enrique Iglesias" };
            ITrack TCHU_TCHU_TCHA = ModelFactory.BuildTrack("7oGRkL31ElVMcevQDceT99", "Tchu Tchu Tcha (feat. Enrique Iglesias)", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", 205800, artists, albumID, DEFAULT_FILE_PATH);

            artists = new string[] { "Pitbull", "Afrojack", "Havana Brown" };
            ITrack LAST_NIGHT = ModelFactory.BuildTrack("60xPqMqnHZl7Jfiu6E9q8X", "Last Night (feat. Afrojack & Havana Brown)", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", 219600, artists, albumID, DEFAULT_FILE_PATH);

            artists = new string[] { "Pitbull" };
            ITrack IM_OFF_THAT = ModelFactory.BuildTrack("1jAdXqOSICyXYLaW9ioSur", "I'm Off That", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", 197520, artists, albumID, DEFAULT_FILE_PATH);
            
            artists = new string[] { "Pitbull", "Papayo" };
            ITrack ECHA_PALLA = ModelFactory.BuildTrack("0fjRYHFz9ealui1lfnN8it", "Echa Pa'lla (Manos Pa'rriba) (feat. Papayo)", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", 196440, artists, albumID, DEFAULT_FILE_PATH);

            artists = new string[] { "Pitbull", "Akon", "David Rush" };
            ITrack EVERYBODY_FUCKS = ModelFactory.BuildTrack("7of35ktwTbL906Z1i3mT4K", "Everybody Fucks (feat. Akon & David Rush)", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", 257613, artists, albumID, DEFAULT_FILE_PATH);
            
            artists = new string[] { "Pitbull", "Shakira" };
            ITrack GET_IT_STARTED = ModelFactory.BuildTrack("2JA6A6Y5f4m7PawM58U2Op", "Get It Started (feat. Shakira)", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", 245920, artists, albumID, DEFAULT_FILE_PATH);

            artists = new string[] { "Pitbull", "Vein" };
            ITrack ELEVEN_FIFTHYNINE = ModelFactory.BuildTrack("726qZxwhP0jVyIA0ujnnhb", "11:59 (feat. Vein)", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", 217680, artists, albumID, DEFAULT_FILE_PATH);
            
            artists = new string[] { "Pitbull", "Marc Anthony", "Alle", "Benny Benassi" };
            ITrack RAIN_OVER_ME = ModelFactory.BuildTrack("6GPER1Sx8MrBiwWxdulg5Q", "Rain Over Me (feat. Marc Anthony) - Benny Benassi Remix", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", 316480, artists, albumID, DEFAULT_FILE_PATH);

            artists = new string[] { "Pitbull", "Chris Brown", "Jump Smokers"};
            ITrack INTERNATIONAL_LOVE = ModelFactory.BuildTrack("4TWgcICXXfGty8MHGWJ4Ne", "International Love (feat. Chris Brown) - Jump Smokers Extended Mix", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", 309626, artists, albumID, DEFAULT_FILE_PATH);

            var tracks = new ITrack[]
            {
                GLOBAL_WARMING_TRACK,
                DONT_STOP_THE_PARTY,
                FEEL_THIS_MOMENT,
                BACK_IN_TIME,
                HOPE_WE_MEET_AGAIN,
                PARTY_AINT_OVER,
                DRINKS_FOR_YOU,
                HAVE_SOME_FUN,
                OUTTA_NOWHERE,
                TCHU_TCHU_TCHA,
                LAST_NIGHT,
                IM_OFF_THAT,
                ECHA_PALLA,
                EVERYBODY_FUCKS,
                GET_IT_STARTED,
                ELEVEN_FIFTHYNINE,
                RAIN_OVER_ME,
                INTERNATIONAL_LOVE
            };

            IAlbum GLOBAL_WARMING = ModelFactory.BuildAlbum(albumID, "Global Warming", "https://i.scdn.co/image/ab67616d000048512c5b24ecfa39523a75c993c4", tracks, new List<string>(), artists);

            albums = new Dictionary<string, IAlbum>(
                new KeyValuePair<string, IAlbum>[]
                {
                    new KeyValuePair<string, IAlbum>(GLOBAL_WARMING.ID, GLOBAL_WARMING)
                }
            );

        }

        private void BuildPlaylists()
        {
            playlists = new Dictionary<string, IPlaylist>();
        }

        //
        // The functions for the IFileWriter
        //

        public bool Write(byte[] bytes, string fileName)
        {
            throw new System.NotImplementedException();
        }

        public bool Write(byte[] bytes, string fileName, string directory)
        {
            throw new System.NotImplementedException();
        }

        //
        // For Test Purposes 
        //

        public IDictionary<string, ITrack> Tracks
        {
            get { return tracks; }
        }
    }
}
