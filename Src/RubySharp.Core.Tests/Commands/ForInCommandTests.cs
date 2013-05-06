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
    public class ForInCommandTests
    {
        [TestMethod]
        public void ExecuteForIn()
        {
            Context context = new Context();
            context.SetValue("total", 0);
            ForInCommand command = new ForInCommand("k", new ConstantExpression(new int[] { 1, 2, 3 }), new AssignCommand("total", new AddExpression(new NameExpression("total"), new NameExpression("k"))));
            command.Execute(context);

            Assert.AreEqual(6, context.GetValue("total"));
        }

        [TestMethod]
        public void Equals()
        {
            ForInCommand cmd1 = new ForInCommand("k", new ConstantExpression(1), new ExpressionCommand(new ConstantExpression(2)));
            ForInCommand cmd2 = new ForInCommand("j", new ConstantExpression(1), new ExpressionCommand(new ConstantExpression(2)));
            ForInCommand cmd3 = new ForInCommand("k", new ConstantExpression(2), new ExpressionCommand(new ConstantExpression(2)));
            ForInCommand cmd4 = new ForInCommand("k", new ConstantExpression(1), new ExpressionCommand(new ConstantExpression(3)));
            ForInCommand cmd5 = new ForInCommand("k", new ConstantExpression(1), new ExpressionCommand(new ConstantExpression(2)));

            Assert.IsTrue(cmd1.Equals(cmd5));
            Assert.IsTrue(cmd5.Equals(cmd1));
            Assert.AreEqual(cmd1.GetHashCode(), cmd5.GetHashCode());

            Assert.IsFalse(cmd1.Equals(null));
            Assert.IsFalse(cmd1.Equals(123));
            Assert.IsFalse(cmd1.Equals(cmd2));
            Assert.IsFalse(cmd2.Equals(cmd1));
            Assert.IsFalse(cmd1.Equals(cmd3));
            Assert.IsFalse(cmd3.Equals(cmd1));
            Assert.IsFalse(cmd1.Equals(cmd4));
            Assert.IsFalse(cmd4.Equals(cmd1));
        }
    }
}
