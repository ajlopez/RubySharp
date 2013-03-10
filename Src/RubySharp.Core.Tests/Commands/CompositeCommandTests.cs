namespace RubySharp.Core.Tests.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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

        [TestMethod]
        public void Equals()
        {
            AssignCommand acmd1 = new AssignCommand("one", new ConstantExpression(1));
            AssignCommand acmd2 = new AssignCommand("two", new ConstantExpression(2));

            CompositeCommand cmd1 = new CompositeCommand(new ICommand[] { acmd1, acmd2 });
            CompositeCommand cmd2 = new CompositeCommand(new ICommand[] { acmd2, acmd1 });
            CompositeCommand cmd3 = new CompositeCommand(new ICommand[] { acmd1 });
            CompositeCommand cmd4 = new CompositeCommand(new ICommand[] { acmd1, acmd2 });

            Assert.IsTrue(cmd1.Equals(cmd4));
            Assert.IsTrue(cmd4.Equals(cmd1));
            Assert.AreEqual(cmd1.GetHashCode(), cmd4.GetHashCode());

            Assert.IsFalse(cmd1.Equals(null));
            Assert.IsFalse(cmd1.Equals(123));
            Assert.IsFalse(cmd1.Equals(cmd2));
            Assert.IsFalse(cmd2.Equals(cmd1));
            Assert.IsFalse(cmd1.Equals(cmd3));
            Assert.IsFalse(cmd3.Equals(cmd1));
        }
    }
}
