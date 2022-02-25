using MusicApi.Backend.Music;

namespace MusicApi.Backend.SourceApi.Database
{
    public interface IDBConnection : IApi
    {
        bool WriteAlbum(IAlbum album);

        bool UpdateAlbum(IAlbum album);

        bool RemoveAlbum(string id);

        bool WriteTrack(ITrack track);

        bool UpdateTrack(ITrack track);

        bool RemoveTrack(string id);

        bool WritePlaylist(IPlaylist playlist);

        bool UpdatePlaylist(IPlaylist playlist);

        bool RemovePlaylist(string id);
    }
}
