namespace RubySharp.Core.Tests.Functions
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Commands;
    using RubySharp.Core.Expressions;

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
