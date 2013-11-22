namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Language;

    [TestClass]
    public class NilClassTests
    {
        [TestMethod]
        public void NilClassInstance()
        {
            Assert.IsNotNull(NilClass.Instance);
            Assert.AreEqual("NilClass", NilClass.Instance.Name);
        }

        [TestMethod]
        public void GetClassInstanceMethod()
        {
            Assert.IsNotNull(NilClass.Instance.GetInstanceMethod("class"));
        }

        [TestMethod]
        public void GetUnknownInstanceMethod()
        {
            Assert.IsNull(NilClass.Instance.GetInstanceMethod("foo"));
        }
    }
}
