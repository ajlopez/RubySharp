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
            var expected = new ConstantExpression(123);
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseName()
        {
            Parser parser = new Parser("foo");
            var expected = new NameExpression("foo");
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseAddTwoIntegers()
        {
            Parser parser = new Parser("1+2");
            var expected = new AddExpression(new ConstantExpression(1), new ConstantExpression(2));
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSubtractTwoIntegers()
        {
            Parser parser = new Parser("1-2");
            var expected = new SubtractExpression(new ConstantExpression(1), new ConstantExpression(2));
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSubtractThreeIntegers()
        {
            Parser parser = new Parser("1-2-3");
            var expected = new SubtractExpression(new SubtractExpression(new ConstantExpression(1), new ConstantExpression(2)), new ConstantExpression(3));
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
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
        [ExpectedException(typeof(ParserException))]
        public void RaiseIfItIsNotAnAssignment()
        {
            Parser parser = new Parser("a b\n");

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

        [TestMethod]
        public void ParseSimpleNameAsExpressionCommand()
        {
            Parser parser = new Parser("a");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ExpressionCommand));

            var cmd = (ExpressionCommand)result;

            Assert.IsNotNull(cmd.Expression);
            Assert.IsInstanceOfType(cmd.Expression, typeof(NameExpression));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleNameAndNewLineAsExpressionCommand()
        {
            Parser parser = new Parser("a\n");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ExpressionCommand));

            var cmd = (ExpressionCommand)result;

            Assert.IsNotNull(cmd.Expression);
            Assert.IsInstanceOfType(cmd.Expression, typeof(NameExpression));

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleIfCommand()
        {
            Parser parser = new Parser("if 1\n a=1\nend");

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IfCommand));

            var cmd = (IfCommand)result;

            Assert.IsNotNull(cmd.Condition);
            Assert.IsInstanceOfType(cmd.Condition, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)cmd.Condition).Value);

            Assert.IsNotNull(cmd.ThenCommand);
            Assert.IsInstanceOfType(cmd.ThenCommand, typeof(AssignCommand));

            var assign = (AssignCommand)cmd.ThenCommand;

            Assert.AreEqual("a", assign.Name);
            Assert.IsNotNull(assign.Expression);
            Assert.IsInstanceOfType(assign.Expression, typeof(ConstantExpression));
            Assert.AreEqual(1, ((ConstantExpression)assign.Expression).Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void RaiseIfNoEndAtIf()
        {
            Parser parser = new Parser("if 1\n a=1\n");

            parser.ParseCommand();
        }
    }
}
