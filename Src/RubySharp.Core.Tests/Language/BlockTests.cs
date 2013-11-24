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
            Block block = new Block(null, new ConstantExpression(1), null);

            Assert.AreEqual(1, block.Apply(null));
        }

        [TestMethod]
        public void CreateAndEvaluateBlockWithFreeVariable()
        {
            Context context = new Context();
            context.SetLocalValue("a", 1);
            Block block = new Block(null, new AddExpression(new NameExpression("a"), new ConstantExpression(1)), context);

            Assert.AreEqual(2, block.Apply(null));
        }

        [TestMethod]
        public void CreateAndEvaluateBlockWithArguments()
        {
            Block block = new Block(new string[] { "a", "b" }, new AddExpression(new NameExpression("a"), new NameExpression("b")), null);

            Assert.AreEqual(3, block.Apply(new object[] { 1, 2 }));
        }

        [TestMethod]
        public void CreateAndEvaluateBlockWithNonProvidedArgument()
        {
            Context context = new Context();
            context.SetLocalValue("a", 1);
            Block block = new Block(new string[] { "a" }, new NameExpression("a"), context);

            Assert.IsNull(block.Apply(new object[] { }));
        }
    }
}
