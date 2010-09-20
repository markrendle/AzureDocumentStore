using System;

namespace Orange.Json
{
    internal class ValueToken : Token
    {
        private readonly object _value;

        public ValueToken(string value)
        {
            if (value.Equals("true", StringComparison.InvariantCultureIgnoreCase)) _value = true;
            else if (value.Equals("false", StringComparison.InvariantCultureIgnoreCase)) _value = false;
            else if (value.Contains("."))
            {
                double d;
                if (!double.TryParse(value, out d)) throw new ArgumentException("value");
                _value = d;
            }
            else
            {
                long l;
                if (!long.TryParse(value, out l)) throw new ArgumentException("value");
                _value = (l > int.MinValue && l < int.MaxValue) ? (int)l : l;
            }
        }

        private ValueToken(string value, bool b)
        {
            _value = value;
        }

        public static ValueToken StringToken(string str)
        {
            return new ValueToken(str, true);
        }

        public object Value
        {
            get { return _value; }
        }
    }
}
