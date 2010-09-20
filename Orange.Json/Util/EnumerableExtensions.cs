using System;
using System.Linq;
using System.Collections.Generic;

namespace System.Linq
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, IEnumerable<T> appenda)
        {
            foreach (var item in source)
            {
                yield return item;
            }

            foreach (var item in appenda)
            {
                yield return item;
            }
        }
    }
}
