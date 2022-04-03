using System;
using System.Collections.Generic;

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
    }
}
