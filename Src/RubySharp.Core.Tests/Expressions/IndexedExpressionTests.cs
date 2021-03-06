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
    public class IndexedExpressionTests
    {
        [TestMethod]
        public void GetIndexedValue()
        {
            Machine machine = new Machine();
            machine.ExecuteText("a = [1,2,3]");

            IndexedExpression expression = new IndexedExpression(new NameExpression("a"), new ConstantExpression(1));

            var result = expression.Evaluate(machine.RootContext);

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GetIndexedNegativeValue()
        {
            Machine machine = new Machine();
            machine.ExecuteText("a = [1,2,3]");

            IndexedExpression expression = new IndexedExpression(new NameExpression("a"), new ConstantExpression(-1));

            var result = expression.Evaluate(machine.RootContext);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void GetIndexedOutOfBandNegativeValue()
        {
            Machine machine = new Machine();
            machine.ExecuteText("a = [1,2,3]");

            IndexedExpression expression = new IndexedExpression(new NameExpression("a"), new ConstantExpression(-10));

            var result = expression.Evaluate(machine.RootContext);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetIndexedOutOfBandValue()
        {
            Machine machine = new Machine();
            machine.ExecuteText("a = [1,2,3]");

            IndexedExpression expression = new IndexedExpression(new NameExpression("a"), new ConstantExpression(10));

            var result = expression.Evaluate(machine.RootContext);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetIndexedString()
        {
            Machine machine = new Machine();

            IndexedExpression expression = new IndexedExpression(new ConstantExpression("foo"), new ConstantExpression(0));

            var result = expression.Evaluate(machine.RootContext);

            Assert.IsNotNull(result);
            Assert.AreEqual("f", result);
        }

        [TestMethod]
        public void GetIndexedStringOutOfBound()
        {
            Machine machine = new Machine();

            IndexedExpression expression = new IndexedExpression(new ConstantExpression("foo"), new ConstantExpression(10));

            var result = expression.Evaluate(machine.RootContext);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetIndexedStringNegativeIndex()
        {
            Machine machine = new Machine();

            IndexedExpression expression = new IndexedExpression(new ConstantExpression("bar"), new ConstantExpression(-1));

            var result = expression.Evaluate(machine.RootContext);

            Assert.IsNotNull(result);
            Assert.AreEqual("r", result);
        }

        [TestMethod]
        public void GetIndexedStringNegativeIndexAsNil()
        {
            Machine machine = new Machine();

            IndexedExpression expression = new IndexedExpression(new ConstantExpression("bar"), new ConstantExpression(-10));

            var result = expression.Evaluate(machine.RootContext);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetIndexedDictionaryEntry()
        {
            Machine machine = new Machine();
            var hash = new DynamicHash();
            hash[new Symbol("one")] = 1;
            hash[new Symbol("two")] = 2;

            IndexedExpression expression = new IndexedExpression(new ConstantExpression(hash), new ConstantExpression(new Symbol("one")));

            var result = expression.Evaluate(machine.RootContext);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void GetIndexedDictionaryEntryAsNil()
        {
            Machine machine = new Machine();
            var hash = new DynamicHash();
            hash[new Symbol("one")] = 1;
            hash[new Symbol("two")] = 2;

            IndexedExpression expression = new IndexedExpression(new ConstantExpression(hash), new ConstantExpression(new Symbol("three")));

            var result = expression.Evaluate(machine.RootContext);

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Equals()
        {
            IndexedExpression expr1 = new IndexedExpression(new NameExpression("a"), new ConstantExpression(1));
            IndexedExpression expr2 = new IndexedExpression(new NameExpression("a"), new ConstantExpression(2));
            IndexedExpression expr3 = new IndexedExpression(new NameExpression("b"), new ConstantExpression(1));
            IndexedExpression expr4 = new IndexedExpression(new NameExpression("a"), new ConstantExpression(1));

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
