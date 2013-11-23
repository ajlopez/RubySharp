namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class ForInExpressionTests
    {
        [TestMethod]
        public void ExecuteForIn()
        {
            Context context = new Context();
            context.SetValue("total", 0);
            ForInExpression command = new ForInExpression("k", new ConstantExpression(new int[] { 1, 2, 3 }), new AssignExpression("total", new AddExpression(new NameExpression("total"), new NameExpression("k"))));
            command.Evaluate(context);

            Assert.AreEqual(6, context.GetValue("total"));
        }

        [TestMethod]
        public void Equals()
        {
            ForInExpression cmd1 = new ForInExpression("k", new ConstantExpression(1), new ConstantExpression(2));
            ForInExpression cmd2 = new ForInExpression("j", new ConstantExpression(1), new ConstantExpression(2));
            ForInExpression cmd3 = new ForInExpression("k", new ConstantExpression(2), new ConstantExpression(2));
            ForInExpression cmd4 = new ForInExpression("k", new ConstantExpression(1), new ConstantExpression(3));
            ForInExpression cmd5 = new ForInExpression("k", new ConstantExpression(1), new ConstantExpression(2));

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
