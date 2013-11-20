namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Language;

    [TestClass]
    public class FixnumClassTests
    {
        [TestMethod]
        public void FixnumInstance()
        {
            Assert.IsNotNull(FixnumClass.Instance);
            Assert.AreEqual("Fixnum", FixnumClass.Instance.Name);
        }

        [TestMethod]
        public void GetUnknownMethod()
        {
            Assert.IsNull(FixnumClass.Instance.GetInstanceMethod("foo"));
        }
    }
}
