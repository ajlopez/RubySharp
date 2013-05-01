namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Language;

    [TestClass]
    public class SymbolTests
    {
        [TestMethod]
        public void CreateSymbol()
        {
            Symbol symbol = new Symbol("foo");

            Assert.AreEqual(":foo", symbol.ToString());
        }

        [TestMethod]
        public void Equals()
        {
            Symbol symbol1 = new Symbol("foo");
            Symbol symbol2 = new Symbol("bar");
            Symbol symbol3 = new Symbol("foo");

            Assert.IsTrue(symbol1.Equals(symbol3));
            Assert.IsTrue(symbol3.Equals(symbol1));
            Assert.AreEqual(symbol1.GetHashCode(), symbol3.GetHashCode());

            Assert.IsFalse(symbol1.Equals(symbol2));
            Assert.IsFalse(symbol2.Equals(symbol1));
            Assert.IsFalse(symbol1.Equals(null));
            Assert.IsFalse(symbol1.Equals("foo"));
        }
    }
}
