using System;
using System.Collections.Generic;
using MusicApi.Backend.Music;
using MusicApi.Backend.SourceApi.Database;

namespace MusicApi.Backend
{
    public class QueueController
    {
        private QueueMusicPlayer player = new QueueMusicPlayer();

        public QueueController()
        {

        }

        public void AddAlbumsToQueue(IList<string> albumIDs)
        {
            var albums = BackendController.AlbumController.GetAlbumsFromID(albumIDs);
            player.AddAlbums(albums);
        }

        public void AddPlaylistsToQueue(IList<string> playlistIDs)
        {
            var playlists = BackendController.PlaylistController.GetPlaylistsFromID(playlistIDs);
            player.AddPlaylists(playlists);
        }

        public void AddTracksToQueue(IList<string> trackIDs)
        {
            var tracks = BackendController.TrackController.GetTracksFromID(trackIDs);
            player.AddTracks(tracks);
        }

        public IList<ITrack> GetTracksOfQueue()
        {
            return player.Tracks;
        }

        public bool MoveTracksInQueue(string trackID, int position)
        {
            var track = BackendController.TrackController.GetTrackFromID(trackID);
            return player.MoveTrack(track, position);
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
