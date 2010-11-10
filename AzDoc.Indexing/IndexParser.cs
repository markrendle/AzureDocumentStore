using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AzDoc.Documents;
using AzDoc.Indexing.Expressions;

namespace AzDoc.Indexing
{
    public class IndexParser
    {
        public static IEnumerable<Tuple<PropertyReference,string>> GetEqExpressions(string index)
        {
            var strings = new StringExtractor(index);
            var fix = WhitespaceFixer.Fix(strings.ModifedString);
            return GetBlocksOfThree(fix)
                .Where(tuple => tuple.Item2.Equals("eq", StringComparison.OrdinalIgnoreCase))
                .Select(tuple => Tuple.Create(new PropertyReference(tuple.Item1), tuple.Item3.StartsWith("?") ? strings.GetValue(tuple.Item3) : tuple.Item3));
        }

        private static IEnumerable<Tuple<string,string,string>> GetBlocksOfThree(string source)
        {
            var bits = source.Split(' ');
            for (int i = 0; i < bits.Length - 2; i++)
            {
                yield return Tuple.Create(bits[i], bits[i + 1], bits[i + 2]);
            }
        }
    }
}
