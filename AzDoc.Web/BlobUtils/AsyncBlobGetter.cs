using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.WindowsAzure.StorageClient;

namespace AzDoc.Web.BlobUtils
{
    public class AsyncBlobGetter
    {
        private readonly CloudBlob _blob;
        private readonly MemoryStream _stream = new MemoryStream();
        private readonly Action<string> _callback;

        public AsyncBlobGetter(CloudBlob blob, Action<string> callback)
        {
            _blob = blob;
            _callback = callback;
        }

        public void Get()
        {
            _blob.BeginDownloadToStream(_stream, Callback, null);
        }
        
        private void Callback(IAsyncResult asyncResult)
        {
            string text;

            using (_stream)
            {
                _blob.EndDownloadToStream(asyncResult);
                text = Encoding.UTF8.GetString(_stream.GetBuffer());
            }

            _callback.BeginInvoke(text, _callback.EndInvoke, null);
        }                              
    }
}