namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class TrueClass : NativeClass
    {
        private static TrueClass instance = new TrueClass();

        public TrueClass()
            : base("TrueClass")
        {
        }

        public static TrueClass Instance { get { return instance; } }
    }
}
