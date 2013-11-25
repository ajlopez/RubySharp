namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class NegationExpressionTests
    {
        [TestMethod]
        public void NegationFalse()
        {
            NegationExpression expr = new NegationExpression(new ConstantExpression(false));

            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void NegationTrue()
        {
            NegationExpression expr = new NegationExpression(new ConstantExpression(true));

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void NegationEmptyString()
        {
            NegationExpression expr = new NegationExpression(new ConstantExpression(string.Empty));

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void NegationString()
        {
            NegationExpression expr = new NegationExpression(new ConstantExpression("foo"));

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void NegationInteger()
        {
            NegationExpression expr = new NegationExpression(new ConstantExpression(123));

            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void Equals()
        {
            NegationExpression expr1 = new NegationExpression(new ConstantExpression(1));
            NegationExpression expr2 = new NegationExpression(new ConstantExpression(2));
            NegationExpression expr3 = new NegationExpression(new ConstantExpression(1));

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
