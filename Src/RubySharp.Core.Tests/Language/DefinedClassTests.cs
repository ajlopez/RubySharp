namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Commands;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class DefinedClassTests
    {
        [TestMethod]
        public void CreateDefinedClass()
        {
            DefinedClass dclass = new DefinedClass("Dog");

            Assert.AreEqual("Dog", dclass.Name);
        }

        [TestMethod]
        public void UndefinedInstanceMethodIsNull()
        {
            DefinedClass dclass = new DefinedClass("Dog");

            Assert.IsNull(dclass.GetInstanceMethod("foo"));
        }

        [TestMethod]
        public void CreateInstance()
        {
            DefinedClass dclass = new DefinedClass("Dog");
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
            DefinedClass @class = new DefinedClass("Dog");

            var result = @class.GetMethod("new");

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UndefinedMethodIsNull()
        {
            DefinedClass @class = new DefinedClass("Dog");

            Assert.IsNull(@class.GetMethod("foo"));
        }

        [TestMethod]
        public void ApplyNewMethod()
        {
            DefinedClass @class = new DefinedClass("Dog");

            var result = @class.GetMethod("new").Apply(@class, null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BaseObject));

            var obj = (BaseObject)result;

            Assert.AreSame(@class, obj.Class);
        }

        [TestMethod]
        public void ApplyNewMethodCallingInitialize()
        {
            DefinedClass @class = new DefinedClass("Dog");
            IFunction initialize = new DefinedFunction(new AssignInstanceVarCommand("age", new ConstantExpression(10)), new string[0], null);
            @class.SetInstanceMethod("initialize", initialize);

            var result = @class.GetMethod("new").Apply(@class, new object[] { });

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BaseObject));

            var obj = (BaseObject)result;

            Assert.AreSame(@class, obj.Class);
            Assert.AreEqual(10, obj.GetValue("age"));
        }
    }
}
