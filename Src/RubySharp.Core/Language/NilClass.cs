namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NilClass : NativeClass
    {
        private static NilClass instance = new NilClass();

        public NilClass()
            : base("NilClass")
        {
        }

        public static NilClass Instance { get { return instance; } }
    }
}
