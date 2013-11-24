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

            Assert.AreEqual(1, expr.Evaluate(machine.RootContext));

            var result = machine.RootContext.GetValue("Module1");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ModuleObject));

            var module = (ModuleObject)result;

            var method = module.GetMethod("name");
            Assert.IsNotNull(method);
            Assert.AreEqual("Module1", method.Apply(module, null));
        }

        [TestMethod]
        public void EvaluateModuleExpressionWithConstantAssignment()
        {
            Machine machine = new Machine();
            ModuleExpression expr = new ModuleExpression("Module1", new AssignExpression("ONE", new ConstantExpression(1)));

            Assert.AreEqual(1, expr.Evaluate(machine.RootContext));

            var result = machine.RootContext.GetValue("Module1");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ModuleObject));

            var module = (ModuleObject)result;

            Assert.AreEqual(1, module.Constants.GetLocalValue("ONE"));
        }

        [TestMethod]
        public void EvaluateModuleExpressionWithInternalAssignment()
        {
            Machine machine = new Machine();
            ModuleExpression expr = new ModuleExpression("Module1", new AssignExpression("one", new ConstantExpression(1)));

            Assert.AreEqual(1, expr.Evaluate(machine.RootContext));

            var result = machine.RootContext.GetValue("Module1");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ModuleObject));

            var module = (ModuleObject)result;

            Assert.IsFalse(module.Constants.HasLocalValue("one"));
        }

        [TestMethod]
        public void EvaluateModuleExpressionWithClassDefinition()
        {
            Machine machine = new Machine();
            ModuleExpression expr = new ModuleExpression("Module1", new ClassExpression("Foo", new ConstantExpression(1)));

            Assert.AreEqual(null, expr.Evaluate(machine.RootContext));

            var result = machine.RootContext.GetValue("Module1");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ModuleObject));

            var module = (ModuleObject)result;

            var @class = module.Constants.GetLocalValue("Foo");

            Assert.IsNotNull(@class);
            Assert.IsInstanceOfType(@class, typeof(DynamicClass));
        }

        [TestMethod]
        public void EvaluateModuleExpressionWithInternalClassDefinition()
        {
            Machine machine = new Machine();
            ModuleExpression expr = new ModuleExpression("Module1", new ClassExpression("foo", new ConstantExpression(1)));

            Assert.AreEqual(null, expr.Evaluate(machine.RootContext));

            var result = machine.RootContext.GetValue("Module1");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ModuleObject));

            var module = (ModuleObject)result;

            Assert.IsFalse(module.Constants.HasLocalValue("foo"));
        }

        [TestMethod]
        public void Equals()
        {
            ModuleExpression mexpr1 = new ModuleExpression("Module1", new ConstantExpression(1));
            ModuleExpression mexpr2 = new ModuleExpression("Module1", new ConstantExpression(2));
            ModuleExpression mexpr3 = new ModuleExpression("Module2", new ConstantExpression(1));
            ModuleExpression mexpr4 = new ModuleExpression("Module1", new ConstantExpression(1));

            Assert.IsFalse(mexpr1.Equals(null));
            Assert.IsFalse(mexpr1.Equals(123));
            Assert.IsFalse(mexpr1.Equals("foo"));

            Assert.IsTrue(mexpr1.Equals(mexpr4));
            Assert.IsTrue(mexpr4.Equals(mexpr1));
            Assert.AreEqual(mexpr1.GetHashCode(), mexpr4.GetHashCode());

            Assert.IsFalse(mexpr1.Equals(mexpr2));
            Assert.IsFalse(mexpr2.Equals(mexpr1));
            Assert.IsFalse(mexpr1.Equals(mexpr3));
            Assert.IsFalse(mexpr3.Equals(mexpr1));
        }
    }
}
