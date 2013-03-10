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
    }
}
