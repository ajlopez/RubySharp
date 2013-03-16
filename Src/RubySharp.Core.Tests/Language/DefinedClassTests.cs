namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Language;

    [TestClass]
    public class DefinedClassTests
    {
        [TestMethod]
        public void CreateDefinedClass()
        {
            DefinedClass dclass = new DefinedClass("Dog");

            Assert.AreEqual("Dog", dclass.Name);
        }
    }
}
