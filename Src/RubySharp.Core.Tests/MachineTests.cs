namespace RubySharp.Core.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
