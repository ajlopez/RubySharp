namespace RubySharp.Core.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Compiler;
    using RubySharp.Core.Expressions;

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

        private object EvaluateExpression(string text)
        {
            Parser parser = new Parser(text);
            IExpression expression = parser.ParseExpression();
            Assert.IsNull(parser.ParseExpression());
            return expression.Evaluate(this.machine.RootContext);
        }
    }
}
