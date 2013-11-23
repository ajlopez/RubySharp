namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Language;

    [TestClass]
    public class FalseClassTests
    {
        private FalseClass @class;

        [TestInitialize]
        public void Setup()
        {
            this.@class = new FalseClass(null);
        }

        [TestMethod]
        public void FalseClassInstance()
        {
            Assert.IsNotNull(this.@class);
            Assert.AreEqual("FalseClass", this.@class.Name);
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
