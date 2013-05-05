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
    public class ForInCommandTests
    {
        [TestMethod]
        public void ExecuteForIn()
        {
            Context context = new Context();
            context.SetValue("total", 0);
            ForInCommand command = new ForInCommand("k", new ConstantExpression(new int[] { 1, 2, 3 }), new AssignCommand("total", new AddExpression(new NameExpression("total"), new NameExpression("k"))));
            command.Execute(context);

            Assert.AreEqual(6, context.GetValue("total"));
        }
    }
}
