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
            var result = this.ParseBinaryExpression();

            if (!(result is NameExpression))
                return result;

            if (this.NextTokenIsEndOfCommand())
                return result;
            if (this.NextTokenIsOperator())
                return result;

            var expr = this.ParseExpression();

            return new CallExpression(((NameExpression)result).Name, new IExpression[] { expr });
        }

        public ICommand ParseCommand()
        {
            Token token = this.lexer.NextToken();

            while (token != null && token.Type == TokenType.EndOfLine)
                token = this.lexer.NextToken();

            if (token == null)
                return null;

            if (token.Type == TokenType.Name)
            {
                if (token.Value == "if")
                    return this.ParseIfCommand();

                if (token.Value == "def")
                    return this.ParseDefCommand();
            }

            this.lexer.PushToken(token);

            IExpression expr = this.ParseExpression();

            if (!(expr is NameExpression))
            {
                this.ParseEndOfCommand();
                return new ExpressionCommand(expr);
            }

            token = this.lexer.NextToken();

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

        private IfCommand ParseIfCommand()
        {
            IExpression condition = this.ParseExpression();
            if (this.TryParseName("then"))
                this.TryParseEndOfLine();
            else
                this.ParseEndOfCommand();
            ICommand thencommand = this.ParseCommandList();
            this.ParseEndOfCommand();
            return new IfCommand(condition, thencommand);
        }

        private DefCommand ParseDefCommand()
        {
            string name = this.ParseName();
            this.ParseEndOfCommand();
            IList<string> parameters = new List<string>();
            ICommand body = this.ParseCommandList();
            this.ParseEndOfCommand();
            return new DefCommand(name, parameters, body);
        }

        private ICommand ParseCommandList()
        {
            Token token;
            IList<ICommand> commands = new List<ICommand>();

            for (token = this.lexer.NextToken(); token != null && (token.Type != TokenType.Name || token.Value != "end"); token = this.lexer.NextToken())
            {
                this.lexer.PushToken(token);
                commands.Add(this.ParseCommand());
            }

            this.lexer.PushToken(token);
            this.ParseName("end");

            if (commands.Count == 1)
                return commands[0];

            return new CompositeCommand(commands);
        }

        private void ParseEndOfCommand()
        {
            Token token = this.lexer.NextToken();

            if (token != null && token.Type == TokenType.Name && token.Value == "end")
            {
                this.lexer.PushToken(token);
                return;
            }

            if (!this.IsEndOfCommand(token))
                throw new ParserException("end of command expected");
        }

        private bool NextTokenIsEndOfCommand()
        {
            Token token = this.lexer.NextToken();
            this.lexer.PushToken(token);
            return this.IsEndOfCommand(token);
        }

        private bool NextTokenIsOperator()
        {
            Token token = this.lexer.NextToken();
            this.lexer.PushToken(token);
            return token.Type == TokenType.Operator;
        }

        private bool IsEndOfCommand(Token token)
        {
            if (token == null)
                return true;

            if (token.Type == TokenType.EndOfLine)
                return true;

            if (token.Type == TokenType.Separator && token.Value == ";")
                return true;

            return false;
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

            if (token.Type == TokenType.String)
                return new ConstantExpression(token.Value);

            if (token.Type == TokenType.Name)    
                return new NameExpression(token.Value);

            if (token.Type == TokenType.Separator && token.Value == "(")
            {
                IExpression expr = this.ParseExpression();
                this.ParseToken(TokenType.Separator, ")");
                return expr;
            }

            throw new ParserException(string.Format("unexpected '{0}'", token.Value));
        }

        private void ParseName(string name)
        {
            this.ParseToken(TokenType.Name, name);
        }

        private void ParseToken(TokenType type, string value)
        {
            Token token = this.lexer.NextToken();

            if (token == null || token.Type != type || token.Value != value)
                throw new ParserException(string.Format("expected '{0}'", value));
        }

        private string ParseName()
        {
            Token token = this.lexer.NextToken();

            if (token == null || token.Type != TokenType.Name)
                throw new ParserException("name expected");

            return token.Value;
        }

        private bool TryParseName(string name)
        {
            Token token = this.lexer.NextToken();

            if (token != null && token.Type == TokenType.Name && token.Value == name)
                return true;

            this.lexer.PushToken(token);

            return false;
        }

        private bool TryParseEndOfLine()
        {
            Token token = this.lexer.NextToken();

            if (token != null && token.Type == TokenType.EndOfLine && token.Value == "\n")
                return true;

            this.lexer.PushToken(token);

            return false;
        }

        private bool IsBinaryOperator(Token token)
        {
            return token.Type == TokenType.Operator && binaryoperators.Contains(token.Value);
        }
    }
}
