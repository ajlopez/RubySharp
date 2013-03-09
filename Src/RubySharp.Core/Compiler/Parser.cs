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
            NameExpression expr = (NameExpression)this.ParseExpression();

            if (expr == null)
                return null;

            Token token = this.lexer.NextToken();

            return new AssignCommand(expr.Name, this.ParseExpression());
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
