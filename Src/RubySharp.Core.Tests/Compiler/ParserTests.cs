namespace RubySharp.Core.Tests.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Commands;
    using RubySharp.Core.Compiler;
    using RubySharp.Core.Expressions;

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
        public void ParseSingleQuoteString()
        {
            Parser parser = new Parser("'foo'");
            var expected = new ConstantExpression("foo");
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
        public void ParseAddTwoIntegersInParentheses()
        {
            Parser parser = new Parser("(1+2)");
            var expected = new AddExpression(new ConstantExpression(1), new ConstantExpression(2));
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void RaiseIsMissingParenthesis()
        {
            Parser parser = new Parser("(1+2");

            try
            {
                parser.ParseExpression();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual("expected ')'", ex.Message);
            }
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
        public void ParseCallExpressionSimplePuts()
        {
            Parser parser = new Parser("puts 123");
            var expected = new CallExpression("puts", new IExpression[] { new ConstantExpression(123) });
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseCallExpressionPutsWithTwoArguments()
        {
            Parser parser = new Parser("puts 1,2");
            var expected = new CallExpression("puts", new IExpression[] { new ConstantExpression(1), new ConstantExpression(2) });
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseCallExpressionPutsWithParentheses()
        {
            Parser parser = new Parser("puts(123)");
            var expected = new CallExpression("puts", new IExpression[] { new ConstantExpression(123) });
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseCallExpressionPutsWithTwoArgumentsAndParentheses()
        {
            Parser parser = new Parser("puts(1,2)");
            var expected = new CallExpression("puts", new IExpression[] { new ConstantExpression(1), new ConstantExpression(2) });
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void RaiseIfNotAnExpression()
        {
            Parser parser = new Parser("+");
            parser.ParseExpression();
        }

        [TestMethod]
        public void ParseSimpleAssignCommand()
        {
            Parser parser = new Parser("a=2");
            var expected = new AssignCommand("a", new ConstantExpression(2));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleAssignCommandWithEndOfLine()
        {
            Parser parser = new Parser("a=2\n");
            var expected = new AssignCommand("a", new ConstantExpression(2));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleAssignCommandPrecededByAnEndOfLine()
        {
            Parser parser = new Parser("\na=2");
            var expected = new AssignCommand("a", new ConstantExpression(2));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

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
        public void ParseTwoNameAsCall()
        {
            Parser parser = new Parser("a b\n");
            var expected = new ExpressionCommand(new CallExpression("a", new IExpression[] { new NameExpression("b") }));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseExpressionCommand()
        {
            Parser parser = new Parser("1+2");
            var expected = new ExpressionCommand(new AddExpression(new ConstantExpression(1), new ConstantExpression(2)));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleNameAsExpressionCommand()
        {
            Parser parser = new Parser("a");
            var expected = new ExpressionCommand(new NameExpression("a"));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleNameAndNewLineAsExpressionCommand()
        {
            Parser parser = new Parser("a\n");
            var expected = new ExpressionCommand(new NameExpression("a"));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleIfCommand()
        {
            Parser parser = new Parser("if 1\n a=1\nend");
            var expected = new IfCommand(new ConstantExpression(1), new AssignCommand("a", new ConstantExpression(1)));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleIfCommandWithThen()
        {
            Parser parser = new Parser("if 1 then\n a=1\nend");
            var expected = new IfCommand(new ConstantExpression(1), new AssignCommand("a", new ConstantExpression(1)));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleIfCommandWithThenOneLine()
        {
            Parser parser = new Parser("if 1 then a=1 end");
            var expected = new IfCommand(new ConstantExpression(1), new AssignCommand("a", new ConstantExpression(1)));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleIfCommandWithSemicolonOneLine()
        {
            Parser parser = new Parser("if 1; a=1 end");
            var expected = new IfCommand(new ConstantExpression(1), new AssignCommand("a", new ConstantExpression(1)));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleIfCommandWithThenOneLineAndOtherLine()
        {
            Parser parser = new Parser("if 1 then a=1\nb=2 end");
            var expected = new IfCommand(new ConstantExpression(1), new CompositeCommand(new ICommand[] { new AssignCommand("a", new ConstantExpression(1)), new AssignCommand("b", new ConstantExpression(2)) }));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseIfCommandWithCompositeThenCommand()
        {
            Parser parser = new Parser("if 1\n a=1\n b=2\nend");
            var expected = new IfCommand(new ConstantExpression(1), new CompositeCommand(new ICommand[] { new AssignCommand("a", new ConstantExpression(1)), new AssignCommand("b", new ConstantExpression(2)) }));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        [ExpectedException(typeof(ParserException))]
        public void RaiseIfNoEndAtIf()
        {
            Parser parser = new Parser("if 1\n a=1\n");

            parser.ParseCommand();
        }

        [TestMethod]
        public void ParseSimpleDefCommand()
        {
            Parser parser = new Parser("def foo\na=1\nend");
            var expected = new DefCommand("foo", new string[] { }, new AssignCommand("a", new ConstantExpression(1)));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseDefCommandWithParameters()
        {
            Parser parser = new Parser("def foo a, b\na+b\nend");
            var expected = new DefCommand("foo", new string[] { "a", "b" }, new ExpressionCommand(new AddExpression(new NameExpression("a"), new NameExpression("b"))));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseDefCommandWithParametersInParentheses()
        {
            Parser parser = new Parser("def foo(a, b)\na+b\nend");
            var expected = new DefCommand("foo", new string[] { "a", "b" }, new ExpressionCommand(new AddExpression(new NameExpression("a"), new NameExpression("b"))));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void RaiseIfDefHasNoName()
        {
            Parser parser = new Parser("def \na=1\nend");

            try
            {
                parser.ParseCommand();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ParserException));
                Assert.AreEqual("name expected", ex.Message);
            }
        }
    }
}
