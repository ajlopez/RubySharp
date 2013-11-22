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
            context.SetValue("a", 1);
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
            context.SetValue("a", 1);
            Block block = new Block(new string[] { "a" }, new NameExpression("a"));

            Assert.IsNull(block.Apply(context, new object[] { }));
        }

        [TestMethod]
        public void CreateAndEvaluateBlockWithFreeVariableAndArgument()
        {
            Context context = new Context();
            context.SetValue("a", 1);
            Block block = new Block(new string[] { "b" }, new SubtractExpression(new NameExpression("a"), new NameExpression("b")));

            Assert.AreEqual(-2, block.Apply(context, new object[] { 3 }));
        }
    }
}
