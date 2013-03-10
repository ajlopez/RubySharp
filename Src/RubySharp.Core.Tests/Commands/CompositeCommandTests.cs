namespace RubySharp.Core.Tests.Commands
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Commands;
    using RubySharp.Core.Expressions;

    [TestClass]
    public class CompositeCommandTests
    {
        [TestMethod]
        public void ExecuteTwoAssignCommands()
        {
            Context context = new Context();
            AssignCommand cmd1 = new AssignCommand("one", new ConstantExpression(1));
            AssignCommand cmd2 = new AssignCommand("two", new ConstantExpression(2));
            CompositeCommand cmd = new CompositeCommand(new ICommand[] { cmd1, cmd2 });

            var result = cmd.Execute(context);

            Assert.AreEqual(2, result);
            Assert.AreEqual(1, context.GetValue("one"));
            Assert.AreEqual(2, context.GetValue("two"));
        }
    }
}
