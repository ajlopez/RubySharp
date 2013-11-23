namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Compiler;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class WhileExpressionTests
    {
        [TestMethod]
        public void ExecuteSimpleWhileWhenConditionIsTrue()
        {
            Parser cmdparser = new Parser("a = a + 1");
            IExpression body = cmdparser.ParseCommand();
            Parser exprparser = new Parser("a < 6");
            IExpression expr = exprparser.ParseExpression();

            Context context = new Context();
            context.SetValue("a", 1);

            WhileExpression cmd = new WhileExpression(expr, body);

            Assert.IsNull(cmd.Evaluate(context));

            Assert.AreEqual(6, context.GetValue("a"));
        }

        [TestMethod]
        public void Equals()
        {
            WhileExpression cmd1 = new WhileExpression(new ConstantExpression(1), new AssignExpression("one", new ConstantExpression(1)));
            WhileExpression cmd2 = new WhileExpression(new ConstantExpression(2), new AssignExpression("one", new ConstantExpression(1)));
            WhileExpression cmd3 = new WhileExpression(new ConstantExpression(1), new AssignExpression("one", new ConstantExpression(2)));
            WhileExpression cmd4 = new WhileExpression(new ConstantExpression(1), new AssignExpression("one", new ConstantExpression(1)));

            Assert.IsTrue(cmd1.Equals(cmd4));
            Assert.IsTrue(cmd4.Equals(cmd1));
            Assert.AreEqual(cmd1.GetHashCode(), cmd4.GetHashCode());

            Assert.IsFalse(cmd1.Equals(null));
            Assert.IsFalse(cmd1.Equals(123));
            Assert.IsFalse(cmd1.Equals(cmd2));
            Assert.IsFalse(cmd1.Equals(cmd3));
        }
    }
}
