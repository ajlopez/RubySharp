namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class AddExpressionTests
    {
        [TestMethod]
        public void AddTwoIntegers()
        {
            AddExpression expr = new AddExpression(new ConstantExpression(1), new ConstantExpression(2));

            Assert.AreEqual(3, expr.Evaluate(null));
        }

        [TestMethod]
        public void Equals()
        {
            AddExpression expr1 = new AddExpression(new ConstantExpression(1), new ConstantExpression(2));
            AddExpression expr2 = new AddExpression(new ConstantExpression(1), new ConstantExpression(3));
            AddExpression expr3 = new AddExpression(new ConstantExpression(1), new ConstantExpression(2));

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
