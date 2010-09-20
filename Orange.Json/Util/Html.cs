using System;

namespace Orange.Json.Util
{
    public class Html
    {
        public static string StripHtmlTags(string value)
        {
            return value.RegexReplace("<p>", Environment.NewLine)
                .RegexReplace("<.*?>", "")
                .RegexReplace(@"^\s+", "")
                .Replace("\\n", "")
                .Replace("\\r", "").TrimEnd(' ');
        }
    }
}
