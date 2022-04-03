using System;
using System.Collections.Generic;

namespace MusicApi.Backend.Music
{
    public class QueueMusicPlayer : MusicPlayer
    {
        public IList<ITrack> Tracks { get; set; }
        public QueueMusicPlayMode Mode { get; set; }
        public int Position { get; set; }

        public QueueMusicPlayer() : this(new List<ITrack>())
        {
        }


        public QueueMusicPlayer(IList<ITrack> tracks)
        {
            Tracks = new List<ITrack>(tracks);
            Mode = QueueMusicPlayMode.QUEUE;
        }

        public void AddAlbums(IList<IAlbum> albums)
        {
            foreach (var each in albums)
            {
                AddTracks(each.Tracks);
            }
        }

        public void AddPlaylists(IList<IPlaylist> playlists)
        {
            foreach (var each in playlists)
            {
                AddTracks(each.Tracks);
            }
        }

        public void AddTracks(IList<ITrack> tracks)
        {
            foreach (var each in tracks)
            {
                Tracks.Add(each);
            }
        }

        public void RemoveTracks(IList<ITrack> tracks)
        {
            foreach (var each in tracks)
            {
                RemoveTrack(each);
            }
        }

        public void RemoveTrack(ITrack track)
        {
            var position = GetPositionOfTrack(track);
            if (position != -1)
            {
                Tracks.RemoveAt(position);
            }
        }

        private int GetPositionOfTrack(ITrack track)
        {
            int position = 0;
            foreach (var each in Tracks)
            {
                if (Contains(track))
                {
                    return position;
                }
                position++;
            }
            return -1;
        }

        public bool MoveTrack(ITrack track, int position)
        {
            if (!Contains(track))
            {
                return false;
            }
            else if (!(-1 < position && position < Tracks.Count))
            {
                return false;
            }
            int oldPosition = GetPositionOfTrack(track);

            SwitchPositions(oldPosition, position);

            return true;
        }

        private void SwitchPositions(int oldPosition, int newPosition)
        {
            var track = Tracks[oldPosition];
            // 0,1,2,3,4,5,6
            // 0,2,3,1,4,5,6
            // 1 soll nach 3 (pos1 -> pos3)
            if (oldPosition < newPosition)
            {
                for (int i = oldPosition; i < newPosition; i++)
                {
                    Tracks[i] = Tracks[i + 1];
                }
                Tracks[newPosition] = track;
            }
            // 0,2,3,1,4,5,6
            // 0,1,2,3,4,5,6
            // 1 soll nach 0 (pos3 -> pos1)
            else if (newPosition < oldPosition)
            {
                for (int i = oldPosition; i > newPosition; i--)
                {
                    Tracks[i] = Tracks[i - 1];
                }
                Tracks[newPosition] = track;
            }
        }


        public void PlayNext()
        {
            Position = GetNextPosition();
            var track = Tracks[Position];
            Stop();
            PlayMp3FromUrl(track.FilePath);
        }

        private int GetNextPosition()
        {
            int position = Position;
            switch (Mode)
            {
                case QueueMusicPlayMode.QUEUE: position++;
                    break;
                case QueueMusicPlayMode.RANDOM: position = GetRandomPosition();
                    break;
            }
            if (position >= Tracks.Count)
            {
                position = 0;
            }
            return position;
        }

        private int GetRandomPosition()
        {
            var random = new Random();
            int position = Position;
            while (position == Position)
            {
                double d = random.NextDouble();
                position = (int)(d * Tracks.Count);
            }
            return position;
        }

        private bool Contains(ITrack track)
        {
            foreach (var each in Tracks)
            {
                if (each.ID.Equals(track.ID))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
