namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Language;

    [TestClass]
    public class StringClassTests
    {
        [TestMethod]
        public void StringInstance()
        {
            Assert.IsNotNull(StringClass.Instance);
            Assert.AreEqual("String", StringClass.Instance.Name);
        }

        [TestMethod]
        public void GetUnknownMethod()
        {
            Assert.IsNull(StringClass.Instance.GetInstanceMethod("foo"));
        }
    }
}
