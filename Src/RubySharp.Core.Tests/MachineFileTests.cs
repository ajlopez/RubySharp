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
            this.machine.RootContext.SetLocalValue("puts", new PutsFunction(writer));
            Assert.AreEqual(null, this.machine.ExecuteFile("MachineFiles\\SimplePuts.rb"));
            Assert.AreEqual("hello\r\n", writer.ToString());
        }

        [TestMethod]
        public void ExecuteSimpleDefFile()
        {
            StringWriter writer = new StringWriter();
            this.machine.RootContext.SetLocalValue("puts", new PutsFunction(writer));
            Assert.AreEqual(null, this.machine.ExecuteFile("MachineFiles\\SimpleDef.rb"));
            Assert.AreEqual("1\r\n", writer.ToString());
        }

        [TestMethod]
        public void ExecuteSimpleClassFile()
        {
            StringWriter writer = new StringWriter();
            this.machine.RootContext.SetLocalValue("puts", new PutsFunction(writer));
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
    }
}
