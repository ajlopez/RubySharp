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
    public class PutsFunctionTests
    {
        [TestMethod]
        public void PutsInteger()
        {
            StringWriter writer = new StringWriter();
            PutsFunction function = new PutsFunction(writer);

            Assert.IsNull(function.Apply(new object[] { 123 }));

            Assert.AreEqual("123\r\n", writer.ToString());
        }

        [TestMethod]
        public void PutsTwoIntegers()
        {
            StringWriter writer = new StringWriter();
            PutsFunction function = new PutsFunction(writer);

            Assert.IsNull(function.Apply(new object[] { 123, 456 }));

            Assert.AreEqual("123\r\n456\r\n", writer.ToString());
        }
    }
}
