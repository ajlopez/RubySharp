namespace RubySharp.Core.Tests.Functions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Functions;

    [TestClass]
    public class PrintFunctionTests
    {
        [TestMethod]
        public void PrintInteger()
        {
            StringWriter writer = new StringWriter();
            PrintFunction function = new PrintFunction(writer);

            Assert.IsNull(function.Apply(null, null, new object[] { 123 }));

            Assert.AreEqual("123", writer.ToString());
        }

        [TestMethod]
        public void PrintTwoIntegers()
        {
            StringWriter writer = new StringWriter();
            PrintFunction function = new PrintFunction(writer);

            Assert.IsNull(function.Apply(null, null, new object[] { 123, 456 }));

            Assert.AreEqual("123456", writer.ToString());
        }
    }
}
