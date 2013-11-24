namespace RubySharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
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

        public Parser(TextReader reader)
        {
            this.lexer = new Lexer(reader);
        }

        public IExpression ParseExpression()
        {
            var result = this.ParseBinaryExpression(0);

            if (!(result is NameExpression))
                return result;

            var nexpr = (NameExpression)result;

            if (this.TryParseToken(TokenType.Separator, "{"))
                return new CallExpression(nexpr.Name, new IExpression[] { new BlockExpression(this.ParseBlock(true)) });

            if (this.TryParseName("do"))
                return new CallExpression(nexpr.Name, new IExpression[] { new BlockExpression(this.ParseBlock()) });

            if (!this.NextTokenStartsExpressionList())
                return result;

            return new CallExpression(((NameExpression)result).Name, this.ParseExpressionList());
        }

        public IExpression ParseCommand()
        {
            Token token = this.lexer.NextToken();

            while (token != null && this.IsEndOfCommand(token))
                token = this.lexer.NextToken();

            if (token == null)
                return null;

            this.lexer.PushToken(token);

            IExpression expr = this.ParseExpression();

            if (!(expr is NameExpression) && !(expr is ClassVarExpression) && !(expr is InstanceVarExpression) && !(expr is DotExpression))
            {
                this.ParseEndOfCommand();
                return expr;
            }

            token = this.lexer.NextToken();

            if (token == null)
            {
                this.ParseEndOfCommand();
                return expr;
            }

            if (token.Type != TokenType.Operator || token.Value != "=")
            {
                this.lexer.PushToken(token);
                this.ParseEndOfCommand();
                return expr;
            }

            IExpression cmd = null;

            if (expr is NameExpression)
                cmd = new AssignExpression(((NameExpression)expr).Name, this.ParseExpression());
            else if (expr is DotExpression)
                cmd = new AssignDotExpressions((DotExpression)expr, this.ParseExpression());
            else if (expr is InstanceVarExpression)
                cmd = new AssignInstanceVarExpression(((InstanceVarExpression)expr).Name, this.ParseExpression());
            else
                cmd = new AssignClassVarExpression(((ClassVarExpression)expr).Name, this.ParseExpression());

            this.ParseEndOfCommand();

            return cmd;
        }

        private IfExpression ParseIfExpression()
        {
            IExpression condition = this.ParseExpression();
            if (this.TryParseName("then"))
                this.TryParseEndOfLine();
            else
                this.ParseEndOfCommand();

            IExpression thencommand = this.ParseCommandList(new string[] { "end", "elif", "else" });
            IExpression elsecommand = null;

            if (this.TryParseName("elif"))
                elsecommand = this.ParseIfExpression();
            else if (this.TryParseName("else"))
                elsecommand = this.ParseCommandList();
            else
                this.ParseName("end");

            return new IfExpression(condition, thencommand, elsecommand);
        }

        private ForInExpression ParseForInExpression()
        {
            string name = this.ParseName();
            this.ParseName("in");
            IExpression expression = this.ParseExpression();
            if (this.TryParseName("do"))
                this.TryParseEndOfLine();
            else
                this.ParseEndOfCommand();
            IExpression command = this.ParseCommandList();
            return new ForInExpression(name, expression, command);
        }

        private WhileExpression ParseWhileExpression()
        {
            IExpression condition = this.ParseExpression();
            if (this.TryParseName("do"))
                this.TryParseEndOfLine();
            else
                this.ParseEndOfCommand();
            IExpression command = this.ParseCommandList();
            return new WhileExpression(condition, command);
        }

        private UntilExpression ParseUntilExpression()
        {
            IExpression condition = this.ParseExpression();
            if (this.TryParseName("do"))
                this.TryParseEndOfLine();
            else
                this.ParseEndOfCommand();
            IExpression command = this.ParseCommandList();
            return new UntilExpression(condition, command);
        }

        private DefExpression ParseDefExpression()
        {
            string name = this.ParseName();
            IList<string> parameters = this.ParseParameterList();
            IExpression body = this.ParseCommandList();
            return new DefExpression(name, parameters, body);
        }

        private ClassExpression ParseClassExpression()
        {
            string name = this.ParseName();
            this.ParseEndOfCommand();
            IExpression body = this.ParseCommandList();
            return new ClassExpression(name, body);
        }

        private ModuleExpression ParseModuleExpression()
        {
            string name = this.ParseName();
            IExpression body = this.ParseCommandList();
            return new ModuleExpression(name, body);
        }

        private IList<string> ParseParameterList(bool canhaveparens = true)
        {
            IList<string> parameters = new List<string>();

            bool inparentheses = false;
            
            if (canhaveparens)
                inparentheses = this.TryParseToken(TokenType.Separator, "(");

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
            {
                this.ParseToken(TokenType.Separator, ")");
                if (this.TryParseName("do"))
                    expressions.Add(new BlockExpression(this.ParseBlock()));
                else if (this.TryParseToken(TokenType.Separator, "{"))
                    expressions.Add(new BlockExpression(this.ParseBlock(true)));
            }

            return expressions;
        }

        private IList<IExpression> ParseExpressionList(string separator)
        {
            IList<IExpression> expressions = new List<IExpression>();

            for (IExpression expression = this.ParseExpression(); expression != null; expression = this.ParseExpression())
            {
                expressions.Add(expression);
                if (!this.TryParseToken(TokenType.Separator, ","))
                    break;
            }

            this.ParseToken(TokenType.Separator, separator);

            return expressions;
        }

        private Block ParseBlock(bool usebraces = false)
        {
            if (this.TryParseToken(TokenType.Separator, "|"))
            {
                IList<string> paramnames = this.ParseParameterList(false);
                this.ParseToken(TokenType.Separator, "|");
                return new Block(paramnames, this.ParseCommandList(usebraces));
            }

            return new Block(null, this.ParseCommandList(usebraces));
        }

        private IExpression ParseCommandList(bool usebraces = false)
        {
            Token token;
            IList<IExpression> commands = new List<IExpression>();

            for (token = this.lexer.NextToken(); token != null; token = this.lexer.NextToken())
            {
                if (usebraces && token.Type == TokenType.Separator && token.Value == "}")
                    break;
                else if (!usebraces && token.Type == TokenType.Name && token.Value == "end")
                    break;

                if (this.IsEndOfCommand(token))
                    continue;

                this.lexer.PushToken(token);
                commands.Add(this.ParseCommand());
            }

            this.lexer.PushToken(token);

            if (usebraces)
                this.ParseToken(TokenType.Separator, "}");
            else
                this.ParseName("end");

            if (commands.Count == 1)
                return commands[0];

            return new CompositeExpression(commands);
        }

        private IExpression ParseCommandList(IList<string> names)
        {
            Token token;
            IList<IExpression> commands = new List<IExpression>();

            for (token = this.lexer.NextToken(); token != null && (token.Type != TokenType.Name || !names.Contains(token.Value)); token = this.lexer.NextToken())
            {
                if (this.IsEndOfCommand(token))
                    continue;

                this.lexer.PushToken(token);
                commands.Add(this.ParseCommand());
            }

            this.lexer.PushToken(token);

            if (commands.Count == 1)
                return commands[0];

            return new CompositeExpression(commands);
        }

        private void ParseEndOfCommand()
        {
            Token token = this.lexer.NextToken();

            if (token != null && token.Type == TokenType.Name && token.Value == "end")
            {
                this.lexer.PushToken(token);
                return;
            }

            if (token != null && token.Type == TokenType.Separator && token.Value == "}")
            {
                this.lexer.PushToken(token);
                return;
            }

            if (!this.IsEndOfCommand(token))
                throw new SyntaxError("end of command expected");
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

            if (token.Type == TokenType.Name && token.Value == "end")
                return false;

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
            IExpression expression = null;

            if (this.TryParseToken(TokenType.Operator, "-"))
                expression = new NegativeExpression(this.ParseTerm());
            else
                expression = this.ParseSimpleTerm();

            if (expression == null)
                return null;

            while (true)
            {
                if (this.TryParseToken(TokenType.Separator, "."))
                {
                    string name = this.ParseName();

                    if (this.TryParseToken(TokenType.Separator, "{"))
                        expression = new DotExpression(expression, name, new IExpression[] { new BlockExpression(this.ParseBlock(true)) });
                    else if (this.NextTokenStartsExpressionList())
                        expression = new DotExpression(expression, name, this.ParseExpressionList());
                    else
                        expression = new DotExpression(expression, name, new IExpression[0]);

                    continue;
                }

                if (this.TryParseToken(TokenType.Separator, "::"))
                {
                    string name = this.ParseName();

                    expression = new DoubleColonExpression(expression, name);

                    continue;
                }

                if (this.TryParseToken(TokenType.Separator, "["))
                {
                    IExpression indexexpr = this.ParseExpression();
                    this.ParseToken(TokenType.Separator, "]");
                    expression = new IndexedExpression(expression, indexexpr);

                    continue;
                }

                break;
            }

            return expression;
        }

        private IExpression ParseSimpleTerm()
        {
            Token token = this.lexer.NextToken();

            if (token == null)
                return null;

            if (token.Type == TokenType.Integer)
                return new ConstantExpression(int.Parse(token.Value, System.Globalization.CultureInfo.InvariantCulture));

            if (token.Type == TokenType.Real)
                return new ConstantExpression(double.Parse(token.Value, System.Globalization.CultureInfo.InvariantCulture));

            if (token.Type == TokenType.String)
                return new ConstantExpression(token.Value);

            if (token.Type == TokenType.Name)
            {
                if (token.Value == "false")
                    return new ConstantExpression(false);

                if (token.Value == "true")
                    return new ConstantExpression(true);

                if (token.Value == "nil")
                    return new ConstantExpression(null);

                if (token.Value == "do")
                    return new BlockExpression(this.ParseBlock());

                if (token.Value == "if")
                    return this.ParseIfExpression();

                if (token.Value == "while")
                    return this.ParseWhileExpression();

                if (token.Value == "until")
                    return this.ParseUntilExpression();

                if (token.Value == "for")
                    return this.ParseForInExpression();

                if (token.Value == "def")
                    return this.ParseDefExpression();

                if (token.Value == "class")
                    return this.ParseClassExpression();

                if (token.Value == "module")
                    return this.ParseModuleExpression();

                return new NameExpression(token.Value);
            }

            if (token.Type == TokenType.InstanceVarName)
                return new InstanceVarExpression(token.Value);

            if (token.Type == TokenType.ClassVarName)
                return new ClassVarExpression(token.Value);

            if (token.Type == TokenType.Symbol)
                return new ConstantExpression(new Symbol(token.Value));

            if (token.Type == TokenType.Separator && token.Value == "(")
            {
                IExpression expr = this.ParseExpression();
                this.ParseToken(TokenType.Separator, ")");
                return expr;
            }

            if (token.Type == TokenType.Separator && token.Value == "{")
                return this.ParseHashExpression();

            if (token.Type == TokenType.Separator && token.Value == "[")
            {
                IList<IExpression> expressions = this.ParseExpressionList("]");
                return new ListExpression(expressions);
            }

            throw new SyntaxError(string.Format("unexpected '{0}'", token.Value));
        }

        private HashExpression ParseHashExpression()
        {
            IList<IExpression> keyexpressions = new List<IExpression>();
            IList<IExpression> valueexpressions = new List<IExpression>();

            while (!this.TryParseToken(TokenType.Separator, "}")) 
            {
                if (keyexpressions.Count > 0)
                    this.ParseToken(TokenType.Separator, ",");

                var keyexpression = this.ParseExpression();
                this.ParseToken(TokenType.Operator, "=>");
                var valueexpression = this.ParseExpression();

                keyexpressions.Add(keyexpression);
                valueexpressions.Add(valueexpression);
            }

            return new HashExpression(keyexpressions, valueexpressions);
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
