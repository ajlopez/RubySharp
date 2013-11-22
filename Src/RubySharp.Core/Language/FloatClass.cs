namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class FloatClass : NativeClass
    {
        private static FloatClass instance = new FloatClass();

        public FloatClass()
            : base("Float")
        {
        }

        public static FloatClass Instance { get { return instance; } }
    }
}
