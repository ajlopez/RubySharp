namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;

    [TestClass]
    public class DefExpressionTests
    {
        [TestMethod]
        public void DefineSimpleFunction()
        {
            Machine machine = new Machine();
            Context context = machine.RootContext;
            DefExpression cmd = new DefExpression(new NameExpression("foo"), new string[0], new CallExpression("puts", new IExpression[] { new ConstantExpression(123) }));

            var result = cmd.Evaluate(context);

            Assert.IsNull(result);

            var value = context.Self.Class.GetInstanceMethod("foo");
            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(DefinedFunction));
        }

        [TestMethod]
        public void DefineFunction()
        {
            Machine machine = new Machine();
            Context context = machine.RootContext;
            DynamicObject obj = new DynamicObject((DynamicClass)context.GetLocalValue("Object"));
            context.SetLocalValue("a", obj);
            DefExpression cmd = new DefExpression(new DotExpression(new NameExpression("a"), "foo", new IExpression[0]), new string[0], new CallExpression("puts", new IExpression[] { new ConstantExpression(123) }));

            var result = cmd.Evaluate(context);

            Assert.IsNull(result);

            var value = obj.SingletonClass.GetInstanceMethod("foo");
            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(DefinedFunction));

            Assert.IsNull(obj.Class.GetInstanceMethod("foo"));
        }

        [TestMethod]
        public void Equals()
        {
            DefExpression cmd1 = new DefExpression(new NameExpression("foo"), new string[0], new ConstantExpression(1));
            DefExpression cmd2 = new DefExpression(new NameExpression("bar"), new string[0], new ConstantExpression(1));
            DefExpression cmd3 = new DefExpression(new NameExpression("foo"), new string[0], new ConstantExpression(2));
            DefExpression cmd4 = new DefExpression(new NameExpression("foo"), new string[0], new ConstantExpression(1));

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

        [TestMethod]
        public void EqualsWithParameters()
        {
            DefExpression cmd1 = new DefExpression(new NameExpression("foo"), new string[] { "c" }, new ConstantExpression(1));
            DefExpression cmd2 = new DefExpression(new NameExpression("foo"), new string[] { "a" }, new ConstantExpression(1));
            DefExpression cmd3 = new DefExpression(new NameExpression("foo"), new string[] { "a", "b" }, new ConstantExpression(1));
            DefExpression cmd4 = new DefExpression(new NameExpression("foo"), new string[] { "c" }, new ConstantExpression(1));

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
