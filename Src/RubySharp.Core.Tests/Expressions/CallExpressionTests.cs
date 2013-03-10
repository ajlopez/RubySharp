namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Functions;

    [TestClass]
    public class CallExpressionTests
    {
        [TestMethod]
        public void CallPutsInteger()
        {
            StringWriter writer = new StringWriter();
            CallExpression expr = new CallExpression("puts", new IExpression[] { new ConstantExpression(123) });
            Machine machine = new Machine();
            PutsFunction puts = new PutsFunction(writer);
            machine.RootContext.SetValue("puts", puts);

            Assert.IsNull(expr.Evaluate(machine.RootContext));
            Assert.AreEqual("123\r\n", writer.ToString());
        }
    }
}
