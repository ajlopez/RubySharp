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
    public class DynamicObjectTests
    {
        private DynamicClass @class;
        private IFunction foo;

        [TestInitialize]
        public void Setup()
        {
            this.@class = new DynamicClass("Dog");
            this.foo = new DefinedFunction(null, null, null);
            this.@class.SetInstanceMethod("foo", this.foo);
        }

        [TestMethod]
        public void CreateObject()
        {
            DynamicObject obj = new DynamicObject(this.@class);

            Assert.AreSame(this.@class, obj.Class);
        }

        [TestMethod]
        public void GetSingletonClass()
        {
            DynamicObject obj = new DynamicObject(this.@class);

            var singleton = obj.SingletonClass;

            Assert.IsNotNull(singleton);
            Assert.AreSame(obj.Class, singleton.SuperClass);
            Assert.AreEqual(string.Format("#<Class:{0}>", obj.ToString()), singleton.Name);
        }

        [TestMethod]
        public void ObjectToString()
        {
            DynamicObject obj = new DynamicObject(this.@class);
            var result = obj.ToString();

            Assert.IsTrue(result.StartsWith("#<Dog:0x"));
            Assert.IsTrue(result.EndsWith(">"));
        }

        [TestMethod]
        public void GetUndefinedValue()
        {
            DynamicObject obj = new DynamicObject(this.@class);

            Assert.IsNull(obj.GetValue("name"));
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            DynamicObject obj = new DynamicObject(this.@class);

            obj.SetValue("name", "Nero");

            Assert.AreEqual("Nero", obj.GetValue("name"));
        }

        [TestMethod]
        public void GetMethodFromClass()
        {
            DynamicObject obj = new DynamicObject(this.@class);
            Assert.AreSame(this.foo, obj.GetMethod("foo"));
        }

        [TestMethod]
        public void GetMethodFromSingletonClass()
        {
            DynamicObject obj = new DynamicObject(this.@class);
            var newfoo = new DefinedFunction(null, null, null);
            obj.SingletonClass.SetInstanceMethod("foo", newfoo);
            Assert.AreSame(newfoo, obj.GetMethod("foo"));
        }
    }
}
