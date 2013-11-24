namespace RubySharp.Core.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Compiler;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Language;

    [TestClass]
    public class EvaluateTests
    {
        private Machine machine;

        [TestInitialize]
        public void Setup()
        {
            this.machine = new Machine();
        }

        [TestMethod]
        public void EvaluateSimpleArithmetic()
        {
            Assert.AreEqual(2, this.EvaluateExpression("1+1"));
            Assert.AreEqual(1 / 2, this.EvaluateExpression("1/2"));
            Assert.AreEqual(1 + (3 * 2), this.EvaluateExpression("1 + 3 * 2"));
        }

        [TestMethod]
        public void EvaluateStringConcatenate()
        {
            Assert.AreEqual("foobar", this.EvaluateExpression("'foo' + 'bar'"));
            Assert.AreEqual("foo1", this.EvaluateExpression("'foo' + 1"));
            Assert.AreEqual("1foo", this.EvaluateExpression("1 + 'foo'"));
        }

        [TestMethod]
        public void EvaluateSimpleAssignment()
        {
            Assert.AreEqual(2, this.Execute("a = 2"));
            Assert.AreEqual(2, this.EvaluateExpression("a"));
        }

        [TestMethod]
        public void EvaluateSimpleArray()
        {
            Assert.IsNotNull(this.Execute("a = [1,2,3]"));
            var result = this.EvaluateExpression("a");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IList));

            var list = (IList)result;

            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(2, list[1]);
            Assert.AreEqual(3, list[2]);
        }

        [TestMethod]
        public void EvaluateIntegerClassAsFixnum()
        {
            var result = this.EvaluateExpression("1.class");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FixnumClass));
        }

        [TestMethod]
        public void EvaluateStringClassAsString()
        {
            var result = this.EvaluateExpression("'foo'.class");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StringClass));
        }

        [TestMethod]
        public void EvaluateTrueClassAsTrueClass()
        {
            var result = this.EvaluateExpression("true.class");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TrueClass));
        }

        [TestMethod]
        public void EvaluateFalseClassAsFalseClass()
        {
            var result = this.EvaluateExpression("false.class");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FalseClass));
        }

        [TestMethod]
        public void EvaluateNilClassAsNilClass()
        {
            var result = this.EvaluateExpression("nil.class");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NilClass));
        }

        [TestMethod]
        public void EvaluateFloatClassAsFloatClass()
        {
            var result = this.EvaluateExpression("1.2.class");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FloatClass));
        }

        [TestMethod]
        public void EvaluateModuleConstant()
        {
            this.Execute("module MyModule;ONE=1;end");
            var result = this.EvaluateExpression("MyModule::ONE");

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void EvaluateObjectNew()
        {
            var result = this.EvaluateExpression("Object.new");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicObject));

            var dobj = (DynamicObject)result;

            Assert.IsNotNull(dobj.Class);
            Assert.AreEqual("Object", dobj.Class.Name);
            Assert.AreSame(dobj.Class, this.EvaluateExpression("Object"));
        }

        [TestMethod]
        public void EvaluateObjectNewClass()
        {
            var objectclass = this.EvaluateExpression("Object");

            Assert.IsNotNull(objectclass);

            var result = this.EvaluateExpression("Object.new.class");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicClass));

            Assert.AreSame(objectclass, result);
        }

        [TestMethod]
        public void EvaluateModuleNewClass()
        {
            var moduleclass = this.EvaluateExpression("Module");

            Assert.IsNotNull(moduleclass);

            var result = this.EvaluateExpression("Module.new.class");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicClass));

            Assert.AreSame(moduleclass, result);
        }

        [TestMethod]
        public void EvaluateClassNewClass()
        {
            var classclass = this.EvaluateExpression("Class");

            Assert.IsNotNull(classclass);

            var result = this.EvaluateExpression("Class.new.class");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicClass));

            Assert.AreSame(classclass, result);
        }

        [TestMethod]
        public void EvaluateDefinedClassClass()
        {
            var classclass = this.EvaluateExpression("Class");

            Assert.IsNotNull(classclass);

            var result = this.Execute("class Foo\n end\n Foo.class");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicClass));

            Assert.AreSame(classclass, result);
        }

        [TestMethod]
        public void EvaluateDefinedModuleClass()
        {
            var moduleclass = this.EvaluateExpression("Module");

            Assert.IsNotNull(moduleclass);

            var result = this.Execute("module Foo\n end\n Foo.class");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicClass));

            Assert.AreSame(moduleclass, result);
        }

        private object EvaluateExpression(string text)
        {
            Parser parser = new Parser(text);
            IExpression expression = parser.ParseExpression();
            Assert.IsNull(parser.ParseExpression());
            return expression.Evaluate(this.machine.RootContext);
        }

        private object Execute(string text)
        {
            return this.machine.ExecuteText(text);
        }
    }
}
