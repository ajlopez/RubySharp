namespace RubySharp.Core.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Token
    {
        private string value;

        public Token(string value)
        {
            this.value = value;
        }

        public string Value { get { return this.value; } }
    }
}
