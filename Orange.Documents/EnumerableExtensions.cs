using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orange.Documents
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, T itemToAppend)
        {
            foreach (var item in source)
            {
                yield return item;
            }

            yield return itemToAppend;
        }

        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> emptyProduct = new[] { Enumerable.Empty<T>() };
            return sequences.Aggregate(
              emptyProduct,
              (accumulator, sequence) =>
                from accseq in accumulator
                from item in sequence
                select accseq.Append(item));
        }

        public static IEnumerable<string> ToStrings<T>(this IEnumerable<T> source)
        {
            if (typeof(T) == typeof(string)) return (IEnumerable<string>)source;

            return source.Select(item => item.ToString());
        }
    }
}
