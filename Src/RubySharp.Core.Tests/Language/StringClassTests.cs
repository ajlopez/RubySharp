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
        public void StringClassInstance()
        {
            Assert.IsNotNull(StringClass.Instance);
            Assert.AreEqual("String", StringClass.Instance.Name);
        }

        [TestMethod]
        public void GetClassInstanceMethod()
        {
            Assert.IsNotNull(StringClass.Instance.GetInstanceMethod("class"));
        }

        [TestMethod]
        public void GetUnknownInstanceMethod()
        {
            Assert.IsNull(StringClass.Instance.GetInstanceMethod("foo"));
        }
    }
}
