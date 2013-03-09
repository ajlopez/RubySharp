namespace RubySharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Expressions;

    public class Parser
    {
        private Lexer lexer;

        public Parser(string text)
        {
            this.lexer = new Lexer(text);
        }

        public IExpression ParseExpression()
        {
            return this.ParseBinaryExpression();
        }

        private IExpression ParseBinaryExpression()
        {
            IExpression expr = this.ParseTerm();

            if (expr == null)
                return null;

            for (Token token = this.lexer.NextToken(); token != null && token.Type == TokenType.Operator; token = this.lexer.NextToken())
            {
                if (token.Value == "+")
                    expr = new AddExpression(expr, this.ParseTerm());
                if (token.Value == "-")
                    expr = new SubtractExpression(expr, this.ParseTerm());
            }

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
    }
}
