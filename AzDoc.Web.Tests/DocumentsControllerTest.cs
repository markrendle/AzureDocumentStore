using System.IO;
using System.Text;
using System.Web;
using System.Web.Routing;
using AzDoc.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web.Mvc;

namespace AzDoc.Web.Tests
{
    
    
    /// <summary>
    ///This is a test class for DocumentsControllerTest and is intended
    ///to contain all DocumentsControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DocumentsControllerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


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
            var target = new DocumentsController();
            const string json = @"{""LastName"":""Smith"", ""Addresses"":[{""Postcode"":""P1""},{""Postcode"":""P2""}]}";
            target.ControllerContext = new ControllerContext
                                           {
                                               Controller = target,
                                               RequestContext =
                                                   new RequestContext(new MockHttpContext(json), new RouteData())
                                           };

            const string collection = "posts";

            const string expected = @"Collection: posts
Smith#P1
Smith#P2
";
            var actual = target.Post(collection) as ContentResult;
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected, actual.Content);
        }

        private class MockHttpContext : HttpContextBase
        {
            private readonly HttpRequestBase _request;
            public MockHttpContext(string postData)
            {
                _request = new MockHttpRequest(postData);
            }

            public override HttpRequestBase Request
            {
                get
                {
                    return _request;
                }
            }
        }

        private class MockHttpRequest : HttpRequestBase
        {
            public MockHttpRequest(string inputStreamData)
            {
                _inputStream = new MemoryStream(Encoding.UTF8.GetBytes(inputStreamData));
            }
            private readonly MemoryStream _inputStream;
            public override Stream InputStream
            {
                get
                {
                    return _inputStream;
                }
            }
        }
    }
}
