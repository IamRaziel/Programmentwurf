using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicApi.Backend.Music;
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
            tracks = Constants.DB.Tracks.Values.ToList();
            player = new QueueMusicPlayer(tracks);
        }

        /// <summary>
        /// Removes tracks, adds them again and swaps postiton, should be the same as before removal.
        /// </summary>
        [Test]
        public void Test1_RemoveAddMoveTracks()
        {
            var track = tracks[0];
            player.RemoveTrack(track);
            player.AddTracks(new ITrack[] { track });
            player.MoveTrack(track, 0 + 1);

            CheckTracks(player.Tracks);

            int number = tracks.Count - 3;
            track = tracks[number];
            player.RemoveTrack(track);
            player.AddTracks(new ITrack[] { track });
            player.MoveTrack(track, number + 1);

            CheckTracks(player.Tracks);

            number = 5;
            track = tracks[number];
            int number2 = tracks.Count - 1;
            var track2 = tracks[number2];

            var rmTracks = new ITrack[] { track, track2 };
            player.RemoveTracks(rmTracks);
            player.AddTracks(rmTracks);
            player.MoveTrack(track, number + 1);
            player.MoveTrack(track2, number2 + 1);

            CheckTracks(player.Tracks);
        }

        private void CheckTracks(IList<ITrack> tracksToCheck)
        {
            CheckTracks(tracks, tracksToCheck);
        }

        private void CheckTracks(IList<ITrack> tracks, IList<ITrack> tracksToCheck)
        {
            int tCount = tracks.Count;
            int ttcCount = tracksToCheck.Count;
            bool failed = false;
            StringBuilder output = new StringBuilder();
            if (tCount != ttcCount)
            {
                failed = true;
                output.AppendLine("The Lists arent the same length. " + tracks.Count + " != " + tracksToCheck.Count);
            }

            for (int i = 0; i < tracks.Count; i++)
            {
                string tID = tracks[i].ID;
                if (i > ttcCount)
                {
                    failed = true;
                    output.AppendLine("At position " + i + " the track with id: " + tID + " is missing.");
                }
                else
                {
                    string ttcID = tracksToCheck[i].ID;
                    if (!tID.Equals(ttcID))
                    {
                        output.AppendLine("At position " + i + " the tracks have different IDs \n" +
                            "      id:        " + tID + "\n" +
                            "      idToCheck: " + ttcID);
                    }
                }
            }

            if (tracksToCheck.Count > tracks.Count)
            {
                failed = true;
                output.AppendLine("The tracks are longer than they should.");
                for (int i = tracks.Count; i < tracksToCheck.Count; i++)
                {
                    string ttcID = tracksToCheck[i].ID;
                    output.AppendLine("The track with id: " + ttcID + " is not existing in the compare List.");
                }
            }

            if (failed)
            {
                Assert.Fail(output.ToString());
            }
        }
    }
}
