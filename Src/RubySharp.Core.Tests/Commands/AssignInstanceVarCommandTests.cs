namespace RubySharp.Core.Tests.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Commands;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Language;

    [TestClass]
    public class AssignInstanceVarCommandTests
    {
        [TestMethod]
        public void AssignValue()
        {
            AssignInstanceVarCommand cmd = new AssignInstanceVarCommand("one", new ConstantExpression(1));
            BaseObject obj = new BaseObject(null);
            Context context = new Context(null, obj, null);

            var result = cmd.Execute(context);

            Assert.AreEqual(1, result);
            Assert.AreEqual(1, obj.GetValue("one"));
        }

        [TestMethod]
        public void Equals()
        {
            AssignInstanceVarCommand cmd1 = new AssignInstanceVarCommand("a", new ConstantExpression(1));
            AssignInstanceVarCommand cmd2 = new AssignInstanceVarCommand("a", new ConstantExpression(2));
            AssignInstanceVarCommand cmd3 = new AssignInstanceVarCommand("b", new ConstantExpression(1));
            AssignInstanceVarCommand cmd4 = new AssignInstanceVarCommand("a", new ConstantExpression(1));

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
