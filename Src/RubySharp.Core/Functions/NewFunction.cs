namespace RubySharp.Core.Functions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Language;

    public class NewFunction : IFunction
    {
        public object Apply(BaseObject self, IList<object> values)
        {
            return ((DefinedClass)self).CreateInstance();
        }
    }
}
