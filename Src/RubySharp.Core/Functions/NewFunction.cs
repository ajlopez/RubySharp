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
            var obj = ((DefinedClass)self).CreateInstance();

            var initialize = obj.GetMethod("initialize");

            if (initialize != null)
                initialize.Apply(obj, values);

            return obj;
        }
    }
}
