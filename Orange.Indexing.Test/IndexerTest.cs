using System.Collections.Generic;
using System.Linq;
using Orange.Documents;
using Orange.Indexing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Orange.Indexing.Test
{
    
    
    /// <summary>
    ///This is a test class for IndexerTest and is intended
    ///to contain all IndexerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IndexerTest
    {
        /// <summary>
        ///A test for Indexer Constructor
        ///</summary>
        [TestMethod()]
        public void IndexerSingleAttributeSingleExpressionTest()
        {
            const string indexText = "Name eq '?'";
            var target = new Indexer(indexText);
            var document = Document.Parse(@"{""Name"":""Bob""}");
            IEnumerable<IndexEntry> entries = target.Index(document);
            Assert.AreEqual(1, entries.Count());
            IndexEntry entry = entries.First();
            Assert.AreEqual("Bob", entry.EqualityPart);
        }

        /// <summary>
        ///A test for Indexer Constructor
        ///</summary>
        [TestMethod()]
        public void IndexerSingleAttributeDoubleExpressionTest()
        {
            const string indexText = "FirstName eq '?' and LastName eq '?'";
            var target = new Indexer(indexText);
            var document = Document.Parse(@"{""FirstName"":""Bob"",""LastName"":""Smith""}");
            IEnumerable<IndexEntry> entries = target.Index(document);
            Assert.AreEqual(1, entries.Count());
            IndexEntry entry = entries.First();
            Assert.AreEqual("FirstName:Bob;LastName:Smith", entry.EqualityPart);
        }
    }
}
