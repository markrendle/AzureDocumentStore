using System;
using System.Collections.Generic;
using System.Dynamic;

namespace Orange.Json.Util
{
    public static class DictionaryToExpandoEx
    {
        public static ExpandoObject ToExpandoObject(this IDictionary<string, object> dictionary)
        {
            return ToExpandoObject(dictionary, str => str);
        }

        public static ExpandoObject ToExpandoObject(this IDictionary<string, object> dictionary, Func<string, string> keyProjector)
        {
            var eo = new ExpandoObject() as IDictionary<string, object>;

            foreach (var kvp in dictionary)
            {
                var subDict = kvp.Value as IDictionary<string, object>;

                if (subDict != null)
                {
                    eo.Add(keyProjector(kvp.Key), subDict.ToExpandoObject(keyProjector));
                }
                else
                {
                    eo.Add(keyProjector(kvp.Key), kvp.Value);
                }
            }

            return eo as ExpandoObject;
        }
    }
}
