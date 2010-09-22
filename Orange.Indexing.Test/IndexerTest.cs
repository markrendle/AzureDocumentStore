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
            // Arrange
            const string indexText = "Name eq '?'";
            var document = Document.Parse(@"{""Name"":""Bob""}");

            // Act
            var entries = new Indexer(indexText).Index(document);
            
            // Assert
            Assert.AreEqual(1, entries.Count());
            var entry = entries.First();
            Assert.AreEqual("Bob", entry.EqualityPart);
        }

        /// <summary>
        ///A test for Indexer Constructor
        ///</summary>
        [TestMethod()]
        public void IndexerSingleAttributeDoubleExpressionTest()
        {
            // Arrange
            const string indexText = "FirstName eq '?' and LastName eq '?'";
            var document = Document.Parse(@"{""FirstName"":""Bob"",""LastName"":""Smith""}");

            // Act
            var entries = new Indexer(indexText).Index(document);

            // Assert
            Assert.AreEqual(1, entries.Count());
            var entry = entries.First();
            Assert.AreEqual("Bob#Smith", entry.EqualityPart);
        }
    }
}
