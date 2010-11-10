using AzDoc.Documents;
using AzDoc.Indexing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable InconsistentNaming
namespace AzDoc.Indexing.Test
{
    
    
    /// <summary>
    ///This is a test class for IndexParserTest and is intended
    ///to contain all IndexParserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IndexParserTest
    {
        /// <summary>
        ///A test for GetEqExpressions
        ///</summary>
        [TestMethod()]
        public void GetEqExpressions_SingleIntExpression_Test()
        {
            string index = "Age eq 42";
            var expected = Tuple.Create(new PropertyReference("Age"), "42");
            var actual = IndexParser.GetEqExpressions(index).Single();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetEqExpressions
        ///</summary>
        [TestMethod()]
        public void GetEqExpressions_SingleStringExpression_Test()
        {
            string index = "Name eq 'Bob'";
            var expected = Tuple.Create(new PropertyReference("Name"), "Bob");
            var actual = IndexParser.GetEqExpressions(index).Single();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetEqExpressions
        ///</summary>
        [TestMethod()]
        public void GetEqExpressions_DoubleIntExpression_Test()
        {
            string index = "Age eq 42 and Size eq 12";
            var expected0 = Tuple.Create(new PropertyReference("Age"), "42");
            var expected1 = Tuple.Create(new PropertyReference("Size"), "12");
            var actual = IndexParser.GetEqExpressions(index).ToArray();
            Assert.AreEqual(expected0, actual[0]);
            Assert.AreEqual(expected1, actual[1]);
        }

        /// <summary>
        ///A test for GetEqExpressions
        ///</summary>
        [TestMethod()]
        public void GetEqExpressions_DoubleStringExpression_Test()
        {
            const string index = "FirstName eq 'Bob' and LastName eq 'Smith'";
            var expected0 = Tuple.Create(new PropertyReference("FirstName"), "Bob");
            var expected1 = Tuple.Create(new PropertyReference("LastName"), "Smith");
            var actual = IndexParser.GetEqExpressions(index).ToArray();
            Assert.AreEqual(expected0, actual[0]);
            Assert.AreEqual(expected1, actual[1]);
        }

        /// <summary>
        ///A test for GetEqExpressions
        ///</summary>
        [TestMethod()]
        public void GetEqExpressions_DoubleSlashedStringExpression_Test()
        {
            const string index = "First/Name eq 'Bob' and Last/Name eq 'Smith'";
            var expected0 = Tuple.Create(new PropertyReference("First/Name"), "Bob");
            var expected1 = Tuple.Create(new PropertyReference("Last/Name"), "Smith");
            var actual = IndexParser.GetEqExpressions(index).ToArray();
            Assert.AreEqual(expected0, actual[0]);
            Assert.AreEqual(expected1, actual[1]);
        }
    }
}
// ReSharper restore InconsistentNaming
