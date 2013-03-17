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
            this.machine.RootContext.SetValue("puts", new PutsFunction(writer));
            Assert.AreEqual(null, this.machine.ExecuteFile("MachineFiles\\SimplePuts.rb"));
            Assert.AreEqual("hello\r\n", writer.ToString());
        }

        [TestMethod]
        public void ExecuteSimpleDefFile()
        {
            StringWriter writer = new StringWriter();
            this.machine.RootContext.SetValue("puts", new PutsFunction(writer));
            Assert.AreEqual(null, this.machine.ExecuteFile("MachineFiles\\SimpleDef.rb"));
            Assert.AreEqual("1\r\n", writer.ToString());
        }

        [TestMethod]
        public void ExecuteSimpleClassFile()
        {
            StringWriter writer = new StringWriter();
            this.machine.RootContext.SetValue("puts", new PutsFunction(writer));
            Assert.AreEqual(null, this.machine.ExecuteFile("MachineFiles\\SimpleClass.rb"));
            Assert.AreEqual("Hello\r\n", writer.ToString());

            var result = this.machine.RootContext.GetValue("Dog");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DefinedClass));

            var dclass = (DefinedClass)result;

            Assert.AreEqual("Dog", dclass.Name);
        }
    }
}
