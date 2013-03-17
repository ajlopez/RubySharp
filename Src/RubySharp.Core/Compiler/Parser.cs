namespace RubySharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Commands;
    using RubySharp.Core.Exceptions;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Language;

    public class Parser
    {
        private static string[][] binaryoperators = new string[][] { new string[] { "==", "!=", "<", ">", "<=", ">=" }, new string[] { "+", "-" }, new string[] { "*", "/" } };
        private Lexer lexer;

        public Parser(string text)
        {
            this.lexer = new Lexer(text);
        }

        public IExpression ParseExpression()
        {
            var result = this.ParseBinaryExpression(0);

            if (!(result is NameExpression))
                return result;

            if (this.NextTokenIsEndOfCommand())
                return result;
            if (this.NextTokenIsOperator())
                return result;

            return new CallExpression(((NameExpression)result).Name, this.ParseExpressionList());
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

                if (token.Value == "class")
                    return this.ParseClassCommand();
            }

            this.lexer.PushToken(token);

            IExpression expr = this.ParseExpression();

            if (!(expr is NameExpression) && !(expr is InstanceVarExpression))
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

            ICommand cmd;

            if (expr is NameExpression)
                cmd = new AssignCommand(((NameExpression)expr).Name, this.ParseExpression());
            else
                cmd = new AssignInstanceVarCommand(((InstanceVarExpression)expr).Name, this.ParseExpression());

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
            IList<string> parameters = this.ParseParameterList();
            this.ParseEndOfCommand();
            ICommand body = this.ParseCommandList();
            this.ParseEndOfCommand();
            return new DefCommand(name, parameters, body);
        }

        private ClassCommand ParseClassCommand()
        {
            string name = this.ParseName();
            this.ParseEndOfCommand();
            ICommand body = this.ParseCommandList();
            this.ParseEndOfCommand();
            return new ClassCommand(name, body);
        }

        private IList<string> ParseParameterList()
        {
            IList<string> parameters = new List<string>();

            bool inparentheses = this.TryParseToken(TokenType.Separator, "(");

            for (string name = this.TryParseName(); name != null; name = this.ParseName())
            {
                parameters.Add(name);
                if (!this.TryParseToken(TokenType.Separator, ","))
                    break;
            }

            if (inparentheses)
                this.ParseToken(TokenType.Separator, ")");

            return parameters;
        }

        private IList<IExpression> ParseExpressionList()
        {
            IList<IExpression> expressions = new List<IExpression>();

            bool inparentheses = this.TryParseToken(TokenType.Separator, "(");

            for (IExpression expression = this.ParseExpression(); expression != null; expression = this.ParseExpression())
            {
                expressions.Add(expression);
                if (!this.TryParseToken(TokenType.Separator, ","))
                    break;
            }

            if (inparentheses)
                this.ParseToken(TokenType.Separator, ")");

            return expressions;
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
                throw new SyntaxError("end of command expected");
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

        private bool NextTokenStartsExpressionList()
        {
            Token token = this.lexer.NextToken();
            this.lexer.PushToken(token);

            if (token == null)
                return false;

            if (this.IsEndOfCommand(token))
                return false;

            if (token.Type == TokenType.Operator)
                return false;

            if (token.Type == TokenType.Separator)
                return token.Value == "(";

            return true;
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

        private IExpression ParseBinaryExpression(int level)
        {
            if (level >= binaryoperators.Length)
                return this.ParseTerm();

            IExpression expr = this.ParseBinaryExpression(level + 1);

            if (expr == null)
                return null;

            Token token;

            for (token = this.lexer.NextToken(); token != null && this.IsBinaryOperator(level, token); token = this.lexer.NextToken())
            {
                if (token.Value == "+")
                    expr = new AddExpression(expr, this.ParseBinaryExpression(level + 1));
                if (token.Value == "-")
                    expr = new SubtractExpression(expr, this.ParseBinaryExpression(level + 1));
                if (token.Value == "*")
                    expr = new MultiplyExpression(expr, this.ParseBinaryExpression(level + 1));
                if (token.Value == "/")
                    expr = new DivideExpression(expr, this.ParseBinaryExpression(level + 1));
                if (token.Value == "==")
                    expr = new CompareExpression(expr, this.ParseBinaryExpression(level + 1), CompareOperator.Equal);
                if (token.Value == "!=")
                    expr = new CompareExpression(expr, this.ParseBinaryExpression(level + 1), CompareOperator.NotEqual);
                if (token.Value == "<")
                    expr = new CompareExpression(expr, this.ParseBinaryExpression(level + 1), CompareOperator.Less);
                if (token.Value == ">")
                    expr = new CompareExpression(expr, this.ParseBinaryExpression(level + 1), CompareOperator.Greater);
                if (token.Value == "<=")
                    expr = new CompareExpression(expr, this.ParseBinaryExpression(level + 1), CompareOperator.LessOrEqual);
                if (token.Value == ">=")
                    expr = new CompareExpression(expr, this.ParseBinaryExpression(level + 1), CompareOperator.GreaterOrEqual);
            }

            if (token != null)
                this.lexer.PushToken(token);

            return expr;
        }

        private IExpression ParseTerm()
        {
            IExpression expression = this.ParseSimpleTerm();

            if (expression == null)
                return null;

            while (this.TryParseToken(TokenType.Separator, "."))
            {
                string name = this.ParseName();

                while (this.NextTokenStartsExpressionList())
                    expression = new DotExpression(expression, name, this.ParseExpressionList());

                if (!(expression is DotExpression))
                    expression = new DotExpression(expression, name, new IExpression[0]);
            }

            return expression;
        }

        private IExpression ParseSimpleTerm()
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

            if (token.Type == TokenType.InstanceVarName)
                return new InstanceVarExpression(token.Value);

            if (token.Type == TokenType.Symbol)
                return new ConstantExpression(new Symbol(token.Value));

            if (token.Type == TokenType.Separator && token.Value == "(")
            {
                IExpression expr = this.ParseExpression();
                this.ParseToken(TokenType.Separator, ")");
                return expr;
            }

            throw new SyntaxError(string.Format("unexpected '{0}'", token.Value));
        }

        private void ParseName(string name)
        {
            this.ParseToken(TokenType.Name, name);
        }

        private void ParseToken(TokenType type, string value)
        {
            Token token = this.lexer.NextToken();

            if (token == null || token.Type != type || token.Value != value)
                throw new SyntaxError(string.Format("expected '{0}'", value));
        }

        private string ParseName()
        {
            Token token = this.lexer.NextToken();

            if (token == null || token.Type != TokenType.Name)
                throw new SyntaxError("name expected");

            return token.Value;
        }

        private bool TryParseName(string name)
        {
            return this.TryParseToken(TokenType.Name, name);
        }

        private bool TryParseToken(TokenType type, string value)
        {
            Token token = this.lexer.NextToken();

            if (token != null && token.Type == type && token.Value == value)
                return true;

            this.lexer.PushToken(token);

            return false;
        }

        private string TryParseName()
        {
            Token token = this.lexer.NextToken();

            if (token != null && token.Type == TokenType.Name)
                return token.Value;

            this.lexer.PushToken(token);

            return null;
        }

        private bool TryParseEndOfLine()
        {
            Token token = this.lexer.NextToken();

            if (token != null && token.Type == TokenType.EndOfLine && token.Value == "\n")
                return true;

            this.lexer.PushToken(token);

            return false;
        }

        private bool IsBinaryOperator(int level, Token token)
        {
            return token.Type == TokenType.Operator && binaryoperators[level].Contains(token.Value);
        }
    }
}
