namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class AssignIndexedExpressionTests
    {
        [TestMethod]
        public void SimpleEvaluate()
        {
            var array = new int[] { 1, 2, 3 };
            AssignIndexedExpression expr = new AssignIndexedExpression(new ConstantExpression(array), new ConstantExpression(1), new ConstantExpression(10));

            var result = expr.Evaluate(null);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result);

            Assert.AreEqual(1, array[0]);
            Assert.AreEqual(10, array[1]);
            Assert.AreEqual(3, array[2]);
        }

        [TestMethod]
        public void Equals()
        {
            AssignIndexedExpression expr1 = new AssignIndexedExpression(new ConstantExpression(1), new ConstantExpression(2), new ConstantExpression(3));
            AssignIndexedExpression expr2 = new AssignIndexedExpression(new ConstantExpression(10), new ConstantExpression(2), new ConstantExpression(3));
            AssignIndexedExpression expr3 = new AssignIndexedExpression(new ConstantExpression(1), new ConstantExpression(20), new ConstantExpression(3));
            AssignIndexedExpression expr4 = new AssignIndexedExpression(new ConstantExpression(1), new ConstantExpression(2), new ConstantExpression(30));
            AssignIndexedExpression expr5 = new AssignIndexedExpression(new ConstantExpression(1), new ConstantExpression(2), new ConstantExpression(3));

            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals(123));
            Assert.IsFalse(expr1.Equals("foo"));

            Assert.IsTrue(expr1.Equals(expr5));
            Assert.IsTrue(expr5.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr5.GetHashCode());

            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr2.Equals(expr1));
            Assert.IsFalse(expr1.Equals(expr3));
            Assert.IsFalse(expr3.Equals(expr1));
            Assert.IsFalse(expr1.Equals(expr4));
            Assert.IsFalse(expr4.Equals(expr1));
        }
    }
}
