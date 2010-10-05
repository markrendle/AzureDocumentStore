using System.IO;
using System.Text;
using System.Web;

namespace AzDoc.Web.Tests
{
    public class MockHttpContextWithInputStream : HttpContextBase
    {
        private readonly HttpRequestBase _request;
        public MockHttpContextWithInputStream(string postData)
        {
            _request = new MockHttpRequestWithInputStream(postData);
        }

        public override HttpRequestBase Request
        {
            get
            {
                return _request;
            }
        }

        private class MockHttpRequestWithInputStream : HttpRequestBase
        {
            public MockHttpRequestWithInputStream(string inputStreamData)
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