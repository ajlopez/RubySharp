namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class NegativeExpressionTests
    {
        [TestMethod]
        public void NegativeIntegers()
        {
            NegativeExpression expr = new NegativeExpression(new ConstantExpression(1));

            Assert.AreEqual(-1, expr.Evaluate(null));
        }

        [TestMethod]
        public void Equals()
        {
            NegativeExpression expr1 = new NegativeExpression(new ConstantExpression(1));
            NegativeExpression expr2 = new NegativeExpression(new ConstantExpression(2));
            NegativeExpression expr3 = new NegativeExpression(new ConstantExpression(1));

            Assert.IsTrue(expr1.Equals(expr3));
            Assert.IsTrue(expr3.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr3.GetHashCode());

            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals("foo"));
            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr2.Equals(expr1));
        }
    }
}
