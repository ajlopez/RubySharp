namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class ArrayExpressionTests
    {
        [TestMethod]
        public void EvaluateSimpleList()
        {
            ArrayExpression expr = new ArrayExpression(new IExpression[] { new ConstantExpression(1), new ConstantExpression(2) });

            var result = expr.Evaluate(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IList));

            var list = (IList)result;

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(2, list[1]);
        }

        [TestMethod]
        public void GetLocalVariables()
        {
            ArrayExpression expr = new ArrayExpression(new IExpression[] { new ConstantExpression(1), new ConstantExpression(2) });
            Assert.IsNull(expr.GetLocalVariables());
        }

        [TestMethod]
        public void Equals()
        {
            ArrayExpression expr1 = new ArrayExpression(new IExpression[] { new ConstantExpression(1), new ConstantExpression(2) });
            ArrayExpression expr2 = new ArrayExpression(new IExpression[] { new ConstantExpression(1), new ConstantExpression(2), new ConstantExpression(3) });
            ArrayExpression expr3 = new ArrayExpression(new IExpression[] { new ConstantExpression(1), new ConstantExpression(3) });
            ArrayExpression expr4 = new ArrayExpression(new IExpression[] { });
            ArrayExpression expr5 = new ArrayExpression(new IExpression[] { new ConstantExpression(1), new ConstantExpression(2) });

            Assert.IsTrue(expr1.Equals(expr5));
            Assert.IsTrue(expr5.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr5.GetHashCode());

            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals(123));
            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr2.Equals(expr1));
            Assert.IsFalse(expr1.Equals(expr3));
            Assert.IsFalse(expr3.Equals(expr1));
            Assert.IsFalse(expr1.Equals(expr4));
            Assert.IsFalse(expr4.Equals(expr1));
        }
    }
}
