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
            DefExpression expr = new DefExpression(new NameExpression("foo"), new string[0], new CallExpression("puts", new IExpression[] { new ConstantExpression(123) }));

            var result = expr.Evaluate(context);

            Assert.IsNull(result);

            var value = context.Self.Class.GetInstanceMethod("foo");
            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(DefinedFunction));
        }

        [TestMethod]
        public void GetLocalVariables()
        {
            DefExpression expr = new DefExpression(new NameExpression("foo"), new string[0], new CallExpression("puts", new IExpression[] { new ConstantExpression(123) }));
            Assert.IsNull(expr.GetLocalVariables());
        }

        [TestMethod]
        public void DefineFunction()
        {
            Machine machine = new Machine();
            Context context = machine.RootContext;
            DynamicObject obj = new DynamicObject((DynamicClass)context.GetLocalValue("Object"));
            context.SetLocalValue("a", obj);
            DefExpression expr = new DefExpression(new DotExpression(new NameExpression("a"), "foo", new IExpression[0]), new string[0], new CallExpression("puts", new IExpression[] { new ConstantExpression(123) }));

            var result = expr.Evaluate(context);

            Assert.IsNull(result);

            var value = obj.SingletonClass.GetInstanceMethod("foo");
            Assert.IsNotNull(value);
            Assert.IsInstanceOfType(value, typeof(DefinedFunction));

            Assert.IsNull(obj.Class.GetInstanceMethod("foo"));
        }

        [TestMethod]
        public void Equals()
        {
            DefExpression expr1 = new DefExpression(new NameExpression("foo"), new string[0], new ConstantExpression(1));
            DefExpression expr2 = new DefExpression(new NameExpression("bar"), new string[0], new ConstantExpression(1));
            DefExpression expr3 = new DefExpression(new NameExpression("foo"), new string[0], new ConstantExpression(2));
            DefExpression expr4 = new DefExpression(new NameExpression("foo"), new string[0], new ConstantExpression(1));

            Assert.IsTrue(expr1.Equals(expr4));
            Assert.IsTrue(expr4.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr4.GetHashCode());

            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals(123));

            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr2.Equals(expr1));
            Assert.IsFalse(expr1.Equals(expr3));
            Assert.IsFalse(expr3.Equals(expr1));
        }

        [TestMethod]
        public void EqualsWithParameters()
        {
            DefExpression expr1 = new DefExpression(new NameExpression("foo"), new string[] { "c" }, new ConstantExpression(1));
            DefExpression expr2 = new DefExpression(new NameExpression("foo"), new string[] { "a" }, new ConstantExpression(1));
            DefExpression expr3 = new DefExpression(new NameExpression("foo"), new string[] { "a", "b" }, new ConstantExpression(1));
            DefExpression expr4 = new DefExpression(new NameExpression("foo"), new string[] { "c" }, new ConstantExpression(1));

            Assert.IsTrue(expr1.Equals(expr4));
            Assert.IsTrue(expr4.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr4.GetHashCode());

            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals(123));

            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr2.Equals(expr1));
            Assert.IsFalse(expr1.Equals(expr3));
            Assert.IsFalse(expr3.Equals(expr1));
        }
    }
}
