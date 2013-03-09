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
    }
}
