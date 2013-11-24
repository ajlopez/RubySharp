namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class RangeExpressionTests
    {
        [TestMethod]
        public void SimpleEvaluate()
        {
            RangeExpression expr = new RangeExpression(new ConstantExpression(1), new ConstantExpression(4));

            var result = expr.Evaluate(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<int>));

            var range = (IEnumerable<int>)result;

            int total = 0;

            foreach (int k in range)
                total += k;

            Assert.AreEqual(10, total);
        }
        [TestMethod]

        public void Equals()
        {
            RangeExpression expr1 = new RangeExpression(new ConstantExpression(1), new ConstantExpression(2));
            RangeExpression expr2 = new RangeExpression(new ConstantExpression(1), new ConstantExpression(3));
            RangeExpression expr3 = new RangeExpression(new ConstantExpression(2), new ConstantExpression(2));
            RangeExpression expr4 = new RangeExpression(new ConstantExpression(1), new ConstantExpression(2));

            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals(123));
            Assert.IsFalse(expr1.Equals("foo"));

            Assert.IsTrue(expr1.Equals(expr4));
            Assert.IsTrue(expr4.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr4.GetHashCode());

            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr2.Equals(expr1));
            Assert.IsFalse(expr1.Equals(expr3));
            Assert.IsFalse(expr3.Equals(expr1));
        }
    }
}
