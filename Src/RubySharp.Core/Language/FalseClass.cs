namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class FalseClass : NativeClass
    {
        private static FalseClass instance = new FalseClass();

        public FalseClass()
            : base("FalseClass")
        {
        }

        public static FalseClass Instance { get { return instance; } }
    }
}
