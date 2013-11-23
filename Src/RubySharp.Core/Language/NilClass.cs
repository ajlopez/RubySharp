namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NilClass : NativeClass
    {
        public NilClass(Machine machine)
            : base("NilClass", machine)
        {
        }
    }
}
