namespace RubySharp.Core.Tests.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Commands;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;

    [TestClass]
    public class ClassCommandTests
    {
        [TestMethod]
        public void DefineSimpleClass()
        {
            Context context = new Context();
            ClassCommand cmd = new ClassCommand("Dog", new ExpressionCommand(new CallExpression("puts", new IExpression[] { new ConstantExpression(123) })));

            var result = cmd.Execute(context);

            Assert.IsNull(result);

            var value = context.GetValue("Dog");
            Assert.IsInstanceOfType(value, typeof(DefinedClass));
            Assert.AreEqual(value, context.GetValue("Dog"));
        }

        [TestMethod]
        public void RedefineSimpleClass()
        {
            Context context = new Context();
            ClassCommand cmd = new ClassCommand("Dog", new ExpressionCommand(new CallExpression("puts", new IExpression[] { new ConstantExpression(123) })));

            cmd.Execute(context);

            var initial = context.GetValue("Dog");

            var result = cmd.Execute(context);

            Assert.IsNull(result);

            var value = context.GetValue("Dog");
            Assert.IsInstanceOfType(value, typeof(DefinedClass));
            Assert.AreEqual(value, context.GetValue("Dog"));
            Assert.AreSame(initial, value);
        }

        [TestMethod]
        public void Equals()
        {
            ClassCommand cmd1 = new ClassCommand("foo", new ExpressionCommand(new ConstantExpression(1)));
            ClassCommand cmd2 = new ClassCommand("bar", new ExpressionCommand(new ConstantExpression(1)));
            ClassCommand cmd3 = new ClassCommand("foo", new ExpressionCommand(new ConstantExpression(2)));
            ClassCommand cmd4 = new ClassCommand("foo", new ExpressionCommand(new ConstantExpression(1)));

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
