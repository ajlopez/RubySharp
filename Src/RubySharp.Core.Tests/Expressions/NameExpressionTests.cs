namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Functions;

    [TestClass]
    public class NameExpressionTests
    {
        [TestMethod]
        public void EvaluateUndefinedName()
        {
            NameExpression expr = new NameExpression("foo");
            Context context = new Context();

            Assert.IsNull(expr.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateDefinedName()
        {
            NameExpression expr = new NameExpression("one");
            Context context = new Context();
            context.SetValue("one", 1);

            Assert.AreEqual(1, expr.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateDefinedFunction()
        {
            NameExpression expr = new NameExpression("foo");
            Context context = new Context();
            context.SetValue("foo", new DefinedFunction(new ConstantExpression(1), new string[0], context));

            Assert.AreEqual(1, expr.Evaluate(context));
        }

        [TestMethod]
        public void Equals()
        {
            NameExpression expr1 = new NameExpression("one");
            NameExpression expr2 = new NameExpression("two");
            NameExpression expr3 = new NameExpression("one");

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
