namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Language;

    [TestClass]
    public class HashExpressionTests
    {
        [TestMethod]
        public void Evaluate()
        {
            IList<IExpression> keyexprs = new IExpression[] { new ConstantExpression(1), new ConstantExpression(3) };
            IList<IExpression> valueexprs = new IExpression[] { new ConstantExpression("one"), new ConstantExpression("three") };

            HashExpression expr = new HashExpression(keyexprs, valueexprs);

            var result = expr.Evaluate(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IDictionary));

            var dict = (IDictionary)result;

            Assert.AreEqual("one", dict[1]);
            Assert.AreEqual("three", dict[3]);
        }

        [TestMethod]
        public void EvaluateWithSymbolKeys()
        {
            IList<IExpression> keyexprs = new IExpression[] { new ConstantExpression(new Symbol("one")), new ConstantExpression(new Symbol("three")) };
            IList<IExpression> valueexprs = new IExpression[] { new ConstantExpression("one"), new ConstantExpression("three") };

            HashExpression expr = new HashExpression(keyexprs, valueexprs);

            var result = expr.Evaluate(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IDictionary));

            var dict = (IDictionary)result;

            Assert.AreEqual("one", dict[new Symbol("one")]);
            Assert.AreEqual("three", dict[new Symbol("three")]);
        }

        [TestMethod]
        public void Equals()
        {
            IList<IExpression> keyexprs1 = new IExpression[] { new ConstantExpression(new Symbol("one")), new ConstantExpression(new Symbol("three")) };
            IList<IExpression> valueexprs1 = new IExpression[] { new ConstantExpression("one"), new ConstantExpression("three") };
            IList<IExpression> keyexprs2 = new IExpression[] { new ConstantExpression(new Symbol("one")), new ConstantExpression(new Symbol("two")) };
            IList<IExpression> valueexprs2 = new IExpression[] { new ConstantExpression("one"), new ConstantExpression("two") };

            HashExpression expr1 = new HashExpression(keyexprs1, valueexprs1);
            HashExpression expr2 = new HashExpression(keyexprs1, valueexprs2);
            HashExpression expr3 = new HashExpression(keyexprs2, valueexprs1);
            HashExpression expr4 = new HashExpression(keyexprs2, valueexprs2);
            HashExpression expr5 = new HashExpression(keyexprs1, valueexprs1);

            Assert.IsFalse(expr1.Equals(null));
            Assert.IsFalse(expr1.Equals("foo"));
            Assert.IsFalse(expr1.Equals(123));

            Assert.IsTrue(expr1.Equals(expr1));
            Assert.IsTrue(expr1.Equals(expr5));
            Assert.IsTrue(expr5.Equals(expr1));

            Assert.AreEqual(expr1.GetHashCode(), expr5.GetHashCode());

            Assert.IsFalse(expr1.Equals(expr2));
            Assert.IsFalse(expr2.Equals(expr1));
            Assert.IsFalse(expr1.Equals(expr3));
            Assert.IsFalse(expr3.Equals(expr1));
            Assert.IsFalse(expr1.Equals(expr4));
            Assert.IsFalse(expr4.Equals(expr1));
        }
    }
}
