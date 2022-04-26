using System;
using System.Collections.Generic;
using MusicApi.Backend.Music;
using MusicApi.Backend.SourceApi.Database;

namespace MusicApi.Backend
{
    public class AlbumController
    {
        private IDictionary<string, IAlbum> albums = new Dictionary<string, IAlbum>();
        private IDBConnection db;

        public AlbumController(IDBConnection dbConnection)
        {
            db = dbConnection;
        }

        public void AddAlbums(IList<IAlbum> albums)
        {
            if (albums != null)
            {
                foreach (var each in albums)
                {
                    if (each != null)
                    {
                        this.albums.Add(each.ID, each);
                    }
                }
            }
        }

        public bool DeleteAlbum(string id)
        {
            if (albums.ContainsKey(id))
            {
                var album = albums[id];
                if (albums.Remove(id))
                {
                    if (db.RemoveAlbum(id))
                    {
                        return true;
                    }
                    albums.Add(id, album);
                }
            }
            return false;
        }

        public IAlbum GetAlbumFromID(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            IAlbum album = null;
            if (!albums.TryGetValue(id, out album)) return null;
            return album;
        }

        //Needs to be changed to GetAlbumFromURL(string url)
        public IAlbum GetAlbumFromUrl(string id)
        {
            //TODO
            if (albums.ContainsKey(id))
            {
                return albums[id];
            }
            var album = spotify.GetAlbum(id);
            if (album != null)
            {
                albums.Add(album.ID, album);
            }
            return album;
        }


        public IList<IAlbum> GetAlbumsFromID(IList<string> ids)
        {
            IList<IAlbum> albumsFromID = new List<IAlbum>();
            foreach (var each in ids)
            {
                var albumFromID = GetAlbumFromID(each);
                if (albumFromID != null)
                {
                    albumsFromID.Add(albumFromID);
                }
            }
            return albumsFromID;
        }
    }
}
