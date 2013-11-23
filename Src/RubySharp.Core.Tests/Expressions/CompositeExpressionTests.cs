namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class CompositeExpressionTests
    {
        [TestMethod]
        public void ExecuteTwoAssignCommands()
        {
            Context context = new Context();
            AssignExpression cmd1 = new AssignExpression("one", new ConstantExpression(1));
            AssignExpression cmd2 = new AssignExpression("two", new ConstantExpression(2));
            CompositeExpression cmd = new CompositeExpression(new IExpression[] { cmd1, cmd2 });

            var result = cmd.Evaluate(context);

            Assert.AreEqual(2, result);
            Assert.AreEqual(1, context.GetValue("one"));
            Assert.AreEqual(2, context.GetValue("two"));
        }

        [TestMethod]
        public void Equals()
        {
            AssignExpression acmd1 = new AssignExpression("one", new ConstantExpression(1));
            AssignExpression acmd2 = new AssignExpression("two", new ConstantExpression(2));

            CompositeExpression cmd1 = new CompositeExpression(new IExpression[] { acmd1, acmd2 });
            CompositeExpression cmd2 = new CompositeExpression(new IExpression[] { acmd2, acmd1 });
            CompositeExpression cmd3 = new CompositeExpression(new IExpression[] { acmd1 });
            CompositeExpression cmd4 = new CompositeExpression(new IExpression[] { acmd1, acmd2 });

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
