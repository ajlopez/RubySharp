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
    public class AssignClassVarExpressionTests
    {
        [TestMethod]
        public void AssignValue()
        {
            AssignClassVarExpression cmd = new AssignClassVarExpression("one", new ConstantExpression(1));
            DynamicClass cls = new DynamicClass(null);
            DynamicObject obj = new DynamicObject(cls);
            Context context = new Context(obj, null);

            var result = cmd.Evaluate(context);

            Assert.AreEqual(1, result);
            Assert.AreEqual(1, cls.GetValue("one"));
        }

        [TestMethod]
        public void Equals()
        {
            AssignClassVarExpression cmd1 = new AssignClassVarExpression("a", new ConstantExpression(1));
            AssignClassVarExpression cmd2 = new AssignClassVarExpression("a", new ConstantExpression(2));
            AssignClassVarExpression cmd3 = new AssignClassVarExpression("b", new ConstantExpression(1));
            AssignClassVarExpression cmd4 = new AssignClassVarExpression("a", new ConstantExpression(1));

            Assert.IsTrue(cmd1.Equals(cmd4));
            Assert.IsTrue(cmd4.Equals(cmd1));
            Assert.AreEqual(cmd1.GetHashCode(), cmd4.GetHashCode());

            Assert.IsFalse(cmd1.Equals(null));
            Assert.IsFalse(cmd1.Equals(cmd2));
            Assert.IsFalse(cmd1.Equals(cmd3));
            Assert.IsFalse(cmd1.Equals(123));
        }
    }
}
