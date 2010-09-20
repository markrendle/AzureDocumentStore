using System.Linq;
using System.Text.RegularExpressions;

namespace Orange.Json.Util
{
    public static class StringEx
    {
        public static string SnakeToPascal(string source)
        {
            return source.Split('_').Select(s => s.Substring(0, 1).ToUpper() + s.Substring(1)).Aggregate((s1, s2) => s1 + s2);
        }

        public static string MaxLength(this string source, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(source)) return string.Empty;

            if (source.Length <= maxLength) return source;

            return source.Substring(0, maxLength);
        }

        public static string Ellipsis(this string source, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(source)) return string.Empty;

            if (source.Length <= maxLength) return source;

            return source.Substring(0, maxLength) + "...";
        }

        public static string RegexReplace(this string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement);
        }
    }
}
