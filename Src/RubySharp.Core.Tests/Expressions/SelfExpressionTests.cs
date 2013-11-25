namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class SelfExpressionTests
    {
        [TestMethod]
        public void Evaluate()
        {
            Machine machine = new Machine();
            SelfExpression expr = new SelfExpression();

            var result = expr.Evaluate(machine.RootContext);

            Assert.IsNotNull(result);
            Assert.AreSame(machine.RootContext.Self, result);
        }

        [TestMethod]
        public void Equals()
        {
            SelfExpression expr1 = new SelfExpression();
            SelfExpression expr2 = new SelfExpression();

            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals(123));
            Assert.IsFalse(expr1.Equals("foo"));

            Assert.IsTrue(expr1.Equals(expr2));
            Assert.IsTrue(expr2.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr2.GetHashCode());
        }
    }
}
