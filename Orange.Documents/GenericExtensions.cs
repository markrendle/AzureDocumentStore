using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orange.Documents
{
    internal static class GenericExtensions
    {
        public static bool In<T>(this T item, params T[] args)
        {
            return args.Contains(item);
        }
    }
}
