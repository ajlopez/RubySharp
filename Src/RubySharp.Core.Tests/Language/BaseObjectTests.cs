namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;

    [TestClass]
    public class BaseObjectTests
    {
        private DefinedClass @class;
        private IFunction foo;

        [TestInitialize]
        public void Setup()
        {
            this.@class = new DefinedClass("Dog");
            this.foo = new DefinedFunction(null, null, null);
            this.@class.SetInstanceMethod("foo", this.foo);
        }

        [TestMethod]
        public void CreateObject()
        {
            BaseObject obj = new BaseObject(this.@class);

            Assert.AreSame(this.@class, obj.Class);
        }

        [TestMethod]
        public void GetUndefinedValue()
        {
            BaseObject obj = new BaseObject(this.@class);

            Assert.IsNull(obj.GetValue("name"));
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            BaseObject obj = new BaseObject(this.@class);

            obj.SetValue("name", "Nero");

            Assert.AreEqual("Nero", obj.GetValue("name"));
        }

        [TestMethod]
        public void GetMethodFromClass()
        {
            BaseObject obj = new BaseObject(this.@class);
            Assert.AreSame(this.foo, obj.GetMethod("foo"));
        }
    }
}
