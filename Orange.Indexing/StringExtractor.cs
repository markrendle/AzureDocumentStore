using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Orange.Indexing
{
    internal class StringExtractor
    {
        private readonly string _source;
        private readonly object _sync = new object();
        private string _modifiedString;
        private Dictionary<string, string> _values;

        public StringExtractor(string source)
        {
            _source = source;
        }

        public string ModifedString
        {
            get
            {
                if (_modifiedString == null)
                {
                    Run();
                }

                return _modifiedString;
            }
        }

        public string GetValue(string name)
        {
            return _values[name];
        }

        private void Run()
        {
            lock (_sync)
            {
                _values = new Dictionary<string, string>();
                _modifiedString = Regex.Replace(_source, @"'(?:[^'\\]|\\.)*'", new MatchEvaluator(ExtractString));
            }
        }

        private string ExtractString(Match match)
        {
            var name = "?" + _values.Count;
            var value = RemoveEnds(match.Value.Replace(@"\'", "'"));
            _values.Add(name, value);
            return name;
        }

        private static string RemoveEnds(string source)
        {
            return source.Substring(1, source.Length - 2);
        }
    }
}
