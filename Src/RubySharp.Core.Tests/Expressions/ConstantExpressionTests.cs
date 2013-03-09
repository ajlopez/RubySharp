namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class ConstantExpressionTests
    {
        [TestMethod]
        public void EvaluateAsInteger()
        {
            ConstantExpression expr = new ConstantExpression(123);

            Assert.AreEqual(123, expr.Evaluate());
        }
    }
}
