using MusicApi.Backend.Music;

namespace MusicApi.Backend.SourceApi.Database
{
    public interface IDBConnection : IApi
    {
        bool WriteTrack(ITrack track);

        bool UpdateTrack(ITrack track);

        bool RemoveTrack(ITrack track);

        bool WritePlaylist(IPlaylist playlist);

        bool UpdatePlaylist(IPlaylist playlist);

        bool RemovePlaylist(IPlaylist playlist);
    }
}
