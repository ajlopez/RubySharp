namespace RubySharp.Core.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Language;

    public class NativeClassFunction : INativeFunction
    {
        public object Apply(object self, IList<object> values)
        {
            if (self is int)
                return FixnumClass.Instance;

            if (self is string)
                return StringClass.Instance;

            throw new NotImplementedException();
        }
    }
}
