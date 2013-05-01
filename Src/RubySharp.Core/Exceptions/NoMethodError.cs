namespace RubySharp.Core.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NoMethodError : Exception
    {
        public NoMethodError(string mthname)
            : base(string.Format("undefined method '{0}'", mthname))
        {
        }
    }
}
