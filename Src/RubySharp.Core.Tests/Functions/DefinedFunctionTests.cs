namespace RubySharp.Core.Tests.Functions
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
    public class DefinedFunctionTests
    {
        [TestMethod]
        public void DefineAndExecuteSimplePuts()
        {
            Machine machine = new Machine();
            StringWriter writer = new StringWriter();
            PutsFunction puts = new PutsFunction(writer);
            machine.RootContext.Self.Class.SetInstanceMethod("puts", puts);

            DefinedFunction function = new DefinedFunction(new CallExpression("puts", new IExpression[] { new ConstantExpression(123) }), new string[] { }, machine.RootContext);

            Assert.IsNull(function.Apply(machine.RootContext.Self, machine.RootContext, new object[] { }));
            Assert.AreEqual("123\r\n", writer.ToString());
        }

        [TestMethod]
        public void DefineAndExecuteFunctionWithParameters()
        {
            Context context = new Context();

            DefinedFunction function = new DefinedFunction(new AddExpression(new NameExpression("a"), new NameExpression("b")), new string[] { "a", "b" }, context);

            var result = function.Apply(null, null, new object[] { 1, 2 });

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }
    }
}
