namespace RubySharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Lexer
    {
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

            string value = ((char)ich).ToString();

            for (ich = this.NextChar(); ich >= 0 && !char.IsWhiteSpace((char)ich); ich = this.NextChar())
                value += (char)ich;

            return new Token(value);
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
    }
}
