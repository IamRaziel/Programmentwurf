using MusicApi.Backend.Music;

namespace MusicApi.Backend.SourceApi
{
    public interface IApi
    {
        ITrack GetTrack(string id);

        IPlaylist GetPlaylist(string id);

        IAlbum GetAlbum(string id);
    }
}
