using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.StorageClient;

namespace AzDoc.Web.Extensions
{
    public static class CloudBlobExtensions
    {
        private static readonly Func<CloudBlob, string> DownloadFunc = DownloadText;

        public static IAsyncResult BeginDownloadText(this CloudBlob blob)
        {
            return DownloadFunc.BeginInvoke(blob, null, null);
        }

        public static string EndDownloadText(IAsyncResult asyncResult)
        {
            return DownloadFunc.EndInvoke(asyncResult);
        }

        private static string DownloadText(CloudBlob blob)
        {
            return blob.DownloadText();
        }
    }
}