using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicApi.Backend;
using MusicApi.Backend.Music;
using MusicApi.Backend.SourceApi.Database;
using MusicApiTest.Backend;
using NUnit.Framework;

namespace MusicApiTest
{
    public class QueueMusicPlayerTest
    {
        private QueueMusicPlayer player;
        private IList<ITrack> tracks;

        [SetUp]
        public void Setup()
        {
            tracks = new DBApi(BackendController.DEFAULT_DIRECTORY).Tracks.Values.ToList();
        }

        /// <summary>
        /// Removes tracks from an queue.
        /// </summary>
        [Test]
        public void Test1_RemoveTracks()
        {
            player = new QueueMusicPlayer(tracks);

            int length = tracks.Count;
            IList<ITrack> subTracks = tracks.Sublist(1, length - 2);

            player.RemoveTrack(tracks[0]);
            player.RemoveTrack(tracks[length - 1]);

            CheckTracks(subTracks, player.Tracks);
        }

        /// <summary>
        /// Adds tracks to an queue.
        /// </summary>
        [Test]
        public void Test2_AddTracks()
        {
            player = new QueueMusicPlayer();

            player.AddTracks(tracks);

            CheckTracks(tracks);

            IList<ITrack> moreTracks = new List<ITrack>(tracks);
            moreTracks.Add(tracks[0]);

            player.AddTracks(new ITrack[] { tracks[0] });

            CheckTracks(moreTracks, player.Tracks);
        }

        /// <summary>
        /// Moves tracks in an queue.
        /// </summary>
        [Test]
        public void Test3_MoveTracks()
        {
            var t1 = tracks[1];
            var t2 = tracks[2];
            var t3 = tracks[3];
            var t4 = tracks[4];
            var t5 = tracks[5];
            var t6 = tracks[6];

            var tracks1 = new ITrack[] { t1, t2, t3, t4, t5, t6 };
            var tracks2 = new ITrack[] { t2, t3, t1, t4, t5, t6 };
            var tracks3 = new ITrack[] { t2, t1, t4, t5, t6, t3 };
            var tracks4 = new ITrack[] { t2, t5, t1, t4, t6, t3 };

            player = new QueueMusicPlayer(tracks1);
            player.MoveTrack(t1, 2);

            CheckTracks(tracks2, player.Tracks);

            player.MoveTrack(t3, 5);

            CheckTracks(tracks3, player.Tracks);

            player.MoveTrack(t5, 1);

            CheckTracks(tracks4, player.Tracks);
        }

        /// <summary>
        /// Removes tracks, adds them again and swaps postiton, should be the same as before removal.
        /// </summary>
        [Test]
        public void Test4_RemoveAddMoveTracks()
        {
            player = new QueueMusicPlayer(tracks);

            var track = tracks[0];
            player.RemoveTrack(track);
            player.AddTracks(new ITrack[] { track });
            player.MoveTrack(track, 0);

            CheckTracks(player.Tracks);

            int number = tracks.Count - 3;
            track = tracks[number];
            player.RemoveTrack(track);
            player.AddTracks(new ITrack[] { track });
            player.MoveTrack(track, number);

            CheckTracks(player.Tracks);

            number = 5;
            track = tracks[number];
            int number2 = tracks.Count - 1;
            var track2 = tracks[number2];

            var rmTracks = new ITrack[] { track, track2 };
            player.RemoveTracks(rmTracks);
            player.AddTracks(rmTracks);
            player.MoveTrack(track, number);
            player.MoveTrack(track2, number2);

            CheckTracks(player.Tracks);
        }

        [Test]
        public void Test5_ChangeMode()
        {
            player = new QueueMusicPlayer(tracks);

            player.Mode = QueueMusicPlayMode.RANDOM;

            Assert.AreEqual(player.Mode, QueueMusicPlayMode.RANDOM, "Mode cant be changed.");
        }

        public void CheckTracks(IList<ITrack> tracksToCheck)
        {
            Util.CheckTracks(tracks, tracksToCheck);
        }
    }
}
