using Orange.Indexing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Orange.Indexing.Test
{
    
    
    /// <summary>
    ///This is a test class for StringExtractorTest and is intended
    ///to contain all StringExtractorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StringExtractorTest
    {
        /// <summary>
        ///A test for GetValue
        ///</summary>
        [TestMethod()]
        public void SingleStringTest()
        {
            string source = @"Foo eq 'Bar'";
            var target = new StringExtractor(source);
            Assert.AreEqual("Foo eq ?0", target.ModifedString);
            Assert.AreEqual("Bar", target.GetValue("?0"));
        }

        /// <summary>
        ///A test for GetValue
        ///</summary>
        [TestMethod()]
        public void TwoStringsTest()
        {
            string source = @"Foo eq 'Bar' and Quux eq 'Wibble'";
            var target = new StringExtractor(source);
            Assert.AreEqual("Foo eq ?0 and Quux eq ?1", target.ModifedString);
            Assert.AreEqual("Bar", target.GetValue("?0"));
            Assert.AreEqual("Wibble", target.GetValue("?1"));
        }

        /// <summary>
        ///A test for GetValue
        ///</summary>
        [TestMethod()]
        public void EscapedQuoteTest()
        {
            string source = @"Foo eq 'B\'ar'";
            var target = new StringExtractor(source);
            Assert.AreEqual("Foo eq ?0", target.ModifedString);
            Assert.AreEqual(@"B'ar", target.GetValue("?0"));
        }

        /// <summary>
        ///A test for GetValue
        ///</summary>
        [TestMethod()]
        public void EscapedQuotesAtEndTest()
        {
            string source = @"Foo eq '\'Bar\''";
            var target = new StringExtractor(source);
            Assert.AreEqual("Foo eq ?0", target.ModifedString);
            Assert.AreEqual(@"'Bar'", target.GetValue("?0"));
        }

        /// <summary>
        ///A test for GetValue
        ///</summary>
        [TestMethod()]
        public void NothingButEscapedQuotesTest()
        {
            string source = @"Foo eq '\'\'\'\''";
            var target = new StringExtractor(source);
            Assert.AreEqual("Foo eq ?0", target.ModifedString);
            Assert.AreEqual(@"''''", target.GetValue("?0"));
        }
    }
}
