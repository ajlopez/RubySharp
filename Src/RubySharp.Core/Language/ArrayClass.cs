namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ArrayClass : NativeClass
    {
        public ArrayClass(Machine machine)
            : base("Array", machine)
        {
        }
    }
}
