using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Orange.Json.Util
{
    public static class IDictionaryEx
    {
        public static T ValueOrDefault<T>(this IDictionary<string, object> dictionary, string key, T defaultValue)
        {
            object obj;

            if (dictionary.TryGetValue(key, out obj))
            {
                try
                {
                    return (T)Convert.ChangeType(obj, typeof(T), CultureInfo.InvariantCulture);
                }
                catch (InvalidCastException)
                {
                    Debug.WriteLine("InvalidCastException in IDictionaryEx.ValueOrDefault");
                    throw;
                }
            }

            return defaultValue;
        }
    }
}
