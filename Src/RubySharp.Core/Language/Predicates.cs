namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class Predicates
    {
        public static bool IsFalse(object obj)
        {
            return obj == null || false.Equals(obj);
        }

        public static bool IsTrue(object obj)
        {
            return !IsFalse(obj);
        }

        public static bool IsConstantName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return false;

            return char.IsUpper(name[0]);
        }
    }
}
