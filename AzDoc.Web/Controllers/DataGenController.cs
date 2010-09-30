using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AzDoc.Web.Entities;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.StorageClient;

namespace AzDoc.Web.Controllers
{
    public class DataGenController : Controller
    {
        private static readonly Dictionary<char, CloudBlobContainer> Containers = InitializeContainers();
        //
        // GET: /DataGen/

        public ActionResult Index()
        {
            GenerateBlobs();
            ViewData["Message"] = "Done";
            return View();
        }

        private static void GenerateBlobs()
        {
            var client = CloudStorageAccount.Parse(RoleEnvironment.GetConfigurationSettingValue("StorageAccount")).CreateCloudTableClient();
            client.CreateTableIfNotExist("documentindex");
            var context = client.GetDataServiceContext();
            
            for (int i = 0; i < 1000; i++)
            {
                var guid = Guid.NewGuid().ToString();
                var ch = guid.ToLower().Last();
                var blob = Containers[ch].GetBlobReference(guid);
                blob.UploadText(guid, Encoding.UTF8, new BlobRequestOptions());
                context.AddObject("documentindex", new DocumentIndex(guid));
            }

            context.SaveChanges();
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
