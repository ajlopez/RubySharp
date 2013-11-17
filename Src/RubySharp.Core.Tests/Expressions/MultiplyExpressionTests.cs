﻿namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class MultiplyExpressionTests
    {
        [TestMethod]
        public void MultiplyTwoIntegers()
        {
            MultiplyExpression expr = new MultiplyExpression(new ConstantExpression(3), new ConstantExpression(2));

            Assert.AreEqual(6, expr.Evaluate(null));
        }

        [TestMethod]
        public void MultiplyIntegerByDouble()
        {
            MultiplyExpression expr = new MultiplyExpression(new ConstantExpression(2), new ConstantExpression(2.5));

            Assert.AreEqual(2 * 2.5, expr.Evaluate(null));
        }

        [TestMethod]
        public void MultiplyDoubleByInteger()
        {
            MultiplyExpression expr = new MultiplyExpression(new ConstantExpression(2.5), new ConstantExpression(3));

            Assert.AreEqual(2.5 * 3, expr.Evaluate(null));
        }

        [TestMethod]
        public void MultiplyTwoDoubles()
        {
            MultiplyExpression expr = new MultiplyExpression(new ConstantExpression(2.5), new ConstantExpression(3.7));

            Assert.AreEqual(2.5 * 3.7, expr.Evaluate(null));
        }

        [TestMethod]
        public void Equals()
        {
            MultiplyExpression expr1 = new MultiplyExpression(new ConstantExpression(1), new ConstantExpression(2));
            MultiplyExpression expr2 = new MultiplyExpression(new ConstantExpression(1), new ConstantExpression(3));
            MultiplyExpression expr3 = new MultiplyExpression(new ConstantExpression(1), new ConstantExpression(2));
            MultiplyExpression expr4 = new MultiplyExpression(new ConstantExpression(2), new ConstantExpression(2));

            Assert.IsTrue(expr1.Equals(expr3));
            Assert.IsTrue(expr3.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr3.GetHashCode());

            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals("foo"));
            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr2.Equals(expr1));
            Assert.IsFalse(expr1.Equals(expr4));
            Assert.IsFalse(expr4.Equals(expr1));
        }
    }
}
