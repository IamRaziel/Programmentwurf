using System;
using System.Collections.Generic;
using MusicApi.Backend.Music;
using MusicApi.Backend.SourceApi.Database;

namespace MusicApi.Backend
{
    public class QueueController
    {
        private QueueMusicPlayer player = new QueueMusicPlayer();
        private IDBConnection db;

        public QueueController(IDBConnection dbConnection)
        {
            db = dbConnection;
        }

        public void AddAlbumsToQueue(IList<IAlbum> albums)
        {
            player.AddAlbums(albums);
        }

        public void AddPlaylistsToQueue(IList<IPlaylist> playlists)
        {
            player.AddPlaylists(playlists);
        }

        public void AddTracksToQueue(IList<ITrack> tracks)
        {
            player.AddTracks(tracks);
        }

        public IList<ITrack> GetTracksOfQueue()
        {
            return player.Tracks;
        }

        public bool MoveTracksInQueue(string trackID, int position)
        {
            return player.MoveTrack(GetTrackFromID(trackID), position);
        }

        public void PlayNextTrackInQueue()
        {
            player.PlayNext();
        }

        public void RemoveTracksFromQueue(ISet<int> positions)
        {
            player.RemoveTracksFromPositions(positions);
        }

        public void ResumeMusicPlayer()
        {
            player.Resume();
        }

        public void StopMusicPlayer()
        {
            player.Stop();
        }

        public QueueMusicPlayMode SwitchModeOfQueue()
        {
            if (player.Mode == QueueMusicPlayMode.RANDOM)
            {
                player.Mode = QueueMusicPlayMode.QUEUE;
            }
            else
            {
                player.Mode = QueueMusicPlayMode.RANDOM;
            }
            return player.Mode;
        }
    }
}
