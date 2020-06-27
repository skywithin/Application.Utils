using System;
using System.Collections.Generic;

namespace Application.Utils
{
    public static class EnumerableExtensions
    {
        //Thanks https://stackoverflow.com/questions/489258/linqs-distinct-on-a-particular-property
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> src, Func<TSource, TKey> func)
        {
            HashSet<TKey> keys = new HashSet<TKey>();

            foreach (TSource elem in src)
            {
                if (keys.Add(func(elem)))
                {
                    yield return elem;
                }
            }
        }
    }
}
