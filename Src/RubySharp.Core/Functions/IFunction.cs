namespace RubySharp.Core.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Language;

    public interface IFunction
    {
        object Apply(DynamicObject self, Context context, IList<object> values);
    }
}
