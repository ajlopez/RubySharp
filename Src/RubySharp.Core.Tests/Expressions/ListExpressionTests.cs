namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class ListExpressionTests
    {
        [TestMethod]
        public void EvaluateSimpleList()
        {
            ListExpression expr = new ListExpression(new IExpression[] { new ConstantExpression(1), new ConstantExpression(2) });

            var result = expr.Evaluate(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IList<object>));

            var list = (IList<object>)result;

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(2, list[1]);
        }
    }
}
