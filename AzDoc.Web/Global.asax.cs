using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AzDoc.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "DocumentPost",
                "data/{collection}",
                new {controller = "Documents", action = "Create"},
                new {httpMethod = new HttpMethodConstraint("POST")}
                );

            routes.MapRoute(
                "DocumentGet",
                "data/{collection}/{id}",
                new { controller = "Documents", action = "Get", id = UrlParameter.Optional },
                new { httpMethod = new HttpMethodConstraint("GET") }
                );

            routes.MapRoute(
                "DocumentPut",
                "data/{collection}/{id}",
                new { controller = "Documents", action = "Put" },
                new { httpMethod = new HttpMethodConstraint("PUT") }
                );

            routes.MapRoute(
                "DocumentDelete",
                "data/{collection}/{id}",
                new { controller = "Documents", action = "Delete" },
                new { httpMethod = new HttpMethodConstraint("DELETE") }
                );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}