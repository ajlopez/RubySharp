namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;

    [TestClass]
    public class ClassVarExpressionTests
    {
        [TestMethod]
        public void EvaluateUndefinedClassVar()
        {
            ClassVarExpression expr = new ClassVarExpression("foo");
            DynamicClass cls = new DynamicClass(null);
            DynamicObject obj = new DynamicObject(cls);
            Context context = new Context(obj, null);

            Assert.IsNull(expr.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateDefinedClassVar()
        {
            ClassVarExpression expr = new ClassVarExpression("one");
            DynamicClass cls = new DynamicClass(null);
            DynamicObject obj = new DynamicObject(cls);
            cls.SetValue("one", 1);
            Context context = new Context(obj, null);

            Assert.AreEqual(1, expr.Evaluate(context));
        }

        [TestMethod]
        public void Equals()
        {
            ClassVarExpression expr1 = new ClassVarExpression("one");
            ClassVarExpression expr2 = new ClassVarExpression("two");
            ClassVarExpression expr3 = new ClassVarExpression("one");

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
