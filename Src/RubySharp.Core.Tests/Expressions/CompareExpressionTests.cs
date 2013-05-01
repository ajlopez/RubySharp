namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class CompareExpressionTests
    {
        [TestMethod]
        public void CompareEqualIntegers()
        {
            CompareExpression expr = new CompareExpression(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.Equal);
            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareNotEqualIntegers()
        {
            CompareExpression expr = new CompareExpression(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.NotEqual);
            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareLessIntegers()
        {
            CompareExpression expr = new CompareExpression(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.Less);
            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareGreaterIntegers()
        {
            CompareExpression expr = new CompareExpression(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.Greater);
            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareGreaterOrEqualIntegers()
        {
            CompareExpression expr = new CompareExpression(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.GreaterOrEqual);
            Assert.AreEqual(false, expr.Evaluate(null));
        }

        [TestMethod]
        public void CompareLessOrEqualIntegers()
        {
            CompareExpression expr = new CompareExpression(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.LessOrEqual);
            Assert.AreEqual(true, expr.Evaluate(null));
        }

        [TestMethod]
        public void Equals()
        {
            CompareExpression expr1 = new CompareExpression(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.Equal);
            CompareExpression expr2 = new CompareExpression(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.NotEqual);
            CompareExpression expr3 = new CompareExpression(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.Equal);

            Assert.IsTrue(expr1.Equals(expr3));
            Assert.IsTrue(expr3.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr3.GetHashCode());

            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals(123));
            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr2.Equals(expr1));
        }
    }
}
