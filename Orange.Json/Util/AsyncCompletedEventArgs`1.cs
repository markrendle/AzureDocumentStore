using System;
using System.ComponentModel;

namespace Orange.Json.Util
{
    public class AsyncCompletedEventArgs<T> : AsyncCompletedEventArgs
    {
        private readonly T _value;

        public AsyncCompletedEventArgs(T value, object userState) : base(null, false, userState)
        {
            _value = value;
        }

        public T Value
        {
            get { return _value; }
        }
    }
}
