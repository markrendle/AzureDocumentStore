using System;
using System.Text;
using System.Collections.Generic;
using Orange.Json.Util;

namespace Orange.Json
{
    internal class JsonTokenizer
    {
        private readonly IEnumerator<char> _reader;

        private readonly DictionaryWithDefault<char, Func<Token>> _tokenizers;

        public JsonTokenizer()
        {
            _tokenizers = new DictionaryWithDefault<char, Func<Token>>(ReadValueToken)
                             {
                                 { '{', () => new StartObjectToken() },
                                 { '}', () => new EndObjectToken() },
                                 { '[', () => new StartListToken() },
                                 { ']', () => new EndListToken() },
                                 { '"', ReadStringToken }
                             };
        }
        
        public JsonTokenizer(IEnumerable<char> source) : this()
        {
            _reader = source.GetEnumerator();
        }

        public IEnumerable<Token> GetTokens()
        {
            while (_reader.MoveNext())
            {
                if (char.IsWhiteSpace(_reader.Current) || _reader.Current == ',' || _reader.Current == ':') continue;
                if (_reader.Current == '\r' || _reader.Current == '\n') continue;
                yield return _tokenizers[_reader.Current]();
                if (_reader.Current == '}') yield return new EndObjectToken();
            }
        }

        private ValueToken ReadStringToken()
        {
            var sb = new StringBuilder();
            bool backslash = false;

            while (_reader.MoveNext())
            {
                if (_reader.Current == '"')
                {
                    if (backslash)
                    {
                        sb.Append(_reader.Current);
                        backslash = false;
                    }
                    else
                    {
                        break;
                    }
                }
                else if (backslash)
                {
                    sb.Append('\\');
                    backslash = false;
                }
                else if (_reader.Current == '\\')
                {
                    backslash = true;
                    continue;
                }

                sb.Append(_reader.Current);
            }

            return ValueToken.StringToken(sb.ToString());
        }

        private ValueToken ReadValueToken()
        {
            var sb = new StringBuilder();
            sb.Append(_reader.Current);

            while (_reader.MoveNext() && !IsTerminator(_reader.Current))
            {
                sb.Append(_reader.Current);
            }

            return new ValueToken(sb.ToString());
        }

        private static bool IsTerminator(char c)
        {
            return c == ',' || c == ' ' || c == '}' || c == ']';
        }
    }
}
