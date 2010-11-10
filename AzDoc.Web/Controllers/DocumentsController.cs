using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AzDoc.Web.Extensions;
using AzDoc.Documents;
using AzDoc.Indexing;

namespace AzDoc.Web.Controllers
{
    public class DocumentsController : Controller
    {
        private static readonly Indexer DefaultIndexer = new Indexer("LastName eq '?' and Addresses/Postcode eq '?'");
        //
        // GET: /Documents/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Get(string collection, string id)
        {
            return Content(string.IsNullOrWhiteSpace(id) ?
                string.Format("Collection: {0} (all)", collection)
                :
                string.Format("Collection: {0}, ID: {1}", collection, id)
                );
        }

        [HttpPost]
        public ActionResult Post(string collection)
        {
            var json = HttpContext.Request.InputStream.ReadToEnd();
            var document = TryParseDocument(json);
            collection = collection.ToLowerInvariant();

            var builder = new StringBuilder();
            builder.AppendLine("Collection: " + collection);
            foreach (var entry in DefaultIndexer.Index(document))
            {
                builder.AppendLine(string.Join("#", entry.EqualityPart));
            }
            return Content(builder.ToString(), "text/text");

        }

        private Document TryParseDocument(string json)
        {
            try
            {
                return Document.Parse(json);
            }
            catch (Exception)
            {
                throw new WebFaultException<string>("Data is not well-formed JSON", HttpStatusCode.BadGateway);
            }
        }

        [HttpPut]
        public ActionResult Put(string collection, string id)
        {
            return Content(string.Format("Collection: {0}, ID: {1}", collection, id));
        }

        [HttpDelete]
        public ActionResult Delete(string collection, string id)
        {
            return Content(string.Format("Collection: {0}, ID: {1}", collection, id));
        }
    }
}
