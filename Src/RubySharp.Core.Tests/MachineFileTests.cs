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
    [DeploymentItem("MachineFiles", "MachineFiles")]
    public class MachineFileTests
    {
        private Machine machine;

        [TestInitialize]
        public void Setup()
        {
            this.machine = new Machine();
        }

        [TestMethod]
        public void ExecuteSimpleAssignFile()
        {
            Assert.AreEqual(1, this.machine.ExecuteFile("MachineFiles\\SimpleAssign.rb"));
            Assert.AreEqual(1, this.machine.RootContext.GetValue("a"));
        }

        [TestMethod]
        public void ExecuteSimpleAssignsFile()
        {
            Assert.AreEqual(2, this.machine.ExecuteFile("MachineFiles\\SimpleAssigns.rb"));
            Assert.AreEqual(1, this.machine.RootContext.GetValue("a"));
            Assert.AreEqual(2, this.machine.RootContext.GetValue("b"));
        }

        [TestMethod]
        public void ExecuteSimplePutsFile()
        {
            StringWriter writer = new StringWriter();
            this.machine.RootContext.Self.Class.SetInstanceMethod("puts", new PutsFunction(writer));
            Assert.AreEqual(null, this.machine.ExecuteFile("MachineFiles\\SimplePuts.rb"));
            Assert.AreEqual("hello\r\n", writer.ToString());
        }

        [TestMethod]
        public void ExecuteSimpleDefFile()
        {
            StringWriter writer = new StringWriter();
            this.machine.RootContext.Self.Class.SetInstanceMethod("puts", new PutsFunction(writer));
            Assert.AreEqual(null, this.machine.ExecuteFile("MachineFiles\\SimpleDef.rb"));
            Assert.AreEqual("1\r\n", writer.ToString());
        }

        [TestMethod]
        public void ExecuteSimpleClassFile()
        {
            StringWriter writer = new StringWriter();
            this.machine.RootContext.Self.Class.SetInstanceMethod("puts", new PutsFunction(writer));
            Assert.AreEqual(null, this.machine.ExecuteFile("MachineFiles\\SimpleClass.rb"));
            Assert.AreEqual("Hello\r\n", writer.ToString());

            var result = this.machine.RootContext.GetValue("Dog");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicClass));

            var dclass = (DynamicClass)result;

            Assert.AreEqual("Dog", dclass.Name);
        }

        [TestMethod]
        public void ExecuteNewInstanceFile()
        {
            var result = this.machine.ExecuteFile("MachineFiles\\NewInstance.rb");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicObject));

            var obj = (DynamicObject)result;

            Assert.AreEqual("Nero", obj.GetValue("name"));
            Assert.AreEqual(6, obj.GetValue("age"));
            Assert.AreEqual("Dog", obj.Class.Name);
        }

        [TestMethod]
        public void ExecutePointFile()
        {
            this.machine.ExecuteFile("MachineFiles\\Point.rb");

            var result = this.machine.ExecuteText("Point");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicClass));
        }

        [TestMethod]
        public void CreatePoint()
        {
            this.machine.ExecuteFile("MachineFiles\\Point.rb");

            var result = this.machine.ExecuteText("Point.new(10, 20)");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicObject));

            var dobj = (DynamicObject)result;

            Assert.AreEqual(10, dobj.GetValue("x"));
            Assert.AreEqual(20, dobj.GetValue("y"));
        }

        [TestMethod]
        public void RequireFile()
        {
            Assert.IsTrue(this.machine.RequireFile("MachineFiles\\Point"));

            var result = this.machine.ExecuteText("Point");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DynamicClass));
        }

        [TestMethod]
        public void RequireLibraryFile()
        {
            Assert.IsTrue(this.machine.RequireFile("MachineFiles\\MyLib"));

            var result = this.machine.ExecuteText("MyLib.MyClass");
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Type));
        }

        [TestMethod]
        public void RequireFileTwice()
        {
            Assert.IsTrue(this.machine.RequireFile("MachineFiles\\SimpleAssign"));
            Assert.IsFalse(this.machine.RequireFile("MachineFiles\\SimpleAssign.rb"));

            var result = this.machine.ExecuteText("a");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void RequireFileTwiceUsingFullPath()
        {
            string filename = Path.GetFullPath("MachineFiles\\SimpleAssign");
            string filename2 = Path.GetFullPath("MachineFiles\\SimpleAssign.rb");
            Assert.IsTrue(this.machine.RequireFile(filename));
            Assert.IsFalse(this.machine.RequireFile(filename2));

            var result = this.machine.ExecuteText("a");
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ExecuteRequireModules()
        {
            this.machine.ExecuteFile("MachineFiles\\RequireModules.rb");

            var result = this.machine.ExecuteText("Module1");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ModuleObject));
            Assert.AreEqual("Module1", ((ModuleObject)result).Name);

            result = this.machine.ExecuteText("Module2");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ModuleObject));
            Assert.AreEqual("Module2", ((ModuleObject)result).Name);
        }

        [TestMethod]
        public void ExecuteRepeatedModuleFile()
        {
            this.machine.ExecuteFile("MachineFiles\\RepeatedModule.rb");

            Assert.AreEqual(1, this.machine.ExecuteText("MyModule::ONE"));
            Assert.AreEqual(2, this.machine.ExecuteText("MyModule::TWO"));
        }

        [TestMethod]
        public void ExecuteModuleWithClassesFile()
        {
            this.machine.ExecuteFile("MachineFiles\\ModuleWithClasses.rb");

            var result1 = this.machine.ExecuteText("MyLisp::List");
            var result2 = this.machine.ExecuteText("MyLisp::Atom");

            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(DynamicClass));
            Assert.AreEqual("List", ((DynamicClass)result1).Name);

            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(DynamicClass));
            Assert.AreEqual("Atom", ((DynamicClass)result2).Name);
        }

        [TestMethod]
        public void ExecuteClassWithModuleFile()
        {
            this.machine.ExecuteFile("MachineFiles\\ClassWithModule.rb");

            var result1 = this.machine.ExecuteText("MyClass");
            var result2 = this.machine.ExecuteText("MyClass::MyModule");

            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(DynamicClass));
            Assert.AreEqual("MyClass", ((DynamicClass)result1).Name);

            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(ModuleObject));
            Assert.AreEqual("MyClass::MyModule", ((ModuleObject)result2).Name);
        }

        [TestMethod]
        public void ExecuteNestedModulesFile()
        {
            this.machine.ExecuteFile("MachineFiles\\NestedModules.rb");

            var result1 = this.machine.ExecuteText("MyModule");
            var result2 = this.machine.ExecuteText("MyModule::MySubmodule");
            var result3 = this.machine.ExecuteText("MyModule::MySubmodule::MySubmodule2");

            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(ModuleObject));
            Assert.AreEqual("MyModule", ((ModuleObject)result1).Name);

            Assert.IsNotNull(result2);
            Assert.IsInstanceOfType(result2, typeof(ModuleObject));
            Assert.AreEqual("MyModule::MySubmodule", ((ModuleObject)result2).Name);

            Assert.IsNotNull(result3);
            Assert.IsInstanceOfType(result3, typeof(ModuleObject));
            Assert.AreEqual("MyModule::MySubmodule::MySubmodule2", ((ModuleObject)result3).Name);
        }

        [TestMethod]
        public void ExecuteModuleWithSelfMethodFile()
        {
            this.machine.ExecuteFile("MachineFiles\\ModuleWithSelfMethod.rb");

            var result1 = this.machine.ExecuteText("MyModule");
            var result2 = this.machine.ExecuteText("MyModule.foo");

            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(ModuleObject));
            Assert.AreEqual("MyModule", ((ModuleObject)result1).Name);
            Assert.AreSame(this.machine.ExecuteText("Module"), this.machine.ExecuteText("MyModule.class"));

            Assert.IsNotNull(result2);
            Assert.AreEqual(42, result2);
        }

        [TestMethod]
        public void ExecuteClassWithSelfMethodFile()
        {
            this.machine.ExecuteFile("MachineFiles\\ClassWithSelfMethod.rb");

            var result1 = this.machine.ExecuteText("MyClass");
            var result2 = this.machine.ExecuteText("MyClass.foo");

            Assert.IsNotNull(result1);
            Assert.IsInstanceOfType(result1, typeof(ModuleObject));
            Assert.AreEqual("MyClass", ((ModuleObject)result1).Name);
            Assert.AreSame(this.machine.ExecuteText("Class"), this.machine.ExecuteText("MyClass.class"));

            Assert.IsNotNull(result2);
            Assert.AreEqual(42, result2);
        }
    }
}
