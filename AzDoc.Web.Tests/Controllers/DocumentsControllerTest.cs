using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using AzDoc.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

namespace AzDoc.Web.Tests.Controllers
{
    
    
    /// <summary>
    ///This is a test class for DocumentsControllerTest and is intended
    ///to contain all DocumentsControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DocumentsControllerTest
    {
        private static DocumentsController CreateController(string data)
        {
            var controller = new DocumentsController();
            controller.ControllerContext = new ControllerContext
            {
                Controller = controller,
                RequestContext =
                    new RequestContext(new MockHttpContextWithInputStream(data), new RouteData())
            };
            return controller;
        }

        /// <summary>
        ///A test for Post
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        public void PostTest()
        {
            // Arrange
            var target =
                CreateController(
                    @"{""LastName"":""Smith"", ""Addresses"":[{""Postcode"":""P1""},{""Postcode"":""P2""}]}");
            const string collection = "posts";

            // Act
            var actual = target.Post(collection) as ContentResult;

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Content.Lines().Contains("Collection: posts"));
            Assert.IsTrue(actual.Content.Lines().Contains("Smith#P1"));
            Assert.IsTrue(actual.Content.Lines().Contains("Smith#P2"));
        }
    }

    static class StringEx
    {
        public static IEnumerable<string> Lines(this string str)
        {
            return str.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries).AsEnumerable();
        }
    }
}