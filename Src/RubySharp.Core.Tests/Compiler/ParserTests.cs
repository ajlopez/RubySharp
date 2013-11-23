namespace RubySharp.Core.Tests.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Commands;
    using RubySharp.Core.Compiler;
    using RubySharp.Core.Exceptions;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Language;

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
                Assert.IsInstanceOfType(ex, typeof(SyntaxError));
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
        public void ParseMultiplyTwoIntegers()
        {
            Parser parser = new Parser("3*2");
            var expected = new MultiplyExpression(new ConstantExpression(3), new ConstantExpression(2));
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseAddAndMultiplyIntegers()
        {
            Parser parser = new Parser("1+3*2");
            var expected = new AddExpression(new ConstantExpression(1), new MultiplyExpression(new ConstantExpression(3), new ConstantExpression(2)));
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseDivideTwoIntegers()
        {
            Parser parser = new Parser("3/2");
            var expected = new DivideExpression(new ConstantExpression(3), new ConstantExpression(2));
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSubtractAndDivideIntegers()
        {
            Parser parser = new Parser("1-3/2");
            var expected = new SubtractExpression(new ConstantExpression(1), new DivideExpression(new ConstantExpression(3), new ConstantExpression(2)));
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseNegativeInteger()
        {
            Parser parser = new Parser("-123");
            var expected = new NegativeExpression(new ConstantExpression(123));
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseNegativeIntegerMinusInteger()
        {
            Parser parser = new Parser("-123-10");
            var expected = new SubtractExpression(new NegativeExpression(new ConstantExpression(123)), new ConstantExpression(10));
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
        public void ParseAddTwoNameExpressions()
        {
            Parser parser = new Parser("foo+bar");
            var expected = new AddExpression(new NameExpression("foo"), new NameExpression("bar"));
            
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        [ExpectedException(typeof(SyntaxError))]
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
        [ExpectedException(typeof(SyntaxError))]
        public void RaiseIfEndOfCommandIsMissing()
        {
            Parser parser = new Parser("a=2 b=3\n");

            parser.ParseCommand();
        }

        [TestMethod]
        public void ParseAssignInstanceVarCommand()
        {
            Parser parser = new Parser("@a=2");
            var expected = new AssignInstanceVarCommand("a", new ConstantExpression(2));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseAssignDotCommand()
        {
            Parser parser = new Parser("a.b = 2");
            DotExpression dotexpr = (DotExpression)(new Parser("a.b")).ParseExpression();
            var expected = new AssignDotCommand(dotexpr, new ConstantExpression(2));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseTwoNameAsCall()
        {
            Parser parser = new Parser("a b\n");
            var expected = new CallExpression("a", new IExpression[] { new NameExpression("b") });
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseExpressionCommand()
        {
            Parser parser = new Parser("1+2");
            var expected = new AddExpression(new ConstantExpression(1), new ConstantExpression(2));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleNameAsExpressionCommand()
        {
            Parser parser = new Parser("a");
            var expected = new NameExpression("a");
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleNameAndNewLineAsExpressionCommand()
        {
            Parser parser = new Parser("a\n");
            var expected = new NameExpression("a");
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
        public void ParseSimpleIfElseCommand()
        {
            Parser parser = new Parser("if 1\n a=1\nelse\n a=2\nend");
            var expected = new IfCommand(new ConstantExpression(1), new AssignCommand("a", new ConstantExpression(1)), new AssignCommand("a", new ConstantExpression(2)));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleIfElifElseCommand()
        {
            Parser parser = new Parser("if 1\n a=1\nelif 2\n a=2\nelse\n a=3\nend");
            var innerexpected = new IfCommand(new ConstantExpression(2), new AssignCommand("a", new ConstantExpression(2)), new AssignCommand("a", new ConstantExpression(3)));
            var expected = new IfCommand(new ConstantExpression(1), new AssignCommand("a", new ConstantExpression(1)), innerexpected);
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
            var expected = new IfCommand(new ConstantExpression(1), new CompositeCommand(new IExpression[] { new AssignCommand("a", new ConstantExpression(1)), new AssignCommand("b", new ConstantExpression(2)) }));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseIfCommandWithCompositeThenCommand()
        {
            Parser parser = new Parser("if 1\n a=1\n b=2\nend");
            var expected = new IfCommand(new ConstantExpression(1), new CompositeCommand(new IExpression[] { new AssignCommand("a", new ConstantExpression(1)), new AssignCommand("b", new ConstantExpression(2)) }));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        [ExpectedException(typeof(SyntaxError))]
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
            var expected = new DefCommand("foo", new string[] { "a", "b" }, new AddExpression(new NameExpression("a"), new NameExpression("b")));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseDefCommandWithParametersInParentheses()
        {
            Parser parser = new Parser("def foo(a, b)\na+b\nend");
            var expected = new DefCommand("foo", new string[] { "a", "b" }, new AddExpression(new NameExpression("a"), new NameExpression("b")));
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
                Assert.IsInstanceOfType(ex, typeof(SyntaxError));
                Assert.AreEqual("name expected", ex.Message);
            }
        }

        [TestMethod]
        public void ParseSymbol()
        {
            Parser parser = new Parser(":foo");
            var expected = new ConstantExpression(new Symbol("foo"));
            var expression = parser.ParseExpression();

            Assert.IsNotNull(expression);
            Assert.AreEqual(expected, expression);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSimpleClassCommand()
        {
            Parser parser = new Parser("class Dog\na=1\nend");
            var expected = new ClassCommand("Dog", new AssignCommand("a", new ConstantExpression(1)));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseDotExpression()
        {
            Parser parser = new Parser("dog.foo");
            var expected = new DotExpression(new NameExpression("dog"), "foo", new IExpression[0]);

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseDotExpressionWithIntegerArgument()
        {
            Parser parser = new Parser("dog.foo 1");
            var expected = new DotExpression(new NameExpression("dog"), "foo", new IExpression[] { new ConstantExpression(1) });

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseDotExpressionWithIntegerArgumentInParentheses()
        {
            Parser parser = new Parser("dog.foo(1)");
            var expected = new DotExpression(new NameExpression("dog"), "foo", new IExpression[] { new ConstantExpression(1) });

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseDotExpressionWithTwoArgumentsInParentheses()
        {
            Parser parser = new Parser("dog.foo('foo', 'bar')");
            var expected = new DotExpression(new NameExpression("dog"), "foo", new IExpression[] { new ConstantExpression("foo"), new ConstantExpression("bar") });

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseDotExpressionWithIntegerAsTarget()
        {
            Parser parser = new Parser("1.foo");
            var expected = new DotExpression(new ConstantExpression(1), "foo", new IExpression[0]);

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseInstanceVariableExpression()
        {
            Parser parser = new Parser("@a");
            var expected = new InstanceVarExpression("a");

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseCompareExpressions()
        {
            Parser parser = new Parser("1==2 1!=2 1<2 1>2 1<=2 1>=2");
            var expected = new IExpression[] 
            {
                new CompareExpression(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.Equal),
                new CompareExpression(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.NotEqual),
                new CompareExpression(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.Less),
                new CompareExpression(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.Greater),
                new CompareExpression(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.LessOrEqual),
                new CompareExpression(new ConstantExpression(1), new ConstantExpression(2), CompareOperator.GreaterOrEqual)
            };

            foreach (var exp in expected)
                Assert.AreEqual(exp, parser.ParseExpression());

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseWhileCommand()
        {
            Parser cmdparser = new Parser("a = a + 1");
            IExpression body = cmdparser.ParseCommand();
            Parser exprparser = new Parser("a < 6");
            IExpression expr = exprparser.ParseExpression();

            Parser parser = new Parser("while a<6; a=a+1; end");
            IExpression expected = new WhileCommand(expr, body);

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseWhileCommandWithDo()
        {
            Parser cmdparser = new Parser("a = a + 1");
            IExpression body = cmdparser.ParseCommand();
            Parser exprparser = new Parser("a < 6");
            IExpression expr = exprparser.ParseExpression();

            Parser parser = new Parser("while a<6 do a=a+1; end");
            IExpression expected = new WhileCommand(expr, body);

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseUntilCommand()
        {
            Parser cmdparser = new Parser("a = a + 1");
            IExpression body = cmdparser.ParseCommand();
            Parser exprparser = new Parser("a >= 6");
            IExpression expr = exprparser.ParseExpression();

            Parser parser = new Parser("until a>=6; a=a+1; end");
            IExpression expected = new UntilCommand(expr, body);

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseUntilCommandWithDo()
        {
            Parser cmdparser = new Parser("a = a + 1");
            IExpression body = cmdparser.ParseCommand();
            Parser exprparser = new Parser("a >= 6");
            IExpression expr = exprparser.ParseExpression();

            Parser parser = new Parser("until a>=6 do a=a+1; end");
            IExpression expected = new UntilCommand(expr, body);

            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleList()
        {
            Parser parser = new Parser("[1,2,3]");
            IExpression expected = new ListExpression(new IExpression[] { new ConstantExpression(1), new ConstantExpression(2), new ConstantExpression(3) });

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSimpleListWithExpression()
        {
            Parser parser = new Parser("[1,1+1,3]");
            IExpression expected = new ListExpression(new IExpression[] { new ConstantExpression(1), new AddExpression(new ConstantExpression(1), new ConstantExpression(1)), new ConstantExpression(3) });

            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseExpression());
        }

        [TestMethod]
        public void ParseSimpleForInCommand()
        {
            Parser parser = new Parser("for k in [1,2,3]\n puts k\nend");
            var expected = new ForInCommand("k", new ListExpression(new IExpression[] { new ConstantExpression(1), new ConstantExpression(2), new ConstantExpression(3) }), new CallExpression("puts", new IExpression[] { new NameExpression("k") }));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleForInCommandWithDo()
        {
            Parser parser = new Parser("for k in [1,2,3] do\n puts k\nend");
            var expected = new ForInCommand("k", new ListExpression(new IExpression[] { new ConstantExpression(1), new ConstantExpression(2), new ConstantExpression(3) }), new CallExpression("puts", new IExpression[] { new NameExpression("k") }));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleForInCommandWithDoSingleLine()
        {
            Parser parser = new Parser("for k in [1,2,3] do puts(k) end");
            var expected = new ForInCommand("k", new ListExpression(new IExpression[] { new ConstantExpression(1), new ConstantExpression(2), new ConstantExpression(3) }), new CallExpression("puts", new IExpression[] { new NameExpression("k") }));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleForInCommandSingleLine()
        {
            Parser parser = new Parser("for k in [1,2,3]; puts k; end");
            var expected = new ForInCommand("k", new ListExpression(new IExpression[] { new ConstantExpression(1), new ConstantExpression(2), new ConstantExpression(3) }), new CallExpression("puts", new IExpression[] { new NameExpression("k") }));
            var result = parser.ParseCommand();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseSimpleIndexedExpression()
        {
            Parser parser = new Parser("a[1]");
            var expected = new IndexedExpression(new NameExpression("a"), new ConstantExpression(1));
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }

        [TestMethod]
        public void ParseCallWithDo()
        {
            Parser parser = new Parser("k.times do print 'foo' end");
            var expected = new DotExpression(new NameExpression("k"), "times", new IExpression[] { new BlockExpression(new Block(null, new CallExpression("print", new IExpression[] { new ConstantExpression("foo") })))} );
            var result = parser.ParseExpression();

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);

            Assert.IsNull(parser.ParseCommand());
        }
    }
}
