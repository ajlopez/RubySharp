namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Language;
    using RubySharp.Core.Tests.Classes;

    [TestClass]
    public class PredicatesTests
    {
        [TestMethod]
        public void IsFalse()
        {
            Assert.IsTrue(Predicates.IsFalse(null));
            Assert.IsTrue(Predicates.IsFalse(false));
            Assert.IsFalse(Predicates.IsFalse(string.Empty));
            Assert.IsFalse(Predicates.IsFalse("foo"));
            Assert.IsFalse(Predicates.IsFalse(123));
            Assert.IsFalse(Predicates.IsFalse(0));
            Assert.IsFalse(Predicates.IsFalse(0.0));
            Assert.IsFalse(Predicates.IsFalse(typeof(Person)));
        }

        [TestMethod]
        public void IsTrue()
        {
            Assert.IsFalse(Predicates.IsTrue(null));
            Assert.IsFalse(Predicates.IsTrue(false));
            Assert.IsTrue(Predicates.IsTrue(string.Empty));
            Assert.IsTrue(Predicates.IsTrue("foo"));
            Assert.IsTrue(Predicates.IsTrue(123));
            Assert.IsTrue(Predicates.IsTrue(0));
            Assert.IsTrue(Predicates.IsTrue(0.0));
            Assert.IsTrue(Predicates.IsTrue(typeof(Person)));
        }

        [TestMethod]
        public void IsConstantName()
        {
            Assert.IsFalse(Predicates.IsConstantName(null));
            Assert.IsFalse(Predicates.IsConstantName(string.Empty));
            Assert.IsFalse(Predicates.IsConstantName("foo"));
            Assert.IsTrue(Predicates.IsConstantName("Foo"));
            Assert.IsTrue(Predicates.IsConstantName("FOO"));
        }
    }
}
