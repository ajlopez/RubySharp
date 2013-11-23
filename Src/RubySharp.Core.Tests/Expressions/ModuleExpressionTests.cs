namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Language;

    [TestClass]
    public class ModuleExpressionTests
    {
        [TestMethod]
        public void EvaluateModuleExpression()
        {
            Machine machine = new Machine();
            ModuleExpression expr = new ModuleExpression("Module1", new ConstantExpression(1));

            Assert.AreEqual(null, expr.Evaluate(machine.RootContext));

            var result = machine.RootContext.GetValue("Module1");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ModuleObject));

            var module = (ModuleObject)result;

            var method = module.GetMethod("name");
            Assert.IsNotNull(method);
            Assert.AreEqual("Module1", method.Apply(module, null));

            Assert.AreEqual("Module1", module.GetValue("name"));
        }
    }
}
