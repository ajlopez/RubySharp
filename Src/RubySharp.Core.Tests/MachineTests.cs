namespace RubySharp.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;

    [TestClass]
    public class MachineTests
    {
        [TestMethod]
        public void HasRootContext()
        {
            Machine machine = new Machine();
            Assert.IsNotNull(machine.RootContext);
        }

        [TestMethod]
        public void PredefinedFunctions()
        {
            Machine machine = new Machine();

            var result = machine.RootContext.GetValue("puts");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IFunction));
        }

        [TestMethod]
        public void ExecuteText()
        {
            Machine machine = new Machine();
            Assert.AreEqual(2, machine.ExecuteText("a=1\nb=2"));
            Assert.AreEqual(1, machine.RootContext.GetValue("a"));
            Assert.AreEqual(2, machine.RootContext.GetValue("b"));
        }

        [TestMethod]
        public void DefineClassWithMethod()
        {
            Machine machine = new Machine();
            Assert.IsNull(machine.ExecuteText("class MyClass;def foo;end;end"));
            
            var result = machine.RootContext.GetValue("MyClass");
            
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DefinedClass));

            var dclass = (DefinedClass)result;

            Assert.AreEqual("MyClass", dclass.Name);

            result = dclass.GetInstanceMethod("foo");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DefinedFunction));
        }

        [TestMethod]
        public void NewInstance()
        {
            Machine machine = new Machine();

            var result = machine.ExecuteText("class MyClass;end;MyClass.new");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(BaseObject));

            var obj = (BaseObject)result;

            Assert.IsNotNull(obj.Class);
            Assert.AreEqual("MyClass", obj.Class.Name);
        }

        [TestMethod]
        public void CallInstanceMethod()
        {
            Machine machine = new Machine();

            var result = machine.ExecuteText("class MyClass;def foo;3;end;end;myobj = MyClass.new;myobj.foo");

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void CallInstanceMethodWithArguments()
        {
            Machine machine = new Machine();

            var result = machine.ExecuteText("class MyClass;def foo a,b;a+b;end;end; myobj = MyClass.new; myobj.foo 1,2");

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void CallInstanceMethodWithArgumentsInParentheses()
        {
            Machine machine = new Machine();

            var result = machine.ExecuteText("class MyClass;def foo(a,b);a+b;end;end; myobj = MyClass.new; myobj.foo(1,2)");

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }
    }
}
