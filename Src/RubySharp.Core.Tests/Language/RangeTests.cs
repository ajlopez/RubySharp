namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Language;

    [TestClass]
    public class RangeTests
    {
        [TestMethod]
        public void EmptyRange()
        {
            int total = 0;
            Range range = new Range(1, 0);

            foreach (int k in range)
                total += k;

            Assert.AreEqual(0, total);
        }

        [TestMethod]
        public void OneNumberRange()
        {
            int total = 0;
            Range range = new Range(1, 1);

            foreach (int k in range)
                total += k;

            Assert.AreEqual(1, total);
        }

        [TestMethod]
        public void ThreeNumbersRange()
        {
            int total = 0;
            Range range = new Range(1, 3);

            foreach (int k in range)
                total += k;

            Assert.AreEqual(6, total);
        }

        [TestMethod]
        public void RangeToString()
        {
            Range range = new Range(1, 3);

            Assert.AreEqual("1..3", range.ToString());
        }
    }
}
