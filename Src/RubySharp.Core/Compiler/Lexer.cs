namespace RubySharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
        private const string Operators = "+-*/";
        private string text;
        private int position = 0;

        public Lexer(string text)
        {
            this.text = text;
        }

        public Token NextToken()
        {
            int ich = this.NextFirstChar();

            if (ich == -1)
                return null;

            char ch = (char)ich;

            if (Operators.Contains(ch))
                return new Token(TokenType.Operator, ch.ToString());

            if (char.IsDigit(ch))
                return this.NextInteger(ch);

            return this.NextName(ch);
        }

        private Token NextName(char ch)
        {
            string value = ch.ToString();

            for (int ich = this.NextChar(); ich >= 0 && char.IsLetterOrDigit((char)ich); ich = this.NextChar())
                value += (char)ich;

            this.BackChar();

            return new Token(TokenType.Name, value);
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
            while (this.position < this.text.Length && char.IsWhiteSpace(this.text[this.position]))
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
