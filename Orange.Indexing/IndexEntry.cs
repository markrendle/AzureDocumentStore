using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orange.Indexing
{
    public class IndexEntry
    {
        private readonly string _equalityPart;

        public IndexEntry(string equalityPart)
        {
            _equalityPart = equalityPart;
        }

        public string EqualityPart
        {
            get { return _equalityPart; }
        }
    }
}
