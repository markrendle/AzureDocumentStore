using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzDoc.Indexing
{
    public class IndexEntry
    {
        private readonly string[] _equalityPart;

        public IndexEntry(IEnumerable<string> equalityParts)
        {
            _equalityPart = equalityParts.ToArray();
        }

        public string[] EqualityPart
        {
            get { return _equalityPart; }
        }
    }
}
