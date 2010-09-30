using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using Microsoft.WindowsAzure.StorageClient;

namespace AzDoc.Web.BlobUtils
{
    public class MultiBlobGetter : IObservable<string>
    {
        private readonly List<IObserver<string>> _observers = new List<IObserver<string>>();
        private int _remaining;

        public void GetBlobTextAsync(CloudBlob blob)
        {
            new AsyncBlobGetter(blob, GotCallback).Get();
            Interlocked.Increment(ref _remaining);
        }

        private void GotCallback(string text)
        {
            Interlocked.Decrement(ref _remaining);
            foreach (var observer in _observers)
            {
                observer.OnNext(text);
                if (_remaining <= 0)
                {
                    observer.OnCompleted();
                }
            }
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            _observers.Add(observer);
            return new DisposeAction(() => _observers.Remove(observer));
        }
    }
}