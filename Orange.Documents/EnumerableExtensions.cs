using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orange.Documents
{
    static class EnumerableExtensions
    {
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, T itemToAppend)
        {
            foreach (var item in source)
            {
                yield return item;
            }

            yield return itemToAppend;
        }
    }
}
