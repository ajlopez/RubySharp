﻿namespace RubySharp.Core.Tests.Functions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
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
    }
}
