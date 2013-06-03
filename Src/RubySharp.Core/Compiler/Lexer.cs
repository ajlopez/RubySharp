namespace RubySharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Exceptions;

    public class Lexer
    {
        private const char Quote = '\'';
        private const char Colon = ':';
        private const char StartComment = '#';
        private const char EndOfLine = '\n';
        private const char Variable = '@';

        private const string Separators = ";()[],.";

        private static string[] operators = new string[] { "+", "-", "*", "/", "=", "<", ">", "!", "==", "<=", ">=", "!=" };

        private string text;
        private int position = 0;
        private Stack<Token> tokens = new Stack<Token>();

        public Lexer(string text)
        {
            this.text = text;
        }

        public Token NextToken()
        {
            if (this.tokens.Count > 0)
                return this.tokens.Pop();

            int ich = this.NextFirstChar();

            if (ich == -1)
                return null;

            char ch = (char)ich;

            if (ch == EndOfLine)
                return new Token(TokenType.EndOfLine, "\n");

            if (ch == Quote)
                return this.NextString();

            if (ch == Colon)
                return this.NextSymbol();

            if (ch == Variable)
                return this.NextInstanceVariableName();

            if (operators.Contains(ch.ToString()))
            {
                string value = ch.ToString();
                ich = this.NextChar();

                if (ich >= 0)
                {
                    value += (char)ich;
                    if (operators.Contains(value))
                        return new Token(TokenType.Operator, value);

                    this.BackChar();
                }

                return new Token(TokenType.Operator, ch.ToString());
            }

            if (Separators.Contains(ch))
                return new Token(TokenType.Separator, ch.ToString());

            if (char.IsDigit(ch))
                return this.NextInteger(ch);

            if (char.IsLetter(ch) || ch == '_')
                return this.NextName(ch);

            throw new SyntaxError(string.Format("unexpected '{0}'", ch));
        }

        public void PushToken(Token token)
        {
            this.tokens.Push(token);
        }

        private Token NextName(char ch)
        {
            string value = ch.ToString();
            int ich;

            for (ich = this.NextChar(); ich >= 0 && ((char)ich == '_' || char.IsLetterOrDigit((char)ich)); ich = this.NextChar())
                value += (char)ich;

            if (ich >= 0)
                this.BackChar();

            return new Token(TokenType.Name, value);
        }

        private Token NextInstanceVariableName()
        {
            string value = string.Empty;
            int ich;

            for (ich = this.NextChar(); ich >= 0 && ((char)ich == '_' || char.IsLetterOrDigit((char)ich)); ich = this.NextChar())
                value += (char)ich;

            if (ich >= 0)
                this.BackChar();

            if (string.IsNullOrEmpty(value) || char.IsDigit(value[0]))
                throw new SyntaxError("invalid instance variable name");

            return new Token(TokenType.InstanceVarName, value);
        }

        private Token NextSymbol()
        {
            string value = string.Empty;
            int ich;

            for (ich = this.NextChar(); ich >= 0 && ((char)ich == '_' || char.IsLetterOrDigit((char)ich)); ich = this.NextChar())
            {
                if (char.IsDigit((char)ich) && string.IsNullOrEmpty(value))
                    throw new SyntaxError("unexpected integer");

                value += (char)ich;
            }

            if (ich >= 0)
                this.BackChar();

            return new Token(TokenType.Symbol, value);
        }

        private Token NextString()
        {
            string value = string.Empty;
            int ich;

            for (ich = this.NextChar(); ich >= 0 && ((char)ich) != Quote; ich = this.NextChar())
                value += (char)ich;

            if (ich < 0)
                throw new SyntaxError("single quote expected");

            return new Token(TokenType.String, value);
        }

        private Token NextInteger(char ch)
        {
            string value = ch.ToString();
            int ich;

            for (ich = this.NextChar(); ich >= 0 && char.IsDigit((char)ich); ich = this.NextChar())
                value += (char)ich;

            if (ich >= 0 && (char)ich == '.')
                return this.NextReal(value);

            if (ich >= 0)
                this.BackChar();

            return new Token(TokenType.Integer, value);
        }

        private Token NextReal(string ivalue)
        {
            string value = ivalue + ".";
            int ich;

            for (ich = this.NextChar(); ich >= 0 && char.IsDigit((char)ich); ich = this.NextChar())
                value += (char)ich;

            if (ich >= 0)
                this.BackChar();

            return new Token(TokenType.Real, value);
        }

        private int NextFirstChar()
        {
            while (this.position < this.text.Length && this.text[this.position] != '\n' && char.IsWhiteSpace(this.text[this.position]))
                this.position++;

            return this.NextChar();
        }

        private int NextChar()
        {
            if (this.position >= this.text.Length)
                return -1;

            char ch = this.text[this.position++];

            if (ch == StartComment)
            {
                this.position++;

                while (this.position < this.text.Length && this.text[this.position] != '\n')
                    this.position++;

                if (this.position >= this.text.Length)
                    return -1;

                ch = this.text[this.position++];
            }

            return ch;
        }

        private void BackChar()
        {
            if (this.position <= this.text.Length)
                this.position--;
        }
    }
}
