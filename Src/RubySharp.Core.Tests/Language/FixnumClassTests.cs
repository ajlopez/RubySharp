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
        private FixnumClass @class;

        [TestInitialize]
        public void Setup()
        {
            this.@class = new FixnumClass(null);
        }

        [TestMethod]
        public void FixnumClassInstance()
        {
            Assert.IsNotNull(this.@class);
            Assert.AreEqual("Fixnum", this.@class.Name);
        }

        [TestMethod]
        public void GetClassInstanceMethod()
        {
            Assert.IsNotNull(this.@class.GetInstanceMethod("class"));
        }

        [TestMethod]
        public void GetUnknownInstanceMethod()
        {
            Assert.IsNull(this.@class.GetInstanceMethod("foo"));
        }
    }
}
