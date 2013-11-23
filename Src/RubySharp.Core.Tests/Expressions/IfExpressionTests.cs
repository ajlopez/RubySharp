namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class IfExpressionTests
    {
        [TestMethod]
        public void ExecuteThenWhenConditionIsTrue()
        {
            IfExpression cmd = new IfExpression(new ConstantExpression(true), new AssignExpression("one", new ConstantExpression(1)));
            Context context = new Context();
            Assert.AreEqual(1, cmd.Evaluate(context));
            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void DontExecuteThenWhenConditionIsFalse()
        {
            IfExpression cmd = new IfExpression(new ConstantExpression(false), new AssignExpression("one", new ConstantExpression(1)));
            Context context = new Context();
            Assert.IsNull(cmd.Evaluate(context));
            Assert.IsNull(context.GetValue("one"));
        }

        [TestMethod]
        public void DontExecuteThenWhenConditionIsNull()
        {
            IfExpression cmd = new IfExpression(new ConstantExpression(null), new AssignExpression("one", new ConstantExpression(1)));
            Context context = new Context();
            Assert.IsNull(cmd.Evaluate(context));
            Assert.IsNull(context.GetValue("one"));
        }

        [TestMethod]
        public void ExecuteElseWhenConditionIsFalse()
        {
            IfExpression cmd = new IfExpression(new ConstantExpression(false), new AssignExpression("one", new ConstantExpression(1)), new AssignExpression("two", new ConstantExpression(2)));
            Context context = new Context();
            Assert.AreEqual(2, cmd.Evaluate(context));
            Assert.IsNull(context.GetValue("one"));
            Assert.AreEqual(2, context.GetValue("two"));
        }

        [TestMethod]
        public void ExecuteElseWhenConditionIsNull()
        {
            IfExpression cmd = new IfExpression(new ConstantExpression(null), new AssignExpression("one", new ConstantExpression(1)), new AssignExpression("two", new ConstantExpression(2)));
            Context context = new Context();
            Assert.AreEqual(cmd.Evaluate(context), 2);
            Assert.IsNull(context.GetValue("one"));
            Assert.AreEqual(2, context.GetValue("two"));
        }

        [TestMethod]
        public void ExecuteThenWhenConditionIsZero()
        {
            IfExpression cmd = new IfExpression(new ConstantExpression(0), new AssignExpression("one", new ConstantExpression(1)));
            Context context = new Context();
            Assert.AreEqual(1, cmd.Evaluate(context));
            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void Equals()
        {
            IfExpression cmd1 = new IfExpression(new ConstantExpression(1), new AssignExpression("one", new ConstantExpression(1)));
            IfExpression cmd2 = new IfExpression(new ConstantExpression(2), new AssignExpression("one", new ConstantExpression(1)));
            IfExpression cmd3 = new IfExpression(new ConstantExpression(1), new AssignExpression("one", new ConstantExpression(2)));
            IfExpression cmd4 = new IfExpression(new ConstantExpression(1), new AssignExpression("one", new ConstantExpression(1)), new AssignExpression("two", new ConstantExpression(2)));
            IfExpression cmd5 = new IfExpression(new ConstantExpression(1), new AssignExpression("one", new ConstantExpression(1)));

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
