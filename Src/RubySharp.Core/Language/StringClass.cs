namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StringClass : NativeClass
    {
        public StringClass(Machine machine)
            : base("String", machine)
        {
        }
    }
}
