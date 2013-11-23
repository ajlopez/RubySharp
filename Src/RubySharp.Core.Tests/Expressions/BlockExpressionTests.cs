﻿namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Language;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class BlockExpressionTests
    {
        [TestMethod]
        public void EvaluateBlockExpression()
        {
            Block block = new Block(null, new ConstantExpression(1));
            BlockExpression expr = new BlockExpression(block);

            Assert.AreSame(block, expr.Evaluate(null));
        }

        [TestMethod]
        public void Equals()
        {
            BlockExpression expr1 = new BlockExpression(new Block(new string[] { "a" }, new NameExpression("a")));
            BlockExpression expr2 = new BlockExpression(new Block(new string[] { "a" }, new NameExpression("b")));
            BlockExpression expr3 = new BlockExpression(new Block(new string[] { "b" }, new NameExpression("a")));
            BlockExpression expr4 = new BlockExpression(new Block(new string[] { "a", "b" }, new NameExpression("a")));
            BlockExpression expr5 = new BlockExpression(new Block(null, new NameExpression("a")));
            BlockExpression expr6 = new BlockExpression(new Block(new string[] { "a" }, new NameExpression("a")));

            Assert.IsFalse(expr1.Equals(123));
            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals("foo"));

            Assert.IsTrue(expr1.Equals(expr6));
            Assert.IsTrue(expr6.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr6.GetHashCode());

            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr2.Equals(expr1));
            Assert.IsFalse(expr1.Equals(expr3));
            Assert.IsFalse(expr3.Equals(expr1));
            Assert.IsFalse(expr1.Equals(expr4));
            Assert.IsFalse(expr4.Equals(expr1));
            Assert.IsFalse(expr1.Equals(expr5));
            Assert.IsFalse(expr5.Equals(expr1));
        }
    }
}
