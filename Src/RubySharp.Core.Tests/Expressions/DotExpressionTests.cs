﻿namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Exceptions;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Language;

    [TestClass]
    public class DotExpressionTests
    {
        [TestMethod]
        public void EvaluateInstanceMethod()
        {
            Machine machine = new Machine();
            machine.ExecuteText("class MyClass;def foo;3;end;end");

            var dclass = (DynamicClass)machine.RootContext.GetValue("MyClass");

            var myobj = dclass.CreateInstance();

            DotExpression expression = new DotExpression(new ConstantExpression(myobj), "foo", new IExpression[0]);

            var result = expression.Evaluate(null);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void EvaluateUndefinedInstanceMethod()
        {
            Machine machine = new Machine();
            machine.ExecuteText("class MyClass;end");

            var dclass = (DynamicClass)machine.RootContext.GetValue("MyClass");

            var myobj = dclass.CreateInstance();

            DotExpression expression = new DotExpression(new ConstantExpression(myobj), "foo", new IExpression[0]);

            try
            {
                expression.Evaluate(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(NoMethodError));
                Assert.AreEqual("undefined method 'foo'", ex.Message);
            }
        }

        [TestMethod]
        public void EvaluateNativeObjectClassMethod()
        {
            Machine machine = new Machine();

            DotExpression expression = new DotExpression(new ConstantExpression(1), "class", null);

            var result = expression.Evaluate(machine.RootContext);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FixnumClass));
        }

        [TestMethod]
        public void NamedExpression()
        {
            INamedExpression expression = new DotExpression(new ConstantExpression(1), "class", null);

            Assert.AreEqual(new ConstantExpression(1), expression.TargetExpression);
            Assert.AreEqual("class", expression.Name);
        }

        [TestMethod]
        public void Equals()
        {
            DotExpression expr1 = new DotExpression(new ConstantExpression(1), "foo", new IExpression[0]);
            DotExpression expr2 = new DotExpression(new ConstantExpression(2), "foo", new IExpression[0]);
            DotExpression expr3 = new DotExpression(new ConstantExpression(1), "bar", new IExpression[0]);
            DotExpression expr4 = new DotExpression(new ConstantExpression(1), "foo", new IExpression[0]);

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

        [TestMethod]
        public void EqualsWithArguments()
        {
            DotExpression expr1 = new DotExpression(new ConstantExpression(1), "foo", new IExpression[] { new ConstantExpression(2) });
            DotExpression expr2 = new DotExpression(new ConstantExpression(2), "foo", new IExpression[] { new ConstantExpression(1) });
            DotExpression expr3 = new DotExpression(new ConstantExpression(1), "bar", new IExpression[] { new ConstantExpression(1), new ConstantExpression(2) });
            DotExpression expr4 = new DotExpression(new ConstantExpression(1), "foo", new IExpression[] { new ConstantExpression(2) });

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
