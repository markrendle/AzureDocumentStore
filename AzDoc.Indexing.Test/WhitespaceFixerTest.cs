using AzDoc.Indexing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AzDoc.Indexing.Test
{
    
    
    /// <summary>
    ///This is a test class for WhitespaceFixerTest and is intended
    ///to contain all WhitespaceFixerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WhitespaceFixerTest
    {
        /// <summary>
        ///A test for Fix
        ///</summary>
        [TestMethod()]
        public void FixTest()
        {
            const string source = @"  The quick    brown
fox          ";
            const string expected = "The quick brown fox";
            string actual = WhitespaceFixer.Fix(source);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void FixParenthesesTest()
        {
            const string source = @"(Hello)";
            const string expected = "( Hello )";
            string actual = WhitespaceFixer.Fix(source);
            Assert.AreEqual(expected, actual);
        }
    }
}
