namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Language;

    [TestClass]
    public class DynamicArrayTests
    {
        [TestMethod]
        public void EmptyArrayToString()
        {
            DynamicArray array = new DynamicArray();

            Assert.AreEqual("[]", array.ToString());
        }

        [TestMethod]
        public void ArrayToString()
        {
            DynamicArray array = new DynamicArray();

            array[0] = 1;
            array[1] = 2;

            var result = array.ToString();

            Assert.AreEqual("[1, 2]", result);
        }

        [TestMethod]
        public void ArrayToStringWithNils()
        {
            DynamicArray array = new DynamicArray();

            array[0] = 1;
            array[3] = 2;

            var result = array.ToString();

            Assert.AreEqual("[1, nil, nil, 2]", result);
        }
    }
}
