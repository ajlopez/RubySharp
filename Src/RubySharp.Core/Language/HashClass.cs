namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class HashClass : NativeClass
    {
        public HashClass(Machine machine)
            : base("Hash", machine)
        {
        }
    }
}
