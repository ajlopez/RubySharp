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
        public void ExecuteThenWhenConditionIsTrue()
        {
            IfCommand cmd = new IfCommand(new ConstantExpression(true), new AssignCommand("one", new ConstantExpression(1)));
            Context context = new Context();
            Assert.AreEqual(1, cmd.Evaluate(context));
            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void DontExecuteThenWhenConditionIsFalse()
        {
            IfCommand cmd = new IfCommand(new ConstantExpression(false), new AssignCommand("one", new ConstantExpression(1)));
            Context context = new Context();
            Assert.IsNull(cmd.Evaluate(context));
            Assert.IsNull(context.GetValue("one"));
        }

        [TestMethod]
        public void DontExecuteThenWhenConditionIsNull()
        {
            IfCommand cmd = new IfCommand(new ConstantExpression(null), new AssignCommand("one", new ConstantExpression(1)));
            Context context = new Context();
            Assert.IsNull(cmd.Evaluate(context));
            Assert.IsNull(context.GetValue("one"));
        }

        [TestMethod]
        public void ExecuteElseWhenConditionIsFalse()
        {
            IfCommand cmd = new IfCommand(new ConstantExpression(false), new AssignCommand("one", new ConstantExpression(1)), new AssignCommand("two", new ConstantExpression(2)));
            Context context = new Context();
            Assert.AreEqual(2, cmd.Evaluate(context));
            Assert.IsNull(context.GetValue("one"));
            Assert.AreEqual(2, context.GetValue("two"));
        }

        [TestMethod]
        public void ExecuteElseWhenConditionIsNull()
        {
            IfCommand cmd = new IfCommand(new ConstantExpression(null), new AssignCommand("one", new ConstantExpression(1)), new AssignCommand("two", new ConstantExpression(2)));
            Context context = new Context();
            Assert.AreEqual(cmd.Evaluate(context), 2);
            Assert.IsNull(context.GetValue("one"));
            Assert.AreEqual(2, context.GetValue("two"));
        }

        [TestMethod]
        public void ExecuteThenWhenConditionIsZero()
        {
            IfCommand cmd = new IfCommand(new ConstantExpression(0), new AssignCommand("one", new ConstantExpression(1)));
            Context context = new Context();
            Assert.AreEqual(1, cmd.Evaluate(context));
            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void Equals()
        {
            IfCommand cmd1 = new IfCommand(new ConstantExpression(1), new AssignCommand("one", new ConstantExpression(1)));
            IfCommand cmd2 = new IfCommand(new ConstantExpression(2), new AssignCommand("one", new ConstantExpression(1)));
            IfCommand cmd3 = new IfCommand(new ConstantExpression(1), new AssignCommand("one", new ConstantExpression(2)));
            IfCommand cmd4 = new IfCommand(new ConstantExpression(1), new AssignCommand("one", new ConstantExpression(1)), new AssignCommand("two", new ConstantExpression(2)));
            IfCommand cmd5 = new IfCommand(new ConstantExpression(1), new AssignCommand("one", new ConstantExpression(1)));

            Assert.IsTrue(cmd1.Equals(cmd5));
            Assert.IsTrue(cmd5.Equals(cmd1));
            Assert.AreEqual(cmd1.GetHashCode(), cmd5.GetHashCode());

            Assert.IsFalse(cmd1.Equals(null));
            Assert.IsFalse(cmd1.Equals(123));
            Assert.IsFalse(cmd1.Equals(cmd2));
            Assert.IsFalse(cmd1.Equals(cmd3));
            Assert.IsFalse(cmd1.Equals(cmd4));
            Assert.IsFalse(cmd2.Equals(cmd1));
            Assert.IsFalse(cmd3.Equals(cmd1));
            Assert.IsFalse(cmd4.Equals(cmd1));
        }
    }
}
