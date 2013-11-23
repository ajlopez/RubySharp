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
        private StringClass @class;

        [TestInitialize]
        public void Setup()
        {
            this.@class = new StringClass(null);
        }

        [TestMethod]
        public void StringClassInstance()
        {
            Assert.IsNotNull(this.@class);
            Assert.AreEqual("String", this.@class.Name);
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
