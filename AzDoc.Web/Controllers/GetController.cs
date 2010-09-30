using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using AzDoc.Web.BlobUtils;
using AzDoc.Web.Entities;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;
using AzDoc.Web.Extensions;

namespace AzDoc.Web.Controllers
{
    public class GetController : Controller
    {
        private static readonly Dictionary<char, CloudBlobContainer> Containers = InitializeContainers();
        //
        // GET: /Get/

        public ActionResult Index()
        {
            var stopwatch = Stopwatch.StartNew();

            var builder = new StringBuilder();

            foreach (var result in DoDownloads())
            {
                builder.AppendFormat("{0} {1}", stopwatch.Elapsed, result);
                builder.Append("<br />");
            }

            ViewData["Blobs"] = builder.ToString();

            stopwatch.Stop();
            ViewData["Time"] = stopwatch.Elapsed;

            return View();
        }

        private static IEnumerable<string> DoDownloads()
        {
            var getter = new MultiBlobGetter();
            var list = getter.ToEnumerable();
            foreach (var guid in GetGuids())
            {
                var container = Containers[guid.Last()];
                var blob = container.GetBlobReference(guid);
                getter.GetBlobTextAsync(blob);
            }
            return list;
        }

        private static void DownloadCallback(IAsyncResult asyncResult)
        {
            var tuple = (Tuple<CloudBlob, MemoryStream, ConcurrentBag<string>>)asyncResult.AsyncState;
            tuple.Item1.EndDownloadToStream(asyncResult);
            tuple.Item3.Add(Encoding.UTF8.GetString(tuple.Item2.GetBuffer()));
        }

        private static IEnumerable<CloudBlob> GetBlobs()
        {
            var client = CloudStorageAccount.DevelopmentStorageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference("testdata");

            return container.ListBlobs()
                .Take(100)
                .Select(blob => container.GetBlobReference(blob.Uri.ToString()));
        }

        private static IEnumerable<string> GetGuids()
        {
            var client = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageAccount")).CreateCloudTableClient();
            var context = client.GetDataServiceContext();
            return context.CreateQuery<DocumentIndex>("documentindex").Take(100).AsEnumerable().Select(di => di.PartitionKey);
        }

        private static Dictionary<char, CloudBlobContainer> InitializeContainers()
        {
            var dict = new Dictionary<char, CloudBlobContainer>();

            var client = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageAccount")).CreateCloudBlobClient();

            foreach (var ch in new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' })
            {
                var container = client.GetContainerReference("testdata" + ch);
                container.CreateIfNotExist();
                dict.Add(ch, container);
            }

            return dict;
        }
    }
}
