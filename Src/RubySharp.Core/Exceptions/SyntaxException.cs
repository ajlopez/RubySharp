namespace RubySharp.Core.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SyntaxException : Exception
    {
        public SyntaxException(string message)
            : base(message)
        {
        }
    }
}
