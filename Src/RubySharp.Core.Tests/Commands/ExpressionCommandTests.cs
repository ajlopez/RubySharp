namespace RubySharp.Core.Tests.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Commands;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class ExpressionCommandTests
    {
        [TestMethod]
        public void ExecuteConstantExpression()
        {
            ExpressionCommand cmd = new ExpressionCommand(new ConstantExpression(1));

            Assert.AreEqual(1, cmd.Execute(null));
        }

        [TestMethod]
        public void Equals()
        {
            ExpressionCommand cmd1 = new ExpressionCommand(new ConstantExpression(1));
            ExpressionCommand cmd2 = new ExpressionCommand(new ConstantExpression(2));
            ExpressionCommand cmd3 = new ExpressionCommand(new ConstantExpression(1));

            Assert.IsTrue(cmd1.Equals(cmd3));
            Assert.IsTrue(cmd3.Equals(cmd1));
            Assert.AreEqual(cmd1.GetHashCode(), cmd3.GetHashCode());

            Assert.IsFalse(cmd1.Equals(null));
            Assert.IsFalse(cmd1.Equals(123));
            Assert.IsFalse(cmd1.Equals(cmd2));
        }
    }
}
