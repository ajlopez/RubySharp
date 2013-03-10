namespace RubySharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
        private const char Quote = '\'';
        private const string Operators = "+-*/=";
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

            if (ch == '\n')
                return new Token(TokenType.EndOfLine, "\n");

            if (ch == Quote)
                return this.NextString();

            if (Operators.Contains(ch))
                return new Token(TokenType.Operator, ch.ToString());

            if (char.IsDigit(ch))
                return this.NextInteger(ch);

            return this.NextName(ch);
        }

        public void PushToken(Token token)
        {
            this.tokens.Push(token);
        }

        private Token NextName(char ch)
        {
            string value = ch.ToString();

            for (int ich = this.NextChar(); ich >= 0 && char.IsLetterOrDigit((char)ich); ich = this.NextChar())
                value += (char)ich;

            this.BackChar();

            return new Token(TokenType.Name, value);
        }

        private Token NextString()
        {
            string value = string.Empty;
            int ich;

            for (ich = this.NextChar(); ich >= 0 && ((char)ich) != Quote; ich = this.NextChar())
                value += (char)ich;

            if (ich < 0)
                throw new ParserException("single quote expected");

            return new Token(TokenType.String, value);
        }

        private Token NextInteger(char ch)
        {
            string value = ch.ToString();

            for (int ich = this.NextChar(); ich >= 0 && char.IsDigit((char)ich); ich = this.NextChar())
                value += (char)ich;

            this.BackChar();

            return new Token(TokenType.Integer, value);
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

            return this.text[this.position++];
        }

        private void BackChar()
        {
            if (this.position < this.text.Length)
                this.position--;
        }
    }
}
