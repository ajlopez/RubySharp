namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Language;

    [TestClass]
    public class AssignInstanceVarExpressionTests
    {
        [TestMethod]
        public void AssignValue()
        {
            AssignInstanceVarExpression expr = new AssignInstanceVarExpression("one", new ConstantExpression(1));
            DynamicObject obj = new DynamicObject(null);
            Context context = new Context(obj, null);

            var result = expr.Evaluate(context);

            Assert.AreEqual(1, result);
            Assert.AreEqual(1, obj.GetValue("one"));
        }

        [TestMethod]
        public void GetLocalVariables()
        {
            AssignInstanceVarExpression expr = new AssignInstanceVarExpression("one", new ConstantExpression(1));
            Assert.IsNull(expr.GetLocalVariables());
        }

        [TestMethod]
        public void Equals()
        {
            AssignInstanceVarExpression expr1 = new AssignInstanceVarExpression("a", new ConstantExpression(1));
            AssignInstanceVarExpression expr2 = new AssignInstanceVarExpression("a", new ConstantExpression(2));
            AssignInstanceVarExpression expr3 = new AssignInstanceVarExpression("b", new ConstantExpression(1));
            AssignInstanceVarExpression expr4 = new AssignInstanceVarExpression("a", new ConstantExpression(1));

            Assert.IsTrue(expr1.Equals(expr4));
            Assert.IsTrue(expr4.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr4.GetHashCode());

            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr1.Equals(expr3));
            Assert.IsFalse(expr1.Equals(123));
        }
    }
}
