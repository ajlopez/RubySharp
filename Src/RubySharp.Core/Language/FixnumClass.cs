namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class FixnumClass : NativeClass
    {
        private static FixnumClass instance = new FixnumClass();

        public FixnumClass()
            : base("Fixnum")
        {
        }

        public static FixnumClass Instance { get { return instance; } }
    }
}
