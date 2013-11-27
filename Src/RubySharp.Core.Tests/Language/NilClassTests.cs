namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Language;

    [TestClass]
    public class NilClassTests
    {
        private NilClass @class;

        [TestInitialize]
        public void Setup()
        {
            this.@class = new NilClass(null);
        }

        [TestMethod]
        public void NilClassName()
        {
            Assert.AreEqual("NilClass", this.@class.Name);
            Assert.AreEqual("NilClass", this.@class.ToString());
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
