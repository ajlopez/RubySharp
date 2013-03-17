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

            DefinedFunction function = new DefinedFunction(new ExpressionCommand(new CallExpression("puts", new IExpression[] { new ConstantExpression(123) })), new string[] { }, machine.RootContext);

            Assert.IsNull(function.Apply(null, new object[] { }));
            Assert.AreEqual("123\r\n", writer.ToString());
        }

        [TestMethod]
        public void DefineAndExecuteFunctionWithParameters()
        {
            Context context = new Context();

            DefinedFunction function = new DefinedFunction(new ExpressionCommand(new AddExpression(new NameExpression("a"), new NameExpression("b"))), new string[] { "a", "b" }, context);

            var result = function.Apply(null, new object[] { 1, 2 });

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }
    }
}
