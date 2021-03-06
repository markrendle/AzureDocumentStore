﻿using System.Collections.Generic;
using AzDoc.Documents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace AzDoc.Documents.Test
{
    /// <summary>
    ///This is a test class for DocumentTest and is intended
    ///to contain all DocumentTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ParsingTest
    {
        /// <summary>
        ///A test for Load
        ///</summary>
        [TestMethod()]
        public void ParseAttributesOnlyTest()
        {
            var actual = Document.Parse(Properties.Resources.JsonDocumentWithAttributesOnly);
            Assert.AreEqual("Mark", actual.GetAttribute("FirstName"));
        }

        [TestMethod]
        public void ParseSubDocumentTest()
        {
            var actual = Document.Parse(Properties.Resources.JsonDocumentWithSubDocument);
            Document subDocument = actual.GetSubDocument("Sub");
            Assert.AreEqual("One", subDocument.GetAttribute("StringOne"));
            Assert.AreEqual(1, subDocument.GetAttribute("NumberOne"));
        }

        [TestMethod]
        public void ParseSubDocumentListTest()
        {
            var actual = Document.Parse(Properties.Resources.JsonDocumentWithSubDocumentList);
            var subDocuments = actual.GetDocumentList("Subs").ToArray();
            Assert.AreEqual("One", subDocuments[0].GetAttribute("String"));
            Assert.AreEqual(1, subDocuments[0].GetAttribute("Number"));
            Assert.AreEqual("Two", subDocuments[1].GetAttribute("String"));
            Assert.AreEqual(2, subDocuments[1].GetAttribute("Number"));
        }

        [TestMethod]
        public void ParseListTest()
        {
            var actual = Document.Parse(Properties.Resources.JsonDocumentWithList);
            var list = actual.GetList("List").ToArray();
            Assert.AreEqual("Zero", list[0]);
            Assert.AreEqual("One", list[1]);
        }
    }
}
