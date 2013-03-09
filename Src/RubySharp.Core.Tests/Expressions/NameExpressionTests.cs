namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class NameExpressionTests
    {
        [TestMethod]
        public void EvaluateUndefinedName()
        {
            NameExpression expr = new NameExpression("foo");
            Context context = new Context();

            Assert.IsNull(expr.Evaluate(context));
        }

        [TestMethod]
        public void EvaluateDefinedName()
        {
            NameExpression expr = new NameExpression("one");
            Context context = new Context();
            context.SetValue("one", 1);

            Assert.AreEqual(1, expr.Evaluate(context));
        }
    }
}
