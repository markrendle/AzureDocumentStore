using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orange.Documents;

namespace Orange.Indexing
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
            var values = new List<string[]>();
            foreach (var eqExpression in IndexParser.GetEqExpressions(_indexText))
            {
                values.Add(document.FindValues(eqExpression.Item1.PropertyName).Select(o => o.ToString()).ToArray());
            }

            foreach (var s in values[0])
            {
                yield return new IndexEntry(s);
            }
        }
    }
}
