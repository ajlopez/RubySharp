namespace RubySharp.Core.Tests.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Compiler;
    using RubySharp.Core.Exceptions;

    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void GetName()
        {
            Lexer lexer = new Lexer("name");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("name", result.Value);
            Assert.AreEqual(TokenType.Name, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameWithDigits()
        {
            Lexer lexer = new Lexer("name123");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("name123", result.Value);
            Assert.AreEqual(TokenType.Name, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameWithUnderscore()
        {
            Lexer lexer = new Lexer("name_123");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("name_123", result.Value);
            Assert.AreEqual(TokenType.Name, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameWithInitialUnderscore()
        {
            Lexer lexer = new Lexer("_foo");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("_foo", result.Value);
            Assert.AreEqual(TokenType.Name, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void RaiseIfUnexpectedCharacter()
        {
            Lexer lexer = new Lexer("\\");

            try
            {
                lexer.NextToken();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(SyntaxError));
                Assert.AreEqual("unexpected '\\'", ex.Message);
            }
        }

        [TestMethod]
        public void GetNameWithSpaces()
        {
            Lexer lexer = new Lexer("  name   ");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("name", result.Value);
            Assert.AreEqual(TokenType.Name, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void SkipComment()
        {
            Lexer lexer = new Lexer("# this is a comment");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void SkipCommentUpToEndOfLine()
        {
            Lexer lexer = new Lexer("# this is a comment\n");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.EndOfLine, token.Type);
            Assert.AreEqual("\n", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetSymbol()
        {
            Lexer lexer = new Lexer(":foo");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("foo", result.Value);
            Assert.AreEqual(TokenType.Symbol, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetSymbolWithDigits()
        {
            Lexer lexer = new Lexer(":foo123");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("foo123", result.Value);
            Assert.AreEqual(TokenType.Symbol, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetSymbolWithUnderscore()
        {
            Lexer lexer = new Lexer(":foo_123");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("foo_123", result.Value);
            Assert.AreEqual(TokenType.Symbol, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetSymbolWithInitialUnderscore()
        {
            Lexer lexer = new Lexer(":_123");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("_123", result.Value);
            Assert.AreEqual(TokenType.Symbol, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void RaiseIfSymbolStartsWithADigit()
        {
            Lexer lexer = new Lexer(":123");

            try
            {
                lexer.NextToken();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(SyntaxError));
                Assert.AreEqual("unexpected integer", ex.Message);
            }
        }

        [TestMethod]
        public void GetInstanceVarName()
        {
            Lexer lexer = new Lexer("@a");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("a", result.Value);
            Assert.AreEqual(TokenType.InstanceVarName, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetInstanceVarNameWithDigits()
        {
            Lexer lexer = new Lexer("@a123");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("a123", result.Value);
            Assert.AreEqual(TokenType.InstanceVarName, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetInstanceVarNameWithDigitsAndUnderscore()
        {
            Lexer lexer = new Lexer("@a_123");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("a_123", result.Value);
            Assert.AreEqual(TokenType.InstanceVarName, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetInstanceVarNameWithInitialUnderscore()
        {
            Lexer lexer = new Lexer("@_123");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("_123", result.Value);
            Assert.AreEqual(TokenType.InstanceVarName, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void RaiseWhenInvalidInstanceVarName()
        {
            Lexer lexer = new Lexer("@");

            try
            {
                lexer.NextToken();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(SyntaxError));
                Assert.AreEqual("invalid instance variable name", ex.Message);
            }
        }

        [TestMethod]
        public void RaiseWhenInstanceVarNameStartsWithADigit()
        {
            Lexer lexer = new Lexer("@123");

            try
            {
                lexer.NextToken();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(SyntaxError));
                Assert.AreEqual("invalid instance variable name", ex.Message);
            }
        }

        [TestMethod]
        public void GetInteger()
        {
            Lexer lexer = new Lexer("123");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("123", result.Value);
            Assert.AreEqual(TokenType.Integer, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetIntegerWithSpaces()
        {
            Lexer lexer = new Lexer("  123   ");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("123", result.Value);
            Assert.AreEqual(TokenType.Integer, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetSingleQuoteString()
        {
            Lexer lexer = new Lexer("'foo'");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("foo", result.Value);
            Assert.AreEqual(TokenType.String, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        [ExpectedException(typeof(SyntaxError))]
        public void RaiseIfSingleQuoteStringIsNotClosed()
        {
            Lexer lexer = new Lexer("'foo");
            lexer.NextToken();
        }

        [TestMethod]
        public void GetAssignOperator()
        {
            Lexer lexer = new Lexer("=");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("=", result.Value);
            Assert.AreEqual(TokenType.Operator, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetComparisonOperators()
        {
            Lexer lexer = new Lexer("== != < > <= >=");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("==", result.Value);
            Assert.AreEqual(TokenType.Operator, result.Type);

            result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("!=", result.Value);
            Assert.AreEqual(TokenType.Operator, result.Type);

            result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("<", result.Value);
            Assert.AreEqual(TokenType.Operator, result.Type);

            result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual(">", result.Value);
            Assert.AreEqual(TokenType.Operator, result.Type);

            result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("<=", result.Value);
            Assert.AreEqual(TokenType.Operator, result.Type);

            result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual(">=", result.Value);
            Assert.AreEqual(TokenType.Operator, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetSemicolonAsSeparator()
        {
            Lexer lexer = new Lexer(";");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual(";", result.Value);
            Assert.AreEqual(TokenType.Separator, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetCommaAsSeparator()
        {
            Lexer lexer = new Lexer(",");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual(",", result.Value);
            Assert.AreEqual(TokenType.Separator, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetParenthesesAsSeparators()
        {
            Lexer lexer = new Lexer("()");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("(", result.Value);
            Assert.AreEqual(TokenType.Separator, result.Type);

            result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual(")", result.Value);
            Assert.AreEqual(TokenType.Separator, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetPlusAsOperator()
        {
            Lexer lexer = new Lexer("+");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual("+", result.Value);
            Assert.AreEqual(TokenType.Operator, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetEndOfLineWithNewLine()
        {
            Lexer lexer = new Lexer("\n");
            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual(TokenType.EndOfLine, result.Type);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetFourArithmeticOperators()
        {
            Lexer lexer = new Lexer("+ - * /");

            for (int k = 0; k < 4; k++)
            {
                var result = lexer.NextToken();
                Assert.IsNotNull(result);
                Assert.AreEqual(TokenType.Operator, result.Type);
                Assert.IsNotNull(result.Value);
                Assert.AreEqual(1, result.Value.Length);
                Assert.AreEqual("+-*/"[k], result.Value[0]);
            }

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetSimpleAdd()
        {
            Lexer lexer = new Lexer("1+2");

            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual(TokenType.Integer, result.Type);
            Assert.AreEqual("1", result.Value);

            result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual(TokenType.Operator, result.Type);
            Assert.AreEqual("+", result.Value);

            result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual(TokenType.Integer, result.Type);
            Assert.AreEqual("2", result.Value);
        }

        [TestMethod]
        public void GetSimpleAddNames()
        {
            Lexer lexer = new Lexer("one+two");

            var result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual(TokenType.Name, result.Type);
            Assert.AreEqual("one", result.Value);

            result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual(TokenType.Operator, result.Type);
            Assert.AreEqual("+", result.Value);

            result = lexer.NextToken();

            Assert.IsNotNull(result);
            Assert.AreEqual(TokenType.Name, result.Type);
            Assert.AreEqual("two", result.Value);
        }
    }
}
