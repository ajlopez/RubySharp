namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Language;

    [TestClass]
    public class DynamicHashTests
    {
        [TestMethod]
        public void EmptyHashToString()
        {
            DynamicHash hash = new DynamicHash();

            Assert.AreEqual("{}", hash.ToString());
        }

        [TestMethod]
        public void HashToString()
        {
            DynamicHash hash = new DynamicHash();

            hash[new Symbol("one")] = 1;
            hash[new Symbol("two")] = 2;

            var result = hash.ToString();

            Assert.IsTrue(result.StartsWith("{"));
            Assert.IsTrue(result.EndsWith("}"));
            Assert.IsTrue(result.Contains(":one=>1"));
            Assert.IsTrue(result.Contains(":two=>2"));
        }
    }
}
