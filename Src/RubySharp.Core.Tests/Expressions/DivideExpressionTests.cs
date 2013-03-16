namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class DivideExpressionTests
    {
        [TestMethod]
        public void DivideTwoIntegers()
        {
            DivideExpression expr = new DivideExpression(new ConstantExpression(6), new ConstantExpression(2));

            Assert.AreEqual(3, expr.Evaluate(null));
        }

        [TestMethod]
        public void Equals()
        {
            DivideExpression expr1 = new DivideExpression(new ConstantExpression(1), new ConstantExpression(2));
            DivideExpression expr2 = new DivideExpression(new ConstantExpression(1), new ConstantExpression(3));
            DivideExpression expr3 = new DivideExpression(new ConstantExpression(1), new ConstantExpression(2));
            DivideExpression expr4 = new DivideExpression(new ConstantExpression(2), new ConstantExpression(2));

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
