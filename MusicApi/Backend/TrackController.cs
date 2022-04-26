using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;
using MusicApi.Backend.Music;
using MusicApi.Backend.SourceApi.Database;

namespace MusicApi.Backend
{
    public class TrackController
    {
        private IDictionary<string, ITrack> tracks = new Dictionary<string, ITrack>();
        private IDBConnection db;
        private IFileWriter fileWriter;

        public TrackController(IDBConnection dbConnection, IFileWriter fileWriter)
        {
            db = dbConnection;
            this.fileWriter = fileWriter;
        }

        public void AddTracks(IList<ITrack> tracks)
        {
            if (tracks != null)
            {
                foreach (var each in tracks)
                {
                    if (each != null)
                    {
                        this.tracks.Add(each.ID, each);
                    }
                }
            }
        }

        public bool DeleteTrack(string id)
        {
            if (tracks.ContainsKey(id))
            {
                var track = tracks[id];
                if (tracks.Remove(id))
                {
                    if (db.RemoveTrack(id))
                    {
                        return true;
                    }
                    tracks.Add(id, track);
                }
            }
            return false;
        }

        public ITrack GetTrackFromID(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            ITrack track = null;
            if (!tracks.TryGetValue(id, out track)) return null;
            return track;
        }


        //Needs to be changed to GetTrackFromUrl(string url)
        public ITrack GetTrackFromUrl(string id)
        {
            //TODO
            if (tracks.ContainsKey(id))
            {
                return tracks[id];
            }
            var api = BackendController.GetApiFromUrl("");
            var track = api.GetTrack(id);
            if (track != null)
            {
                tracks.Add(track.ID, track);
            }
            return track;
        }

        public IList<ITrack> GetTracksFromID(IList<string> ids)
        {
            IList<ITrack> tracksFromID = new List<ITrack>();
            foreach (var each in ids)
            {
                var trackFromID = GetTrackFromID(each);
                if (trackFromID != null)
                {
                    tracksFromID.Add(trackFromID);
                }
            }
            return tracksFromID;
        }

        public bool UploadTrack(IFormFile file)
        {
            try
            {
                string name = file.FileName.Replace(@"\\\\", @"\\");

                if (file.Length > 0)
                {
                    var memoryStream = new MemoryStream();

                    try
                    {
                        file.CopyTo(memoryStream);

                        // Upload check if less than 2mb!
                        if (memoryStream.Length < 2097152)
                        {
                            fileWriter.Write(memoryStream.ToArray(), Path.GetFileName(name));
                        }
                    }
                    finally
                    {
                        memoryStream.Close();
                        memoryStream.Dispose();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }
    }
}
