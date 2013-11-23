namespace RubySharp.Core.Tests.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Language;

    [TestClass]
    public class BlockTests
    {
        [TestMethod]
        public void CreateAndEvaluateSimpleBlock()
        {
            Block block = new Block(null, new ConstantExpression(1));

            Assert.AreEqual(1, block.Apply(null, null));
        }

        [TestMethod]
        public void CreateAndEvaluateBlockWithFreeVariable()
        {
            Context context = new Context();
            context.SetLocalValue("a", 1);
            Block block = new Block(null, new AddExpression(new NameExpression("a"), new ConstantExpression(1)));

            Assert.AreEqual(2, block.Apply(context, null));
        }

        [TestMethod]
        public void CreateAndEvaluateBlockWithArguments()
        {
            Block block = new Block(new string[] { "a", "b" }, new AddExpression(new NameExpression("a"), new NameExpression("b")));

            Assert.AreEqual(3, block.Apply(null, new object[] { 1, 2 }));
        }

        [TestMethod]
        public void CreateAndEvaluateBlockWithNonProvidedArgument()
        {
            Context context = new Context();
            context.SetLocalValue("a", 1);
            Block block = new Block(new string[] { "a" }, new NameExpression("a"));

            Assert.IsNull(block.Apply(context, new object[] { }));
        }

        [TestMethod]
        public void Equals()
        {
            Block blk1 = new Block(new string[] { "a" }, new NameExpression("a"));
            Block blk2 = new Block(new string[] { "a" }, new NameExpression("b"));
            Block blk3 = new Block(new string[] { "b" }, new NameExpression("a"));
            Block blk4 = new Block(new string[] { "a", "b" }, new NameExpression("a"));
            Block blk5 = new Block(null, new NameExpression("a"));
            Block blk6 = new Block(new string[] { "a" }, new NameExpression("a"));

            Assert.IsFalse(blk1.Equals(123));
            Assert.IsFalse(blk1.Equals(null));
            Assert.IsFalse(blk1.Equals("foo"));

            Assert.IsTrue(blk1.Equals(blk6));
            Assert.IsTrue(blk6.Equals(blk1));
            Assert.AreEqual(blk1.GetHashCode(), blk6.GetHashCode());

            Assert.IsFalse(blk1.Equals(blk2));
            Assert.IsFalse(blk2.Equals(blk1));
            Assert.IsFalse(blk1.Equals(blk3));
            Assert.IsFalse(blk3.Equals(blk1));
            Assert.IsFalse(blk1.Equals(blk4));
            Assert.IsFalse(blk4.Equals(blk1));
            Assert.IsFalse(blk1.Equals(blk5));
            Assert.IsFalse(blk5.Equals(blk1));
        }
    }
}
