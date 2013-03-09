namespace RubySharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Commands;
    using RubySharp.Core.Expressions;

    public class Parser
    {
        private static string[] binaryoperators = new string[] { "+", "-" };
        private Lexer lexer;

        public Parser(string text)
        {
            this.lexer = new Lexer(text);
        }

        public IExpression ParseExpression()
        {
            return this.ParseBinaryExpression();
        }

        public ICommand ParseCommand()
        {
            IExpression expr = this.ParseExpression();

            if (expr == null)
                return null;

            if (!(expr is NameExpression))
            {
                this.ParseEndOfCommand();
                return new ExpressionCommand(expr);
            }

            Token token = this.lexer.NextToken();

            if (token == null)
            {
                this.ParseEndOfCommand();
                return new ExpressionCommand(expr);
            }

            if (token.Type != TokenType.Operator || token.Value != "=")
            {
                this.lexer.PushToken(token);
                this.ParseEndOfCommand();
                return new ExpressionCommand(expr);
            }

            var cmd = new AssignCommand(((NameExpression)expr).Name, this.ParseExpression());

            this.ParseEndOfCommand();

            return cmd;
        }

        private void ParseEndOfCommand()
        {
            Token token = this.lexer.NextToken();

            if (token == null)
                return;

            if (token.Type == TokenType.EndOfLine)
                return;

            throw new ParserException("end of command expected");
        }

        private IExpression ParseBinaryExpression()
        {
            IExpression expr = this.ParseTerm();

            if (expr == null)
                return null;

            Token token;

            for (token = this.lexer.NextToken(); token != null && this.IsBinaryOperator(token); token = this.lexer.NextToken())
            {
                if (token.Value == "+")
                    expr = new AddExpression(expr, this.ParseTerm());
                if (token.Value == "-")
                    expr = new SubtractExpression(expr, this.ParseTerm());
            }

            if (token != null)
                this.lexer.PushToken(token);

            return expr;
        }

        private IExpression ParseTerm()
        {
            Token token = this.lexer.NextToken();

            if (token == null)
                return null;

            if (token.Type == TokenType.Integer)
                return new ConstantExpression(int.Parse(token.Value));

            return new NameExpression(token.Value);
        }

        private bool IsBinaryOperator(Token token)
        {
            return token.Type == TokenType.Operator && binaryoperators.Contains(token.Value);
        }
    }
}
