using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AzDoc.Web.Controllers
{
    public class DocumentsController : Controller
    {
        //
        // GET: /Documents/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create()
        {
            using (var reader = new StreamReader(HttpContext.Request.InputStream))
            {
                return Content(reader.ReadToEnd(), "text/text", Encoding.UTF8);
            }
        }

    }
}
