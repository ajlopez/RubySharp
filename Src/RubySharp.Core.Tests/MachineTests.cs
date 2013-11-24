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
        public void InitialObjectAndModule()
        {
            Machine machine = new Machine();
            var context = machine.RootContext;

            var objectclass = (DynamicClass)context.GetLocalValue("Object");

            Assert.IsNotNull(context.Self);
            Assert.IsNull(context.Module);
            Assert.AreSame(objectclass, context.Self.Class);
        }

        [TestMethod]
        public void PredefinedFunctions()
        {
            Machine machine = new Machine();

            var result = machine.RootContext.Self.Class.GetInstanceMethod("puts");
            Assert.IsNotNull(result);

            result = machine.RootContext.Self.Class.GetInstanceMethod("print");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BasicObjectClass()
        {
            Machine machine = new Machine();
           
            var result = machine.RootContext.GetValue("BasicObject");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicClass));

            var @class = (DynamicClass)result;
            Assert.AreEqual("BasicObject", @class.Name);
            Assert.IsNull(@class.SuperClass);
        }

        [TestMethod]
        public void ObjectClass()
        {
            Machine machine = new Machine();

            var result = machine.RootContext.GetValue("Object");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicClass));

            var @class = (DynamicClass)result;
            Assert.AreEqual("Object", @class.Name);
            Assert.AreSame(machine.RootContext.GetValue("BasicObject"), @class.SuperClass);
        }

        [TestMethod]
        public void ModuleClass()
        {
            Machine machine = new Machine();

            var result = machine.RootContext.GetValue("Module");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicClass));

            var @class = (DynamicClass)result;
            Assert.AreEqual("Module", @class.Name);
            Assert.AreSame(machine.RootContext.GetValue("Object"), @class.SuperClass);
        }

        [TestMethod]
        public void ClassClass()
        {
            Machine machine = new Machine();

            var result = machine.RootContext.GetValue("Class");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicClass));

            var @class = (DynamicClass)result;
            Assert.AreEqual("Class", @class.Name);
            Assert.AreSame(machine.RootContext.GetValue("Module"), @class.SuperClass);
        }

        [TestMethod]
        public void FixnumClass()
        {
            Machine machine = new Machine();

            var result = machine.RootContext.GetValue("Fixnum");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FixnumClass));

            var @class = (FixnumClass)result;
            Assert.AreEqual("Fixnum", @class.Name);
        }

        [TestMethod]
        public void FloatClass()
        {
            Machine machine = new Machine();

            var result = machine.RootContext.GetValue("Float");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FloatClass));

            var @class = (FloatClass)result;
            Assert.AreEqual("Float", @class.Name);
        }

        [TestMethod]
        public void StringClass()
        {
            Machine machine = new Machine();

            var result = machine.RootContext.GetValue("String");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StringClass));

            var @class = (StringClass)result;
            Assert.AreEqual("String", @class.Name);
        }

        [TestMethod]
        public void NilClass()
        {
            Machine machine = new Machine();

            var result = machine.RootContext.GetValue("NilClass");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NilClass));

            var @class = (NilClass)result;
            Assert.AreEqual("NilClass", @class.Name);
        }

        [TestMethod]
        public void FalseClass()
        {
            Machine machine = new Machine();

            var result = machine.RootContext.GetValue("FalseClass");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FalseClass));

            var @class = (FalseClass)result;
            Assert.AreEqual("FalseClass", @class.Name);
        }

        [TestMethod]
        public void TrueClass()
        {
            Machine machine = new Machine();

            var result = machine.RootContext.GetValue("TrueClass");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TrueClass));

            var @class = (TrueClass)result;
            Assert.AreEqual("TrueClass", @class.Name);
        }

        [TestMethod]
        public void GetClassessClass()
        {
            Machine machine = new Machine();

            var result = machine.RootContext.GetValue("Class");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicClass));

            var @class = (DynamicClass)result;

            Assert.AreSame(@class, machine.ExecuteText("BasicObject.class"));
            Assert.AreSame(@class, machine.ExecuteText("Object.class"));
            Assert.AreSame(@class, machine.ExecuteText("Module.class"));
            Assert.AreSame(@class, machine.ExecuteText("Class.class"));
        }

        [TestMethod]
        public void GetClassessSuperClass()
        {
            Machine machine = new Machine();

            Assert.IsNull(machine.ExecuteText("BasicObject.superclass"));
            Assert.IsNotNull(machine.ExecuteText("Object.superclass"));
            Assert.AreSame(machine.ExecuteText("BasicObject"), machine.ExecuteText("Object.superclass"));
            Assert.IsNotNull(machine.ExecuteText("Module.superclass"));
            Assert.AreSame(machine.ExecuteText("Object"), machine.ExecuteText("Module.superclass"));
            Assert.IsNotNull(machine.ExecuteText("Class.superclass"));
            Assert.AreSame(machine.ExecuteText("Module"), machine.ExecuteText("Class.superclass"));
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
            Assert.IsInstanceOfType(result, typeof(DynamicClass));

            var dclass = (DynamicClass)result;

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
            Assert.IsInstanceOfType(result, typeof(DynamicObject));

            var obj = (DynamicObject)result;

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

        [TestMethod]
        public void CreateInstanceWithArgumentsUsingInitialize()
        {
            Machine machine = new Machine();

            var result = machine.ExecuteText("class Dog;def initialize(age);@age = age;end;end; nero = Dog.new(10)");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicObject));

            var obj = (DynamicObject)result;

            Assert.AreEqual(10, obj.GetValue("age"));
        }

        [TestMethod]
        public void EvaluateSimpleList()
        {
            Machine machine = new Machine();

            var result = machine.ExecuteText("[1,2,3]");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IList<object>));

            var list = (IList<object>)result;

            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(2, list[1]);
            Assert.AreEqual(3, list[2]);
        }

        [TestMethod]
        public void EvaluateListWithExpression()
        {
            Machine machine = new Machine();

            var result = machine.ExecuteText("[1,1+1,3]");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IList<object>));

            var list = (IList<object>)result;

            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(2, list[1]);
            Assert.AreEqual(3, list[2]);
        }

        [TestMethod]
        public void EvaluateFalse()
        {
            Machine machine = new Machine();
            var result = machine.ExecuteText("false");

            Assert.IsNotNull(result);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void EvaluateTrue()
        {
            Machine machine = new Machine();
            var result = machine.ExecuteText("true");

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void EvaluateNil()
        {
            Machine machine = new Machine();
            var result = machine.ExecuteText("nil");

            Assert.IsNull(result);
        }
    }
}
