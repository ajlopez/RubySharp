namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TrueClass : NativeClass
    {
        public TrueClass(Machine machine)
            : base("TrueClass", machine)
        {
        }
    }
}
