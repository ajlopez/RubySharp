namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Expressions;

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
