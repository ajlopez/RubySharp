namespace RubySharp.Core.Tests.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Commands;
    using RubySharp.Core.Compiler;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Language;
    using RubySharp.Core.Functions;

    [TestClass]
    public class AssignDotCommandTests
    {
        [TestMethod]
        public void CreateAssignDotCommand()
        {
            DotExpression leftvalue = (DotExpression)(new Parser("a.b")).ParseExpression();
            IExpression value = new ConstantExpression(1);
            AssignDotCommand cmd = new AssignDotCommand(leftvalue, value);

            Assert.AreSame(leftvalue, cmd.LeftValue);
            Assert.AreSame(value, cmd.Expression);
        }

        [TestMethod]
        public void ExecuteAssignDotCommand()
        {
            Machine machine = new Machine();
            var @class = new DefinedClass("Dog");
            var method = new DefinedFunction((new Parser("@name = name")).ParseCommand(), new string[] { "name" }, machine.RootContext);
            @class.SetInstanceMethod("name=", method);
            var nero = @class.CreateInstance();
            machine.RootContext.SetValue("nero", nero);
            var leftvalue = (DotExpression)(new Parser("nero.name")).ParseExpression();
            var value = new ConstantExpression("Nero");
            AssignDotCommand cmd = new AssignDotCommand(leftvalue, value);

            var result = cmd.Execute(machine.RootContext);

            Assert.IsNotNull(result);
            Assert.AreEqual("Nero", result);
            Assert.AreEqual("Nero", nero.GetValue("name"));
        }
    }
}
