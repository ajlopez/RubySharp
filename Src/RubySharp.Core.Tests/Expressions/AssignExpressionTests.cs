namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class AssignExpressionTests
    {
        [TestMethod]
        public void AssignValue()
        {
            AssignExpression cmd = new AssignExpression("one", new ConstantExpression(1));
            Context context = new Context();

            var result = cmd.Evaluate(context);

            Assert.AreEqual(1, result);
            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void Equals()
        {
            AssignExpression cmd1 = new AssignExpression("a", new ConstantExpression(1));
            AssignExpression cmd2 = new AssignExpression("a", new ConstantExpression(2));
            AssignExpression cmd3 = new AssignExpression("b", new ConstantExpression(1));
            AssignExpression cmd4 = new AssignExpression("a", new ConstantExpression(1));

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
