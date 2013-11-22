namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Language;

    [TestClass]
    public class FloatClassTests
    {
        [TestMethod]
        public void FloatClassInstance()
        {
            Assert.IsNotNull(FloatClass.Instance);
            Assert.AreEqual("Float", FloatClass.Instance.Name);
        }

        [TestMethod]
        public void GetClassInstanceMethod()
        {
            Assert.IsNotNull(FloatClass.Instance.GetInstanceMethod("class"));
        }

        [TestMethod]
        public void GetUnknownInstanceMethod()
        {
            Assert.IsNull(FloatClass.Instance.GetInstanceMethod("foo"));
        }
    }
}
