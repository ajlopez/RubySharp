namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Language;

    [TestClass]
    public class ConstantNameTests
    {
        [TestMethod]
        public void CreateConstantName()
        {
            ConstantName name = new ConstantName("Foo");

            Assert.AreEqual("Foo", name.ToString());
        }

        [TestMethod]
        public void Equals()
        {
            ConstantName name1 = new ConstantName("Foo");
            ConstantName name2 = new ConstantName("Bar");
            ConstantName name3 = new ConstantName("Foo");

            Assert.IsTrue(name1.Equals(name3));
            Assert.IsTrue(name3.Equals(name1));
            Assert.AreEqual(name1.GetHashCode(), name3.GetHashCode());

            Assert.IsFalse(name1.Equals(name2));
            Assert.IsFalse(name2.Equals(name1));
            Assert.IsFalse(name1.Equals(null));
            Assert.IsFalse(name1.Equals("foo"));
        }
    }
}
