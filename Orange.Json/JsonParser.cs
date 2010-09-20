using System;
using System.Collections.Generic;
using System.IO;
using Orange.Json;
using Orange.Library.Json;

namespace Orange.Json
{
    public class JsonParser
    {
        public static IDictionary<string, object> Parse(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return Parse(reader.ReadToEnd());
            }
        }

        public static IDictionary<string, object> Parse(string source)
        {
            var tokens = new JsonTokenizer(source).GetTokens().GetEnumerator();
            tokens.MoveNext();
            if (tokens.Current is StartListToken) throw new ParseException("Source is a List");
            return ObjectFromTokens(tokens);
        }

        private static IDictionary<string, object> ObjectFromTokens(IEnumerator<Token> tokens)
        {
            var json = new Dictionary<string, object>();

            if (tokens.Current is StartObjectToken)
            {
                tokens.MoveNext();
            }

            while (!(tokens.Current is EndObjectToken))
            {
                var key = tokens.Current as ValueToken;

                if (key == null) throw new ParseException(string.Format("Unexpected {0}: expected ValueToken", tokens.Current.GetType().Name));

                if (!tokens.MoveNext()) break;

                if (tokens.Current is EndObjectToken) break;

                object result;

                if (tokens.Current is StartObjectToken)
                {
                    result = ObjectFromTokens(tokens);
                }
                else if (tokens.Current is StartListToken)
                {
                    result = ListFromTokens(tokens);
                }
                else
                {
                    result = ValueFromTokens(tokens);
                }

                try
                {
                    json.Add((string)key.Value, result);
                }
                catch (ArgumentException)
                {
                    throw;
                }
            }

            tokens.MoveNext();

            return json;
        }

        private static object ValueFromTokens(IEnumerator<Token> tokens)
        {
            var value = tokens.Current as ValueToken;
            if (value == null) throw new ParseException(string.Format("Unexpected {0}: expected ValueToken", tokens.Current.GetType().Name));
            tokens.MoveNext();
            return value.Value;
        }

        private static List<object> ListFromTokens(IEnumerator<Token> tokens)
        {
            var list = new List<object>();
            tokens.MoveNext();
            while (!(tokens.Current is EndListToken))
            {
                if (tokens.Current is StartObjectToken)
                {
                    list.Add(ObjectFromTokens(tokens));
                }
                else if (tokens.Current is StartListToken)
                {
                    list.Add(ListFromTokens(tokens));
                }
                else
                {
                    list.Add(ValueFromTokens(tokens));
                }
            }
            tokens.MoveNext();
            return list;
        }
    }
}
