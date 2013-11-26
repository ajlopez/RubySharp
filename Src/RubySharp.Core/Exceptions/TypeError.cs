namespace RubySharp.Core.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TypeError : Exception
    {
        public TypeError(string msg)
            : base(msg)
        {
        }
    }
}
