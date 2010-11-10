using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AzDoc.Documents;

namespace AzDoc.Indexing
{
    public class Indexer
    {
        private readonly string _indexText;

        public Indexer(string indexText)
        {
            _indexText = indexText;
        }

        public string IndexText
        {
            get { return _indexText; }
        }

        public IEnumerable<IndexEntry> Index(Document document)
        {
            return IndexParser.GetEqExpressions(_indexText)
                .Select(eqExpression => document.FindValues(eqExpression.Item1.FullPath).ToStrings())
                .CartesianProduct()
                .Select(combination => new IndexEntry(combination));
        }
    }
}
