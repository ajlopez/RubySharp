﻿namespace RubySharp.Core.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IFunction
    {
        object Apply(IList<object> values);
    }
}
