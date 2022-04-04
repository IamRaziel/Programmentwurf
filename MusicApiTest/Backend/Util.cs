using MusicApi.Backend.Music;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApiTest.Backend
{
    public static class Util
    {
        public static IList<T> Sublist<T>(this IList<T> list, int start, int count)
        {
            IList<T> sublist = new List<T>();
            for (int i = start; i < start + count; i++)
            {
                if (i < list.Count)
                {
                    sublist.Add(list[i]);
                }
            }
            return sublist;
        }

        public static void CheckTracks(IList<ITrack> tracks, IList<ITrack> tracksToCheck)
        {
            int tCount = tracks.Count;
            int ttcCount = tracksToCheck.Count;
            bool failed = false;
            StringBuilder output = new StringBuilder();
            if (tCount != ttcCount)
            {
                failed = true;
                output.AppendLine("The Lists arent the same length. " + tracks.Count + " != " + tracksToCheck.Count);
            }

            for (int i = 0; i < tCount; i++)
            {
                string tID = tracks[i].ID;
                if (i >= ttcCount)
                {
                    failed = true;
                    output.AppendLine("At position " + i + " the track with id: " + tID + " is missing.");
                }
                else
                {
                    string ttcID = tracksToCheck[i].ID;
                    if (!tID.Equals(ttcID))
                    {
                        output.AppendLine("At position " + i + " the tracks have different IDs \n" +
                            "      id:        " + tID + "\n" +
                            "      idToCheck: " + ttcID);
                    }
                }
            }

            if (ttcCount > tCount)
            {
                failed = true;
                output.AppendLine("The tracks are longer than they should.");
                for (int i = tCount; i < ttcCount; i++)
                {
                    string ttcID = tracksToCheck[i].ID;
                    output.AppendLine("The track with id: " + ttcID + " is not existing in the compare List.");
                }
            }

            if (failed)
            {
                Assert.Fail(output.ToString());
            }
        }

        public static void CheckPlaylists(IList<IPlaylist> playlists, IList<IPlaylist> playlistsToCheck)
        {
            int pCount = playlists.Count;
            int ptcCount = playlistsToCheck.Count;
            bool failed = false;
            StringBuilder output = new StringBuilder();
            if (pCount != ptcCount)
            {
                failed = true;
                output.AppendLine("The Lists arent the same length. " + playlists.Count + " != " + playlistsToCheck.Count);
            }

            for (int i = 0; i < pCount; i++)
            {
                string pID = playlists[i].ID;
                if (i >= ptcCount)
                {
                    failed = true;
                    output.AppendLine("At position " + i + " the playlist with id: " + pID + " is missing.");
                }
                else
                {
                    string ptcID = playlistsToCheck[i].ID;
                    if (!pID.Equals(ptcID))
                    {
                        output.AppendLine("At position " + i + " the playlists have different IDs \n" +
                            "      id:        " + pID + "\n" +
                            "      idToCheck: " + ptcID);
                    }
                }
            }

            if (ptcCount > pCount)
            {
                failed = true;
                output.AppendLine("The playlists are longer than they should.");
                for (int i = pCount; i < ptcCount; i++)
                {
                    string ptcID = playlistsToCheck[i].ID;
                    output.AppendLine("The playlist with id: " + ptcID + " is not existing in the compare List.");
                }
            }

            if (failed)
            {
                Assert.Fail(output.ToString());
            }
        }

        public static void CheckAlbums(IList<IAlbum> albums, IList<IAlbum> albumsToCheck)
        {
            int aCount = albums.Count;
            int atcCount = albumsToCheck.Count;
            bool failed = false;
            StringBuilder output = new StringBuilder();
            if (aCount != atcCount)
            {
                failed = true;
                output.AppendLine("The Lists arent the same length. " + albums.Count + " != " + albumsToCheck.Count);
            }

            for (int i = 0; i < aCount; i++)
            {
                string aID = albums[i].ID;
                if (i >= atcCount)
                {
                    failed = true;
                    output.AppendLine("At position " + i + " the album with id: " + aID + " is missing.");
                }
                else
                {
                    string atcID = albumsToCheck[i].ID;
                    if (!aID.Equals(atcID))
                    {
                        output.AppendLine("At position " + i + " the albums have different IDs \n" +
                            "      id:        " + aID + "\n" +
                            "      idToCheck: " + atcID);
                    }
                }
            }

            if (atcCount > aCount)
            {
                failed = true;
                output.AppendLine("The albums are longer than they should.");
                for (int i = aCount; i < atcCount; i++)
                {
                    string ptcID = albumsToCheck[i].ID;
                    output.AppendLine("The albums with id: " + ptcID + " is not existing in the compare List.");
                }
            }

            if (failed)
            {
                Assert.Fail(output.ToString());
            }
        }
    }
}
