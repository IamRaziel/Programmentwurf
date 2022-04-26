using Microsoft.AspNetCore.Http;
using MusicApi.Backend.Music;
using MusicApi.Backend.SourceApi;
using MusicApi.Backend.SourceApi.Database;
using MusicApi.Backend.SourceApi.Spotify;
using MusicApi.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace MusicApi.Backend
{
    public static class BackendController
    {
        public static readonly string DEFAULT_DIRECTORY = "Q:/Dateien/Music/";

        private static IApi spotify = new SpotifyApi();

        private static DBApi dbApi = new DBApi(DEFAULT_DIRECTORY);
        private static IDBConnection db = dbApi;
        private static IFileWriter fileWriter = dbApi;

        private static AlbumController albumController;
        private static PlaylistController playlistController;
        private static QueueController queueController;
        private static TrackController trackController;

        public static AlbumController AlbumController { get { return albumController; } }
        public static PlaylistController PlaylistController { get { return playlistController; } }
        public static QueueController QueueController { get { return queueController; } }
        public static TrackController TrackController { get { return trackController; } }

        public static void BuildController()
        {
            albumController = new AlbumController(db);
            playlistController = new PlaylistController(db);
            queueController = new QueueController(db);
            trackController = new TrackController(db, fileWriter);
        }

        private static void LoadMusicFromDB()
        {
            albumController.AddAlbums(db.GetAlbums());
            playlistController.AddPlaylists(db.GetPlaylists());
            trackController.AddTracks(db.GetTracks());
        }

        public static bool Download(string url)
        {
            

            return true;
        }
    }
}
