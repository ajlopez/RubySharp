namespace RubySharp.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Functions;

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
        [DeploymentItem("MachineFiles", "MachineFiles")]
        public void ExecuteSimpleAssignFile()
        {
            Machine machine = new Machine();
            Assert.AreEqual(1, machine.ExecuteFile("MachineFiles\\SimpleAssign.rb"));
            Assert.AreEqual(1, machine.RootContext.GetValue("a"));
        }

        [TestMethod]
        [DeploymentItem("MachineFiles", "MachineFiles")]
        public void ExecuteSimpleAssignsFile()
        {
            Machine machine = new Machine();
            Assert.AreEqual(2, machine.ExecuteFile("MachineFiles\\SimpleAssigns.rb"));
            Assert.AreEqual(1, machine.RootContext.GetValue("a"));
            Assert.AreEqual(2, machine.RootContext.GetValue("b"));
        }

        [TestMethod]
        [DeploymentItem("MachineFiles", "MachineFiles")]
        public void ExecuteSimplePutsFile()
        {
            Machine machine = new Machine();
            StringWriter writer = new StringWriter();
            machine.RootContext.SetValue("puts", new PutsFunction(writer));
            Assert.AreEqual(null, machine.ExecuteFile("MachineFiles\\SimplePuts.rb"));
            Assert.AreEqual("hello\r\n", writer.ToString());
        }

        [TestMethod]
        [DeploymentItem("MachineFiles", "MachineFiles")]
        public void ExecuteSimpleDefFile()
        {
            Machine machine = new Machine();
            StringWriter writer = new StringWriter();
            machine.RootContext.SetValue("puts", new PutsFunction(writer));
            Assert.AreEqual(null, machine.ExecuteFile("MachineFiles\\SimpleDef.rb"));
            Assert.AreEqual("1\r\n", writer.ToString());
        }
    }
}
