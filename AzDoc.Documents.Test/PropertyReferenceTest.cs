using AzDoc.Documents;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AzDoc.Documents.Test
{
    
    
    /// <summary>
    ///This is a test class for PropertyReferenceTest and is intended
    ///to contain all PropertyReferenceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PropertyReferenceTest
    {
        // ReSharper disable InconsistentNaming

        /// <summary>
        ///A test for PropertyReference Constructor
        ///</summary>
        [TestMethod()]
        public void PropertyReferenceConstructor_WithNoSlashes_ShouldOnlySetPropertyName()
        {
            const string fullPath = "X";
            var target = new PropertyReference(fullPath);
            Assert.AreEqual(string.Empty, target.Path);
            Assert.AreEqual("X", target.PropertyName);
        }

        /// <summary>
        ///A test for PropertyReference Constructor
        ///</summary>
        [TestMethod()]
        public void PropertyReferenceConstructor_WithOneSlash_ShouldSetPathAndPropertyName()
        {
            const string fullPath = "X/Y";
            var target = new PropertyReference(fullPath);
            Assert.AreEqual("X", target.Path);
            Assert.AreEqual("Y", target.PropertyName);
        }

        /// <summary>
        ///A test for PropertyReference Constructor
        ///</summary>
        [TestMethod()]
        public void PropertyReferenceConstructor_WithTwoSlashes_ShouldSetPathAndPropertyName()
        {
            const string fullPath = "X/Y/Z";
            var target = new PropertyReference(fullPath);
            Assert.AreEqual("X/Y", target.Path);
            Assert.AreEqual("Z", target.PropertyName);
        }

        // ReSharper restore InconsistentNaming
    }
}
