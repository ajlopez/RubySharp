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
            if (this.position < this.text.Length)
            {
                this.position = this.text.Length;
                return new Token(this.text);
            }

            return null;
        }
    }
}
