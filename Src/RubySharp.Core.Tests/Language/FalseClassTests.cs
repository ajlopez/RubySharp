namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Language;

    [TestClass]
    public class FalseClassTests
    {
        [TestMethod]
        public void FalseClassInstance()
        {
            Assert.IsNotNull(FalseClass.Instance);
            Assert.AreEqual("FalseClass", FalseClass.Instance.Name);
        }

        [TestMethod]
        public void GetClassInstanceMethod()
        {
            Assert.IsNotNull(FalseClass.Instance.GetInstanceMethod("class"));
        }

        [TestMethod]
        public void GetUnknownInstanceMethod()
        {
            Assert.IsNull(FalseClass.Instance.GetInstanceMethod("foo"));
        }
    }
}
