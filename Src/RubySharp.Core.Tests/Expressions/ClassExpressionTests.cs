namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;

    [TestClass]
    public class ClassExpressionTests
    {
        [TestMethod]
        public void DefineSimpleClass()
        {
            Context context = new Context();
            StringWriter writer = new StringWriter();
            context.SetValue("puts", new PutsFunction(writer));
            ClassExpression cmd = new ClassExpression("Dog", new CallExpression("puts", new IExpression[] { new ConstantExpression(123) }));

            var result = cmd.Evaluate(context);

            Assert.IsNull(result);

            var value = context.GetValue("Dog");
            Assert.IsInstanceOfType(value, typeof(DefinedClass));
            Assert.AreEqual(value, context.GetValue("Dog"));
            Assert.AreEqual("123\r\n", writer.ToString());
        }

        [TestMethod]
        public void RedefineSimpleClass()
        {
            Context context = new Context();
            StringWriter writer = new StringWriter();
            context.SetValue("puts", new PutsFunction(writer));
            ClassExpression cmd = new ClassExpression("Dog", new CallExpression("puts", new IExpression[] { new ConstantExpression(123) }));

            cmd.Evaluate(context);

            var initial = context.GetValue("Dog");

            var result = cmd.Evaluate(context);

            Assert.IsNull(result);

            var value = context.GetValue("Dog");
            Assert.IsInstanceOfType(value, typeof(DefinedClass));
            Assert.AreEqual(value, context.GetValue("Dog"));
            Assert.AreSame(initial, value);
            Assert.AreEqual("123\r\n123\r\n", writer.ToString());
        }

        [TestMethod]
        public void Equals()
        {
            ClassExpression cmd1 = new ClassExpression("foo", new ConstantExpression(1));
            ClassExpression cmd2 = new ClassExpression("bar", new ConstantExpression(1));
            ClassExpression cmd3 = new ClassExpression("foo", new ConstantExpression(2));
            ClassExpression cmd4 = new ClassExpression("foo", new ConstantExpression(1));

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
