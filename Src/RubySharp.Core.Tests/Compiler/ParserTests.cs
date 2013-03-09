namespace RubySharp.Core.Tests.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Compiler;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Commands;

    [TestClass]
    public class ParserTests
    {
        [TestMethod]
        public void ParseInteger()
        {
            Parser parser = new Parser("123");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ConstantExpression));

            var expr = (ConstantExpression)result;
            Assert.AreEqual(123, expr.Value);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseName()
        {
            Parser parser = new Parser("foo");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NameExpression));

            var expr = (NameExpression)result;
            Assert.AreEqual("foo", expr.Name);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseAddTwoIntegers()
        {
            Parser parser = new Parser("1+2");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AddExpression));

            var expr = (AddExpression)result;
            Assert.IsNotNull(expr.LeftExpression);
            Assert.IsNotNull(expr.RightExpression);
            Assert.IsInstanceOfType(expr.LeftExpression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(expr.RightExpression, typeof(ConstantExpression));

            var lexpr = (ConstantExpression)expr.LeftExpression;
            Assert.AreEqual(1, lexpr.Value);

            var rexpr = (ConstantExpression)expr.RightExpression;
            Assert.AreEqual(2, rexpr.Value);
        }

        [TestMethod]
        public void ParseSubtractTwoIntegers()
        {
            Parser parser = new Parser("1-2");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SubtractExpression));

            var expr = (SubtractExpression)result;
            Assert.IsNotNull(expr.LeftExpression);
            Assert.IsNotNull(expr.RightExpression);
            Assert.IsInstanceOfType(expr.LeftExpression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(expr.RightExpression, typeof(ConstantExpression));

            var lexpr = (ConstantExpression)expr.LeftExpression;
            Assert.AreEqual(1, lexpr.Value);

            var rexpr = (ConstantExpression)expr.RightExpression;
            Assert.AreEqual(2, rexpr.Value);
        }

        [TestMethod]
        public void ParseSubtractThreeIntegers()
        {
            Parser parser = new Parser("1-2-3");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(SubtractExpression));

            var expr = (SubtractExpression)result;
            Assert.IsNotNull(expr.LeftExpression);
            Assert.IsNotNull(expr.RightExpression);
            Assert.IsInstanceOfType(expr.LeftExpression, typeof(SubtractExpression));
            Assert.IsInstanceOfType(expr.RightExpression, typeof(ConstantExpression));

            var lexpr = (SubtractExpression)expr.LeftExpression;
            Assert.IsInstanceOfType(lexpr.LeftExpression, typeof(ConstantExpression));
            Assert.IsInstanceOfType(lexpr.RightExpression, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)lexpr.LeftExpression).Value);
            Assert.AreEqual(2, ((ConstantExpression)lexpr.RightExpression).Value);

            var rexpr = (ConstantExpression)expr.RightExpression;
            Assert.AreEqual(3, rexpr.Value);
        }

        [TestMethod]
        public void ParseSimpleAssignCommand()
        {
            Parser parser = new Parser("a=2");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AssignCommand));

            var cmd = (AssignCommand)result;

            Assert.IsNotNull(cmd.Name);
            Assert.IsNotNull(cmd.Expression);

            Assert.AreEqual("a", cmd.Name);
            Assert.IsInstanceOfType(cmd.Expression, typeof(ConstantExpression));
            Assert.AreEqual(2, ((ConstantExpression)cmd.Expression).Value);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleAssignCommandWithEndOfLine()
        {
            Parser parser = new Parser("a=2\n");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AssignCommand));

            var cmd = (AssignCommand)result;

            Assert.IsNotNull(cmd.Name);
            Assert.IsNotNull(cmd.Expression);

            Assert.AreEqual("a", cmd.Name);
            Assert.IsInstanceOfType(cmd.Expression, typeof(ConstantExpression));
            Assert.AreEqual(2, ((ConstantExpression)cmd.Expression).Value);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void RaiseIfEndOfCommandIsMissing()
        {
            Parser parser = new Parser("a=2 b=3\n");

            parser.ParseCommand();
        }

        [TestMethod]
        public void ParseExpressionCommand()
        {
            Parser parser = new Parser("1+2");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ExpressionCommand));

            var cmd = (ExpressionCommand)result;

            Assert.IsNotNull(cmd.Expression);
            Assert.IsInstanceOfType(cmd.Expression, typeof(AddExpression));

            Assert.IsNull(parser.ParseCommand());
        }
    }
}
