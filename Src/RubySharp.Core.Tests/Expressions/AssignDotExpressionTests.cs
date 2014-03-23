namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Compiler;
    using RubySharp.Core.Exceptions;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;
    using RubySharp.Core.Tests.Classes;

    [TestClass]
    public class AssignDotExpressionTests
    {
        [TestMethod]
        public void CreateAssignDotCommand()
        {
            DotExpression leftvalue = (DotExpression)(new Parser("a.b")).ParseExpression();
            IExpression value = new ConstantExpression(1);
            AssignDotExpressions cmd = new AssignDotExpressions(leftvalue, value);

            Assert.AreSame(leftvalue, cmd.LeftValue);
            Assert.AreSame(value, cmd.Expression);
        }

        [TestMethod]
        public void ExecuteAssignDotCommand()
        {
            Machine machine = new Machine();
            var @class = new DynamicClass("Dog");
            var method = new DefinedFunction((new Parser("@name = name")).ParseCommand(), new string[] { "name" }, machine.RootContext);
            @class.SetInstanceMethod("name=", method);
            var nero = @class.CreateInstance();
            machine.RootContext.SetLocalValue("nero", nero);
            var leftvalue = (DotExpression)(new Parser("nero.name")).ParseExpression();
            var value = new ConstantExpression("Nero");
            AssignDotExpressions cmd = new AssignDotExpressions(leftvalue, value);

            var result = cmd.Evaluate(machine.RootContext);

            Assert.IsNotNull(result);
            Assert.AreEqual("Nero", result);
            Assert.AreEqual("Nero", nero.GetValue("name"));
        }

        [TestMethod]
        public void ExecuteAssignDotCommandWithUnknownMethod()
        {
            Machine machine = new Machine();
            var @class = new DynamicClass("Dog");
            var nero = @class.CreateInstance();
            machine.RootContext.SetLocalValue("nero", nero);
            var leftvalue = (DotExpression)(new Parser("nero.name")).ParseExpression();
            var value = new ConstantExpression("Nero");
            AssignDotExpressions cmd = new AssignDotExpressions(leftvalue, value);

            try
            {
                cmd.Evaluate(machine.RootContext);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(NoMethodError));
            }
        }

        [TestMethod]
        public void ExecuteAssignDotCommandOnNativeProperty()
        {
            Person person = new Person();
            Machine machine = new Machine();
            machine.RootContext.SetLocalValue("p", person);
            var leftvalue = (DotExpression)(new Parser("p.FirstName")).ParseExpression();
            var value = new ConstantExpression("Adam");
            AssignDotExpressions cmd = new AssignDotExpressions(leftvalue, value);

            var result = cmd.Evaluate(machine.RootContext);

            Assert.IsNotNull(result);
            Assert.AreEqual("Adam", result);
            Assert.AreEqual("Adam", person.FirstName);
        }

        [TestMethod]
        public void Equals()
        {
            DotExpression expr1 = (DotExpression)(new Parser("a.b")).ParseExpression();
            DotExpression expr2 = (DotExpression)(new Parser("a.c")).ParseExpression();

            AssignDotExpressions cmd1 = new AssignDotExpressions(expr1, new ConstantExpression(1));
            AssignDotExpressions cmd2 = new AssignDotExpressions(expr1, new ConstantExpression(2));
            AssignDotExpressions cmd3 = new AssignDotExpressions(expr2, new ConstantExpression(1));
            AssignDotExpressions cmd4 = new AssignDotExpressions(expr1, new ConstantExpression(1));

            Assert.IsTrue(cmd1.Equals(cmd4));
            Assert.IsTrue(cmd4.Equals(cmd1));
            Assert.AreEqual(cmd1.GetHashCode(), cmd4.GetHashCode());

            Assert.IsFalse(cmd1.Equals(null));
            Assert.IsFalse(cmd1.Equals(cmd2));
            Assert.IsFalse(cmd1.Equals(cmd3));
            Assert.IsFalse(cmd1.Equals(123));
        }
    }
}
