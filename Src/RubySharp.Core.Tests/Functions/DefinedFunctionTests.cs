namespace RubySharp.Core.Tests.Functions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Commands;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Functions;

    [TestClass]
    public class DefinedFunctionTests
    {
        [TestMethod]
        public void DefineAndExecuteSimplePuts()
        {
            Machine machine = new Machine();
            StringWriter writer = new StringWriter();
            PutsFunction puts = new PutsFunction(writer);
            machine.RootContext.SetValue("puts", puts);

            DefinedFunction function = new DefinedFunction(new ExpressionCommand(new CallExpression("puts", new IExpression[] { new ConstantExpression(123) })), machine.RootContext);

            Assert.IsNull(function.Apply(null));
            Assert.AreEqual("123\r\n", writer.ToString());
        }
    }
}
