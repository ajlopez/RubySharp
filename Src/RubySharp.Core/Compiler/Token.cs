namespace RubySharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Token
    {
        private string value;
        private TokenType type;

        public Token(TokenType type, string value)
        {
            this.type = type;
            this.value = value;
        }

        public string Value { get { return this.value; } }

        public TokenType Type { get { return this.type; } }
    }
}
