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
            Context context = new Context();
            ModuleExpression expr = new ModuleExpression("Module1", new ConstantExpression(1));

            Assert.AreEqual(null, expr.Evaluate(context));

            var result = context.GetValue("Module1");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Module));

            var module = (Module)result;

            Assert.AreEqual("Module1", module.Name);
        }
    }
}
