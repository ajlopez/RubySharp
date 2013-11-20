namespace RubySharp.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Compiler;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Language;

    [TestClass]
    public class EvaluateTests
    {
        private Machine machine;

        [TestInitialize]
        public void Setup()
        {
            this.machine = new Machine();
        }

        [TestMethod]
        public void EvaluateSimpleArithmetic()
        {
            Assert.AreEqual(2, this.EvaluateExpression("1+1"));
            Assert.AreEqual(1 / 2, this.EvaluateExpression("1/2"));
            Assert.AreEqual(1 + 3 * 2, this.EvaluateExpression("1 + 3 * 2"));
        }

        [TestMethod]
        public void EvaluateSimpleAssignment()
        {
            Assert.AreEqual(2, this.Execute("a = 2"));
            Assert.AreEqual(2, this.EvaluateExpression("a"));
        }

        [TestMethod]
        public void EvaluateSimpleArray()
        {
            Assert.IsNotNull(this.Execute("a = [1,2,3]"));
            var result = this.EvaluateExpression("a");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IList<object>));

            var list = (IList<object>)result;

            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(1, list[0]);
            Assert.AreEqual(2, list[1]);
            Assert.AreEqual(3, list[2]);
        }

        [TestMethod]
        public void EvaluateIntegerClassAsFixnum()
        {
            var result = this.EvaluateExpression("1.class");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FixnumClass));
        }

        [TestMethod]
        public void EvaluateStringClassAsString()
        {
            var result = this.EvaluateExpression("'foo'.class");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(StringClass));
        }

        private object EvaluateExpression(string text)
        {
            Parser parser = new Parser(text);
            IExpression expression = parser.ParseExpression();
            Assert.IsNull(parser.ParseExpression());
            return expression.Evaluate(this.machine.RootContext);
        }

        private object Execute(string text)
        {
            return this.machine.ExecuteText(text);
        }
    }
}
