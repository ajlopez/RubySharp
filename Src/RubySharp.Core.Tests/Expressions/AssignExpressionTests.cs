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
            AssignExpression expr = new AssignExpression("one", new ConstantExpression(1));
            Context context = new Context();

            var result = expr.Evaluate(context);

            Assert.AreEqual(1, result);
            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void GetLocalVariables()
        {
            AssignExpression expr = new AssignExpression("one", new ConstantExpression(1));
            var result = expr.GetLocalVariables();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("one", result[0]);
        }

        [TestMethod]
        public void Equals()
        {
            AssignExpression expr1 = new AssignExpression("a", new ConstantExpression(1));
            AssignExpression expr2 = new AssignExpression("a", new ConstantExpression(2));
            AssignExpression expr3 = new AssignExpression("b", new ConstantExpression(1));
            AssignExpression expr4 = new AssignExpression("a", new ConstantExpression(1));

            Assert.IsTrue(expr1.Equals(expr4));
            Assert.IsTrue(expr4.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr4.GetHashCode());

            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr1.Equals(expr3));
            Assert.IsFalse(expr1.Equals(123));
        }
    }
}
