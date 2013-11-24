namespace RubySharp.Core.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ValueError : Exception
    {
        public ValueError(string msg)
            : base(msg)
        {
        }
    }
}
