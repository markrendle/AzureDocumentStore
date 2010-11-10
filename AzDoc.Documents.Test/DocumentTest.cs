using System.Linq;
using AzDoc.Documents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AzDoc.Documents.Test
{
    
    
    /// <summary>
    ///This is a test class for DocumentTest and is intended
    ///to contain all DocumentTest Unit Tests
    ///</summary>
    // ReSharper disable InconsistentNaming
    [TestClass()]
    public class DocumentTest
    {
        /// <summary>
        ///A test for GetAttribute
        ///</summary>
        [TestMethod()]
        public void GetSetAttributeTest()
        {
            var target = new Document();
            const string name = "foo";
            object expected = "bar";
            target.SetAttribute(name, expected);
            var actual = target.GetAttribute(name);
            Assert.AreEqual(expected, actual, "GetAttributeReturnedDifferentValueFromSet");
        }

        /// <summary>
        ///A test for GetAttribute
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetAttribute_WhenNotSet_ShouldThrowIndexOutOfRangeException()
        {
            var target = new Document();
            const string name = "foo";
            target.GetAttribute(name);
        }

        /// <summary>
        ///A test for GetDocumentList
        ///</summary>
        [TestMethod()]
        public void GetDocumentListTest()
        {
            var target = new Document();
            const string name = "foo";
            var actual = target.GetDocumentList(name);
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for GetList
        ///</summary>
        [TestMethod()]
        public void GetListTest()
        {
            var target = new Document();
            const string name = "foo";
            var actual = target.GetList(name);
            Assert.IsNotNull(actual);
            Assert.AreEqual(0, actual.Count);
        }

        /// <summary>
        ///A test for GetSubDocument
        ///</summary>
        [TestMethod()]
        public void GetSubDocumentTest()
        {
            var target = new Document();
            const string name = "foo";
            var actual = target.GetSubDocument(name);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for SetAttribute
        ///</summary>
        [TestMethod()]
        public void SetAttributeTest()
        {
            var target = new Document();
            const string name = "foo";
            object value = "bar";
            target.SetAttribute(name, value);
            Assert.AreEqual(value, target.GetAttribute(name));
        }

        /// <summary>
        ///A test for SetDocumentList
        ///</summary>
        [TestMethod()]
        public void SetDocumentListTest()
        {
            var target = new Document();
            const string name = "foo";
            var list = new List<Document>();
            target.SetDocumentList(name, list);
            Assert.AreEqual(list, target.GetDocumentList(name));
        }

        /// <summary>
        ///A test for SetList
        ///</summary>
        [TestMethod()]
        public void SetListTest()
        {
            var target = new Document();
            const string name = "foo";
            var list = new List<object>();
            target.SetList(name, list);
            Assert.AreEqual(list, target.GetList(name));
        }

        /// <summary>
        ///A test for SetSubDocument
        ///</summary>
        [TestMethod()]
        public void SetSubDocumentTest()
        {
            var target = new Document();
            const string name = "foo";
            var document = new Document();
            target.SetSubDocument(name, document);
            Assert.AreEqual(document, target.GetSubDocument(name));
        }

        [TestMethod]
        public void GetAttributeNamesTest()
        {
            var target = new Document();
            const string name = "foo";
            target.SetAttribute(name, "");
            Assert.AreEqual(name, target.GetAttributeNames().Single());
        }

        [TestMethod]
        public void GetSubDocumentNamesTest()
        {
            var target = new Document();
            const string name = "foo";
            target.SetSubDocument(name, new Document());
            Assert.AreEqual(name, target.GetSubDocumentNames().Single());
        }

        [TestMethod]
        public void GetListNamesTest()
        {
            var target = new Document();
            const string name = "foo";
            target.SetList(name, new List<object>());
            Assert.AreEqual(name, target.GetListNames().Single());
        }

        [TestMethod]
        public void GetDocumentListNamesTest()
        {
            var target = new Document();
            const string name = "foo";
            target.SetDocumentList(name, new List<Document>());
            Assert.AreEqual(name, target.GetDocumentListNames().Single());
        }

        [TestMethod]
        public void FindValues_WithAttributeName_ShouldReturnAttributeValue()
        {
            var target = new Document();
            const string name = "foo";
            const string value = "bar";
            object expected = new Tuple<string,object>(name,value);
            target.SetAttribute(name, value);
            var actual = target.FindValues(name).Single();
            Assert.AreEqual(expected, actual, "GetAttributeReturnedDifferentValueFromSet");
   
        }

        [TestMethod]
        public void FindValues_WithTwoPartPath_ShouldReturnSubDocumentAttributeValue()
        {
            var target = new Document();
            const string value = "bar";
            object expected = new Tuple<string,object>("foo/foo","bar");
            target.GetSubDocument("foo").SetAttribute("foo", value);
            var actual = target.FindValues("foo/foo").Single();
            Assert.AreEqual(expected, actual, "GetAttributeReturnedDifferentValueFromSet");

        }

        [TestMethod]
        public void FindValues_WithThreePartPath_ShouldReturnAttributeValue()
        {
            var target = new Document();
            object expected = new Tuple<string,object>("foo/bar/quux","baz");
            target.GetSubDocument("foo").GetSubDocument("bar").SetAttribute("quux", "baz");
            var actual = target.FindValues("foo/bar/quux").Single();
            Assert.AreEqual(expected, actual, "GetAttributeReturnedDifferentValueFromSet");
        }

        //[TestMethod]
        //public void FindMultipleValues_WithList_ShouldReturnList()
        //{
        //    var target = new Document();
        //    const string name = "foo";
        //    target.GetList(name).Add("foo");
        //    target.GetList(name).Add("bar"});
        //    var actual = target.FindValues("foo").ToArray();
        //    Assert.AreEqual("foo", actual[0]);
        //    Assert.AreEqual("bar", actual[1]);
        //}

        //[TestMethod]
        //public void FindMultipleValues_WithDocumentList_ShouldReturnList()
        //{
        //    var target = new Document();
        //    const string name = "foo";
        //    var sub1 = new Document();
        //    sub1.SetAttribute(name, "foo");
        //    var sub2 = new Document();
        //    sub2.SetAttribute(name, "bar");
        //    target.GetDocumentList("quux").AddRange(new[] { sub1, sub2 });
        //    var actual = target.FindValues("quux/foo").ToArray();
        //    Assert.AreEqual("foo", actual[0]);
        //    Assert.AreEqual("bar", actual[1]);
        //}

        //[TestMethod]
        //public void FindMultipleValues_WithListsInDocumentList_ShouldReturnList()
        //{
        //    var target = new Document();
        //    const string name = "foo";
        //    var sub1 = new Document();
        //    sub1.SetList(name, new List<object> { "one", "two"});
        //    var sub2 = new Document();
        //    sub2.SetList(name, new List<object> { "three", "four" });
        //    target.GetDocumentList("quux").AddRange(new[] { sub1, sub2 });
        //    var actual = target.FindValues("quux/foo").ToArray();
        //    Assert.AreEqual("one", actual[0]);
        //    Assert.AreEqual("two", actual[1]);
        //    Assert.AreEqual("three", actual[2]);
        //    Assert.AreEqual("four", actual[3]);
        //}
    }
    // ReSharper restore InconsistentNaming
}
