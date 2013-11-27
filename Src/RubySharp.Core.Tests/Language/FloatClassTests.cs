namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Language;

    [TestClass]
    public class FloatClassTests
    {
        private FloatClass @class;

        [TestInitialize]
        public void Setup()
        {
            this.@class = new FloatClass(null);
        }

        [TestMethod]
        public void FloatClassInstance()
        {
            Assert.IsNotNull(this.@class);
            Assert.AreEqual("Float", this.@class.Name);
            Assert.AreEqual("Float", this.@class.ToString());
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
