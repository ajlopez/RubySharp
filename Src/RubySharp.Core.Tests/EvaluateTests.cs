namespace RubySharp.Core.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Compiler;
    using RubySharp.Core.Exceptions;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Language;
    using RubySharp.Core.Tests.Classes;
    using RubySharp.Core.Utilities;

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
        public void EvaluateArrayClassAsArrayClass()
        {
            var result = this.EvaluateExpression("[1,2].class");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ArrayClass));
        }

        [TestMethod]
        public void EvaluateHashClassAsHashClass()
        {
            var result = this.EvaluateExpression("{:one=>1, :two => 2}.class");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(HashClass));
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
        public void EvaluateDefinedClassSuperClass()
        {
            var classclass = this.EvaluateExpression("Class");
            var objectclass = this.EvaluateExpression("Object");

            Assert.IsNotNull(classclass);

            var result = this.Execute("class Foo\n end\n Foo.superclass");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicClass));

            Assert.AreSame(objectclass, result);
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

        [TestMethod]
        public void EvaluateRangeClass()
        {
            var rangeclass = this.EvaluateExpression("Range");

            Assert.IsNotNull(rangeclass);

            var result = this.Execute("(1..10).class");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RangeClass));

            Assert.AreSame(rangeclass, result);
        }

        [TestMethod]
        public void EvaluateEachOnArray()
        {
            var result = this.Execute("total = 0\n[1,2,3].each do |x| total = total + x end\ntotal");

            Assert.IsNotNull(result);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void EvaluateForInRange()
        {
            var result = this.Execute("total = 0\nfor k in 1..3\ntotal = total + k\nend\ntotal");

            Assert.IsNotNull(result);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void EvaluateRangeEach()
        {
            var result = this.Execute("total = 0\n(1..3).each do |x| total = total + x end\ntotal");

            Assert.IsNotNull(result);
            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void EvaluateStringNativeProperty()
        {
            var result = this.Execute("'foo'.Length");

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void EvaluateStringNativeMethod()
        {
            var result = this.Execute("'foo'.ToUpper");

            Assert.IsNotNull(result);
            Assert.AreEqual("FOO", result);
        }

        [TestMethod]
        public void EvaluateDynamicObjectNativeMethod()
        {
            var obj = (DynamicObject)this.Execute("obj = Object.new");

            var result = this.Execute("obj.GetHashCode");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(obj.GetHashCode(), result);
        }

        [TestMethod]
        public void EvaluateStringNativeMethodWithArguments()
        {
            var result = this.Execute("'foo'.Substring 1,2");
            Assert.IsNotNull(result);
            Assert.AreEqual("oo", result);
        }

        [TestMethod]
        public void EvaluateNewPerson()
        {
            this.machine.RootContext.SetLocalValue("Person", typeof(Person));
            var result = this.EvaluateExpression("Person.new");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Person));
        }

        [TestMethod]
        public void EvaluateNewPersonWithNames()
        {
            this.machine.RootContext.SetLocalValue("Person", typeof(Person));
            var result = this.EvaluateExpression("Person.new 'Adam', 'Smith'");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Person));

            var person = (Person)result;
            Assert.AreEqual("Adam", person.FirstName);
            Assert.AreEqual("Smith", person.LastName);
        }

        [TestMethod]
        public void EvaluateQualifiedType()
        {
            var result = this.EvaluateExpression("RubySharp.Core.Tests.Classes.Person");

            Assert.IsNotNull(result);
            Assert.AreSame(typeof(Person), result);
        }

        [TestMethod]
        public void EvaluateQualifiedPersonNew()
        {
            var result = this.EvaluateExpression("RubySharp.Core.Tests.Classes.Person.new");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Person));
        }

        [TestMethod]
        public void EvaluateQualifiedPersonNewWithNames()
        {
            var result = this.EvaluateExpression("RubySharp.Core.Tests.Classes.Person.new('Adam', 'Smith')");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Person));

            var person = (Person)result;
            Assert.AreEqual("Adam", person.FirstName);
            Assert.AreEqual("Smith", person.LastName);
        }

        [TestMethod]
        public void EvaluateFormNew()
        {
            var result = this.EvaluateExpression("System.Windows.Forms.Form.new");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Form));
        }

        [TestMethod]
        public void EvaluateFormNewWithParens()
        {
            var result = this.EvaluateExpression("System.Windows.Forms.Form.new()");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Form));
        }

        [TestMethod]
        public void EvaluateFileExists()
        {
            var result = this.EvaluateExpression("System.IO.File.Exists('foo.txt')");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.IsFalse((bool)result);
        }

        [TestMethod]
        public void EvaluateTypeEnum()
        {
            var result = this.EvaluateExpression("System.Windows.Forms.DockStyle::Fill");

            Assert.IsNotNull(result);
            Assert.AreEqual(System.Windows.Forms.DockStyle.Fill, result);
        }

        [TestMethod]
        public void EvaluateCreateByteArray()
        {
            var result = this.EvaluateExpression("System.Array.CreateInstance(System.Byte, 1024)");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(byte[]));

            var array = (byte[])result;

            Assert.AreEqual(1024, array.Length);
        }

        [TestMethod]
        public void EvaluateAccessStringAsArray()
        {
            var result = this.EvaluateExpression("'foo'[0]");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(string));
            Assert.AreEqual("f", result);
        }

        [TestMethod]
        public void DefineQualifiedClass()
        {
            this.Execute("module MyModule\nend");
            this.Execute("class MyModule::MyClass\nend");

            var result = this.EvaluateExpression("MyModule::MyClass");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicClass));
            Assert.AreEqual("MyClass", ((DynamicClass)result).Name);
            Assert.AreEqual("MyModule::MyClass", ((DynamicClass)result).FullName);
        }

        [TestMethod]
        public void TypeErrorWhenDefineQualifiedClassOnAnObject()
        {
            try
            {
                this.Execute("class self::MyClass\nend");
                Assert.Fail();
            }
            catch (Exception ex) 
            {
                Assert.IsInstanceOfType(ex, typeof(TypeError));
                Assert.IsTrue(ex.Message.EndsWith(" is not a class/module"));
            }
        }

        [TestMethod]
        public void DefineObjectMethod()
        {
            this.Execute("class Object\ndef foo\nend\nend");

            var result = this.Execute("Object");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicClass));

            var dclass = (DynamicClass)result;

            Assert.IsNotNull(dclass.GetInstanceMethod("foo"));
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
