namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;

    [TestClass]
    public class DynamicClassTests
    {
        [TestMethod]
        public void CreateDefinedClass()
        {
            DynamicClass dclass = new DynamicClass("Dog");

            Assert.AreEqual("Dog", dclass.Name);
        }

        [TestMethod]
        public void ToString()
        {
            DynamicClass dclass = new DynamicClass("Dog");

            Assert.AreEqual("Dog", dclass.ToString());
        }

        [TestMethod]
        public void UndefinedInstanceMethodIsNull()
        {
            DynamicClass dclass = new DynamicClass("Dog");

            Assert.IsNull(dclass.GetInstanceMethod("foo"));
        }

        [TestMethod]
        public void RetrieveDefinedInstanceMethodIsNull()
        {
            DynamicClass dclass = new DynamicClass("Dog");
            IFunction foo = new DefinedFunction(null, null, null);
            dclass.SetInstanceMethod("foo", foo);

            Assert.AreSame(foo, dclass.GetInstanceMethod("foo"));
        }

        [TestMethod]
        public void GetEmptyOwnInstanceMethodNames()
        {
            DynamicClass dclass = new DynamicClass("Dog");

            var result = dclass.GetOwnInstanceMethodNames();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void GetOwnInstanceMethodNames()
        {
            DynamicClass dclass = new DynamicClass("Dog");
            IFunction foo = new DefinedFunction(null, null, null);
            dclass.SetInstanceMethod("foo", foo);

            var result = dclass.GetOwnInstanceMethodNames();

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Count);
            Assert.IsTrue(result.Contains("foo"));
        }

        [TestMethod]
        public void CreateInstance()
        {
            DynamicClass dclass = new DynamicClass("Dog");
            IFunction foo = new DefinedFunction(null, null, null);
            dclass.SetInstanceMethod("foo", foo);

            var result = dclass.CreateInstance();

            Assert.IsNotNull(result);
            Assert.AreSame(dclass, result.Class);
            Assert.AreSame(foo, result.GetMethod("foo"));
        }

        [TestMethod]
        public void ClassHasNewMethod()
        {
            Machine machine = new Machine();
            DynamicClass @class = new DynamicClass((DynamicClass)machine.RootContext.GetLocalValue("Class"), "Dog", (DynamicClass)machine.RootContext.GetLocalValue("Object"));

            var result = @class.GetMethod("new");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UndefinedMethodIsNull()
        {
            DynamicClass @class = new DynamicClass("Dog");

            Assert.IsNull(@class.GetMethod("foo"));
        }

        [TestMethod]
        public void ApplyNewMethod()
        {
            Machine machine = new Machine();
            DynamicClass @class = new DynamicClass((DynamicClass)machine.RootContext.GetLocalValue("Class"), "Dog", (DynamicClass)machine.RootContext.GetLocalValue("Object"));

            var result = @class.GetMethod("new").Apply(@class, machine.RootContext, null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicObject));

            var obj = (DynamicObject)result;

            Assert.AreSame(@class, obj.Class);
        }

        [TestMethod]
        public void ApplyNewMethodCallingInitialize()
        {
            Machine machine = new Machine();
            DynamicClass @class = new DynamicClass((DynamicClass)machine.RootContext.GetLocalValue("Class"), "Dog", (DynamicClass)machine.RootContext.GetLocalValue("Object"));
            IFunction initialize = new DefinedFunction(new AssignInstanceVarExpression("age", new ConstantExpression(10)), new string[0], null);
            @class.SetInstanceMethod("initialize", initialize);

            var result = @class.GetMethod("new").Apply(@class, @class.Constants, new object[] { });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicObject));

            var obj = (DynamicObject)result;

            Assert.AreSame(@class, obj.Class);
            Assert.AreEqual(10, obj.GetValue("age"));
        }
    }
}
