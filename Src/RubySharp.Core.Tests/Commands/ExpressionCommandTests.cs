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
    }
}
