using MusicApi.Backend.Music;

namespace MusicApi.Backend.SourceApi.Database
{
    public interface IDBConnection : IApi
    {
        bool WriteAlbum(IAlbum album);

        bool UpdateAlbum(IAlbum album);

        bool RemoveAlbum(IAlbum album);

        bool WriteTrack(ITrack track);

        bool UpdateTrack(ITrack track);

        bool RemoveTrack(ITrack track);

        bool WritePlaylist(IPlaylist playlist);

        bool UpdatePlaylist(IPlaylist playlist);

        bool RemovePlaylist(IPlaylist playlist);
    }
}
