using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Orange.Documents;
using Orange.Indexing;

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

        [HttpPost]
        public ActionResult Create()
        {
            try
            {
                var document = Document.Load(HttpContext.Request.InputStream);
                var builder = new StringBuilder();
                foreach (var entry in DefaultIndexer.Index(document))
                {
                    builder.AppendLine(entry.EqualityPart);
                }
                return Content(builder.ToString(), "text/text");
            }
            catch (Exception ex)
            {
                return Content(ex.Message, "text/text");
            }
        }
    }
}
