namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Language;

    [TestClass]
    public class TrueClassTests
    {
        [TestMethod]
        public void TrueClassInstance()
        {
            Assert.IsNotNull(TrueClass.Instance);
            Assert.AreEqual("TrueClass", TrueClass.Instance.Name);
        }

        [TestMethod]
        public void GetClassInstanceMethod()
        {
            Assert.IsNotNull(TrueClass.Instance.GetInstanceMethod("class"));
        }

        [TestMethod]
        public void GetUnknownInstanceMethod()
        {
            Assert.IsNull(TrueClass.Instance.GetInstanceMethod("foo"));
        }
    }
}
