namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IExpression
    {
        object Evaluate(Context context);
    }
}
