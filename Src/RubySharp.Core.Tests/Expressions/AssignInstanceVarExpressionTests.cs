﻿namespace RubySharp.Core.Tests.Expressions
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
            AssignInstanceVarExpression cmd = new AssignInstanceVarExpression("one", new ConstantExpression(1));
            DynamicObject obj = new DynamicObject(null);
            Context context = new Context(obj, null);

            var result = cmd.Evaluate(context);

            Assert.AreEqual(1, result);
            Assert.AreEqual(1, obj.GetValue("one"));
        }

        [TestMethod]
        public void Equals()
        {
            AssignInstanceVarExpression cmd1 = new AssignInstanceVarExpression("a", new ConstantExpression(1));
            AssignInstanceVarExpression cmd2 = new AssignInstanceVarExpression("a", new ConstantExpression(2));
            AssignInstanceVarExpression cmd3 = new AssignInstanceVarExpression("b", new ConstantExpression(1));
            AssignInstanceVarExpression cmd4 = new AssignInstanceVarExpression("a", new ConstantExpression(1));

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
