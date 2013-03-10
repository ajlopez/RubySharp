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
    public class IfCommandTests
    {
        [TestMethod]
        public void ExecuteSimpleIfWhenConditionIsTrue()
        {
            IfCommand cmd = new IfCommand(new ConstantExpression(true), new AssignCommand("one", new ConstantExpression(1)));
            Context context = new Context();
            Assert.AreEqual(1, cmd.Execute(context));
            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void ExecuteSimpleIfWhenConditionIsFalse()
        {
            IfCommand cmd = new IfCommand(new ConstantExpression(false), new AssignCommand("one", new ConstantExpression(1)));
            Context context = new Context();
            Assert.IsNull(cmd.Execute(context));
            Assert.IsNull(context.GetValue("one"));
        }

        [TestMethod]
        public void ExecuteSimpleIfWhenConditionIsNull()
        {
            IfCommand cmd = new IfCommand(new ConstantExpression(null), new AssignCommand("one", new ConstantExpression(1)));
            Context context = new Context();
            Assert.IsNull(cmd.Execute(context));
            Assert.IsNull(context.GetValue("one"));
        }

        [TestMethod]
        public void ExecuteSimpleIfWhenConditionIsZero()
        {
            IfCommand cmd = new IfCommand(new ConstantExpression(0), new AssignCommand("one", new ConstantExpression(1)));
            Context context = new Context();
            Assert.AreEqual(1, cmd.Execute(context));
            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void Equals()
        {
            IfCommand cmd1 = new IfCommand(new ConstantExpression(1), new AssignCommand("one", new ConstantExpression(1)));
            IfCommand cmd2 = new IfCommand(new ConstantExpression(2), new AssignCommand("one", new ConstantExpression(1)));
            IfCommand cmd3 = new IfCommand(new ConstantExpression(1), new AssignCommand("one", new ConstantExpression(2)));
            IfCommand cmd4 = new IfCommand(new ConstantExpression(1), new AssignCommand("one", new ConstantExpression(1)));

            Assert.IsTrue(cmd1.Equals(cmd4));
            Assert.IsTrue(cmd4.Equals(cmd1));
            Assert.AreEqual(cmd1.GetHashCode(), cmd4.GetHashCode());

            Assert.IsFalse(cmd1.Equals(null));
            Assert.IsFalse(cmd1.Equals(123));
            Assert.IsFalse(cmd1.Equals(cmd2));
            Assert.IsFalse(cmd1.Equals(cmd3));
        }
    }
}
