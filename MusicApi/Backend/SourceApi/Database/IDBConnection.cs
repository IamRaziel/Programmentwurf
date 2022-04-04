using MusicApi.Backend.Music;
using System.Collections.Generic;

namespace MusicApi.Backend.SourceApi.Database
{
    public interface IDBConnection : IApi
    {
        IList<IAlbum> GetAlbums();

        bool WriteAlbum(IAlbum album);

        bool UpdateAlbum(IAlbum album);

        bool RemoveAlbum(string id);

        IList<ITrack> GetTracks();

        bool WriteTrack(ITrack track);

        bool UpdateTrack(ITrack track);

        bool RemoveTrack(string id);

        IList<IPlaylist> GetPlaylists();

        bool WritePlaylist(IPlaylist playlist);

        bool UpdatePlaylist(IPlaylist playlist);

        bool RemovePlaylist(string id);
    }
}
