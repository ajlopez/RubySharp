namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Language;

    [TestClass]
    public class DotExpressionTests
    {
        [TestMethod]
        public void EvaluateObjectMethod()
        {
            Machine machine = new Machine();
            machine.ExecuteText("class MyClass;def foo;3;end;end");

            var dclass = (DefinedClass)machine.RootContext.GetValue("MyClass");

            var myobj = dclass.CreateInstance();

            DotExpression expression = new DotExpression(new ConstantExpression(myobj), "foo");

            var result = expression.Evaluate(null);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void Equals()
        {
            DotExpression expr1 = new DotExpression(new ConstantExpression(1), "foo");
            DotExpression expr2 = new DotExpression(new ConstantExpression(2), "foo");
            DotExpression expr3 = new DotExpression(new ConstantExpression(1), "bar");
            DotExpression expr4 = new DotExpression(new ConstantExpression(1), "foo");

            Assert.IsTrue(expr1.Equals(expr4));
            Assert.IsTrue(expr4.Equals(expr1));
            Assert.AreEqual(expr1.GetHashCode(), expr4.GetHashCode());

            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr2.Equals(expr1));
            Assert.IsFalse(expr1.Equals(expr3));
            Assert.IsFalse(expr3.Equals(expr1));
            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals("foo"));
        }
    }
}
