namespace RubySharp.Core.Tests.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Commands;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class AssignCommandTests
    {
        [TestMethod]
        public void AssignValue()
        {
            AssignCommand cmd = new AssignCommand("one", new ConstantExpression(1));
            Context context = new Context();

            var result = cmd.Execute(context);

            Assert.AreEqual(1, result);
            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void Equals()
        {
            AssignCommand cmd1 = new AssignCommand("a", new ConstantExpression(1));
            AssignCommand cmd2 = new AssignCommand("a", new ConstantExpression(2));
            AssignCommand cmd3 = new AssignCommand("b", new ConstantExpression(1));
            AssignCommand cmd4 = new AssignCommand("a", new ConstantExpression(1));

            Assert.IsTrue(cmd1.Equals(cmd4));
            Assert.IsTrue(cmd4.Equals(cmd1));
            Assert.AreEqual(cmd1.GetHashCode(), cmd4.GetHashCode());

            Assert.IsFalse(cmd1.Equals(null));
            Assert.IsFalse(cmd1.Equals(cmd2));
            Assert.IsFalse(cmd1.Equals(cmd3));
            Assert.IsFalse(cmd1.Equals(123));
        }
    }
}
