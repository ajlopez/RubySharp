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
    public class AssignCommandTests
    {
        [TestMethod]
        public void AssignValue()
        {
            AssignCommand cmd = new AssignCommand("one", new ConstantExpression(1));
            Context context = new Context();

            var result = cmd.Execute(context);

            Assert.AreEqual(1, result);
            Assert.AreEqual(1, context.GetValue("one"));
        }
    }
}
