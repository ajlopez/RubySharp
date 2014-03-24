namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class CompositeExpressionTests
    {
        [TestMethod]
        public void ExecuteTwoAssignCommands()
        {
            Context context = new Context();
            AssignExpression expr1 = new AssignExpression("one", new ConstantExpression(1));
            AssignExpression expr2 = new AssignExpression("two", new ConstantExpression(2));
            CompositeExpression expr = new CompositeExpression(new IExpression[] { expr1, expr2 });

            var result = expr.Evaluate(context);

            Assert.AreEqual(2, result);
            Assert.AreEqual(1, context.GetValue("one"));
            Assert.AreEqual(2, context.GetValue("two"));
        }

        [TestMethod]
        public void GetLocalVariables()
        {
            Context context = new Context();
            AssignExpression expr1 = new AssignExpression("one", new ConstantExpression(1));
            AssignExpression expr2 = new AssignExpression("two", new ConstantExpression(2));
            CompositeExpression expr = new CompositeExpression(new IExpression[] { expr1, expr2 });

            var result = expr.GetLocalVariables();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Contains("one"));
            Assert.IsTrue(result.Contains("two"));
        }

        [TestMethod]
        public void Equals()
        {
            AssignExpression aexpr1 = new AssignExpression("one", new ConstantExpression(1));
            AssignExpression aexpr2 = new AssignExpression("two", new ConstantExpression(2));

            CompositeExpression expr1 = new CompositeExpression(new IExpression[] { aexpr1, aexpr2 });
            CompositeExpression expr2 = new CompositeExpression(new IExpression[] { aexpr2, aexpr1 });
            CompositeExpression expr3 = new CompositeExpression(new IExpression[] { aexpr1 });
            CompositeExpression expr4 = new CompositeExpression(new IExpression[] { aexpr1, aexpr2 });

            Assert.IsTrue(expr1.Equals(expr4));
            Assert.IsTrue(expr4.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr4.GetHashCode());

            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals(123));
            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr2.Equals(expr1));
            Assert.IsFalse(expr1.Equals(expr3));
            Assert.IsFalse(expr3.Equals(expr1));
        }
    }
}
