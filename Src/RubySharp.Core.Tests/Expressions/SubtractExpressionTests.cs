namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class SubtractExpressionTests
    {
        [TestMethod]
        public void SubtractTwoIntegers()
        {
            SubtractExpression expr = new SubtractExpression(new ConstantExpression(2), new ConstantExpression(1));
            Assert.IsNotNull(expr.LeftExpression);
            Assert.IsNotNull(expr.RightExpression);
            Assert.AreEqual(1, expr.Evaluate(null));
        }


        [TestMethod]
        public void SubtractDoubleFromInteger()
        {
            SubtractExpression expr = new SubtractExpression(new ConstantExpression(1), new ConstantExpression(2.5));

            Assert.AreEqual(1 - 2.5, expr.Evaluate(null));
        }

        [TestMethod]
        public void SubtractIntegerFromDouble()
        {
            SubtractExpression expr = new SubtractExpression(new ConstantExpression(2.5), new ConstantExpression(1));

            Assert.AreEqual(2.5 - 1, expr.Evaluate(null));
        }

        [TestMethod]
        public void SubtractTwoDoubles()
        {
            SubtractExpression expr = new SubtractExpression(new ConstantExpression(2.5), new ConstantExpression(3.7));

            Assert.AreEqual(2.5 - 3.7, expr.Evaluate(null));
        }


        [TestMethod]
        public void Equals()
        {
            SubtractExpression expr1 = new SubtractExpression(new ConstantExpression(1), new ConstantExpression(2));
            SubtractExpression expr2 = new SubtractExpression(new ConstantExpression(1), new ConstantExpression(3));
            SubtractExpression expr3 = new SubtractExpression(new ConstantExpression(1), new ConstantExpression(2));
            SubtractExpression expr4 = new SubtractExpression(new ConstantExpression(2), new ConstantExpression(2));

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
