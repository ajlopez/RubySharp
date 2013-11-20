namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class StringClass : NativeClass
    {
        private static StringClass instance = new StringClass();

        public StringClass()
            : base("String")
        {
        }

        public static StringClass Instance { get { return instance; } }
    }
}
