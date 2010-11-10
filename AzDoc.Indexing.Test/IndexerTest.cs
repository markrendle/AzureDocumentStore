using System.Collections.Generic;
using System.Linq;
using AzDoc.Documents;
using AzDoc.Indexing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AzDoc.Indexing.Test
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

        /// <summary>
        ///A test for Indexer With Sub Things
        ///</summary>
        [TestMethod()]
        public void IndexerSingleAttributeAndListTest()
        {
            // Arrange
            const string indexText = "LastName eq '?' and Addresses/Postcode eq '?'";
            var document = Document.Parse(@"{""FirstName"":""Bob"",""LastName"":""Smith"",
""Addresses"":[{""Postcode"":""P1""},{""Postcode"":""P2""}]
}");

            // Act
            var entries = new Indexer(indexText).Index(document);

            // Assert
            Assert.AreEqual(2, entries.Count());
            Assert.AreEqual(1, entries.Count(e => e.EqualityPart == "Smith#P1"));
            Assert.AreEqual(1, entries.Count(e => e.EqualityPart == "Smith#P2"));
        }
    }
}
