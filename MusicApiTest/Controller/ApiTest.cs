using Microsoft.AspNetCore.Mvc;
using MusicApi.Backend;
using MusicApi.Backend.Music;
using MusicApi.Backend.SourceApi.Database;
using MusicApi.Controllers;
using MusicApiTest.Backend;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MusicApiTest.Controller
{
    public class ApiTest
    {
        private AlbumApi albumApi;
        private PlaylistApi playlistApi;
        private TrackApi trackApi;
        private PlaylistTrackApi playlistTrackApi;
        private QueueApi queueApi;

        private DBApi dbApi = new DBApi(BackendController.DEFAULT_DIRECTORY);
        private IList<ITrack> tracks;
        private IList<IAlbum> albums;
        private IList<IPlaylist> playlists;

        [SetUp]
        public void Setup()
        {
            albumApi = new AlbumApi();
            playlistApi = new PlaylistApi();    
            playlistTrackApi = new PlaylistTrackApi();  
            trackApi = new TrackApi();
            queueApi = new QueueApi();
        }

        [Test]
        public void Test1_ValidCreationTest()
        {
            Assert.IsNotNull(albumApi);
            Assert.IsNotNull(playlistApi);
            Assert.IsNotNull(playlistTrackApi);
            Assert.IsNotNull(trackApi);
            Assert.IsNotNull(queueApi);
        }

        [Test]
        public void Test2_LoadMusic()
        {
            var music = BackendController.LoadMusicFromDB();
            albums = music.Item1;
            tracks = music.Item2;
            playlists = music.Item3;

            Util.CheckTracks(tracks, dbApi.GetTracks());
            Util.CheckAlbums(albums, dbApi.GetAlbums());
            Util.CheckPlaylists(playlists, dbApi.GetPlaylists());
        }

        [Test]
        public void Test3_QueueApi_AddRemoveAndMoveTrackInQueue()
        {
            var subTracks = new List<ITrack>(new ITrack[] { tracks[0], tracks[2], tracks[4], tracks[0], tracks[0] });
            var subTrackIDs = new List<string>();
            foreach (var each in subTracks)
            {
                subTrackIDs.Add(each.ID);
            }
            queueApi.AddTracksToQueue(subTrackIDs);

            // t0, t2, t4, t0, t0
            Util.CheckTracks(subTracks, LoadTracksOfResult());

            var trackPositionsToRemove = new HashSet<int>(new int[] { 0, 4, 3 });
            subTracks = new List<ITrack>(new ITrack[] {tracks[2], tracks[4]});

            queueApi.RemoveTracksFromQueue(trackPositionsToRemove);

            // t2, t4
            Util.CheckTracks(subTracks, LoadTracksOfResult());

            var subAlbums = new List<IAlbum>(new IAlbum[] { albums[0] });
            var subAlbumIDs = new List<string>();
            foreach (var each in subAlbums)
            {
                subAlbumIDs.Add(each.ID);
            }
            queueApi.AddAlbumsToQueue(subAlbumIDs);

            subTracks.AddRange(albums[0].Tracks);

            // t2, t4, a0t0, a0t1, a0t2, a0t3, a0t4, a0t5, a0t6, a0t7, a0t8, a0t9, a0t10, a0t11, a0t12, a0t13, a0t14, a0t15, a0t16, a0t17
            Util.CheckTracks(subTracks, LoadTracksOfResult());

            subTracks.Add(tracks[5]);
            subTracks.Add(tracks[6]);
            queueApi.AddTracksToQueue(new string[] { tracks[5].ID, tracks[6].ID });

            // t2, t4, a0t0, a0t1, a0t2, a0t3, a0t4, a0t5, a0t6, a0t7, a0t8, a0t9, a0t10, a0t11, a0t12, a0t13, a0t14, a0t15, a0t16, a0t17, t5, t6
            Util.CheckTracks(subTracks, LoadTracksOfResult());

            var a0t0 = subTracks[2];
            var t4 = subTracks[1];
            subTracks[1] = subTracks[2];
            subTracks[2] = t4;

            queueApi.MoveTrackInQueue(t4.ID, 2);

            // t2, a0t0, t4, a0t1, a0t2, a0t3, a0t4, a0t5, a0t6, a0t7, a0t8, a0t9, a0t10, a0t11, a0t12, a0t13, a0t14, a0t15, a0t16, a0t17, t5, t6
            Util.CheckTracks(subTracks, LoadTracksOfResult());

            Assert.True(BackendController.MoveTracksInQueue(t4.ID, 1), "Reverseing the switch failed.");
            Assert.True(BackendController.MoveTracksInQueue(a0t0.ID, 1), "Can't switch position of track t4.");

            // t2, a0t0, t4, a0t1, a0t2, a0t3, a0t4, a0t5, a0t6, a0t7, a0t8, a0t9, a0t10, a0t11, a0t12, a0t13, a0t14, a0t15, a0t16, a0t17, t5, t6
            Util.CheckTracks(subTracks, LoadTracksOfResult());

            queueApi.RemoveTracksFromQueue(new HashSet<int>(new int[] { -1, 100 }));

            // t2, a0t0, t4, a0t1, a0t2, a0t3, a0t4, a0t5, a0t6, a0t7, a0t8, a0t9, a0t10, a0t11, a0t12, a0t13, a0t14, a0t15, a0t16, a0t17, t5, t6
            Util.CheckTracks(subTracks, LoadTracksOfResult());
        }

        private IList<ITrack> LoadTracksOfResult()
        {
            var result = queueApi.GetQueue();
            Assert.NotNull(result, "Result is null.");

            OkObjectResult objResult = result is OkObjectResult ? result as OkObjectResult : null;
            Assert.NotNull(objResult, "Can't convert the result.");

            Assert.AreEqual(objResult.StatusCode, 200, "The status code is not 200.");

            IList<ITrack> tracks = objResult.Value is IList<ITrack> ? objResult.Value as IList<ITrack>: null;
            Assert.NotNull(tracks, "Can't convert the value of the result to a list of tracks.");

            return tracks;
        }
    }
}
