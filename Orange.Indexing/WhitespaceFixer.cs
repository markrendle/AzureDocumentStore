using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Orange.Indexing
{
    class WhitespaceFixer
    {
        private static readonly Regex SingleSpace = new Regex(@"\s+");

        public static string Fix(string source)
        {
            source = source.Replace("(", " ( ").Replace(")", " ) ").Trim();
            return SingleSpace.Replace(source, " ");
        }
    }
}
