namespace RubySharp.Core.Tests.Commands
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Commands;

    [TestClass]
    public class AssignCommandTests
    {
        [TestMethod]
        public void AssignValue()
        {
            AssignCommand cmd = new AssignCommand("one", new ConstantExpression(1));
            Context context = new Context();

            cmd.Execute(context);

            Assert.AreEqual(1, context.GetValue("one"));
        }
    }
}
