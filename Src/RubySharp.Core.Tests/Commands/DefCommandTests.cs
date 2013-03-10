namespace RubySharp.Core.Tests.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Commands;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Functions;

    [TestClass]
    public class DefCommandTests
    {
        [TestMethod]
        public void DefineSimpleFunction()
        {
            Context context = new Context();
            DefCommand cmd = new DefCommand("foo", new ExpressionCommand(new CallExpression("puts", new IExpression[] { new ConstantExpression(123) })));

            var result = cmd.Execute(context);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(DefinedFunction));
            Assert.AreEqual(result, context.GetValue("foo"));
        }

        [TestMethod]
        public void Equals()
        {
            DefCommand cmd1 = new DefCommand("foo", new ExpressionCommand(new ConstantExpression(1)));
            DefCommand cmd2 = new DefCommand("bar", new ExpressionCommand(new ConstantExpression(1)));
            DefCommand cmd3 = new DefCommand("foo", new ExpressionCommand(new ConstantExpression(2)));
            DefCommand cmd4 = new DefCommand("foo", new ExpressionCommand(new ConstantExpression(1)));

            Assert.IsTrue(cmd1.Equals(cmd4));
            Assert.IsTrue(cmd4.Equals(cmd1));
            Assert.AreEqual(cmd1.GetHashCode(), cmd4.GetHashCode());

            Assert.IsFalse(cmd1.Equals(null));
            Assert.IsFalse(cmd1.Equals(123));

            Assert.IsFalse(cmd1.Equals(cmd2));
            Assert.IsFalse(cmd2.Equals(cmd1));
            Assert.IsFalse(cmd1.Equals(cmd3));
            Assert.IsFalse(cmd3.Equals(cmd1));
        }
    }
}
