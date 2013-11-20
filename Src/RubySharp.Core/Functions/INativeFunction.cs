namespace RubySharp.Core.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Language;

    public interface INativeFunction
    {
        object Apply(object self, IList<object> values);
    }
}
