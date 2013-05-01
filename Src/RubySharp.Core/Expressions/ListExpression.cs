namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ListExpression : IExpression
    {
        private IList<IExpression> expressions;

        public ListExpression(IList<IExpression> expressions)
        {
            this.expressions = expressions;
        }

        public object Evaluate(Context context)
        {
            IList<object> result = new List<object>();

            foreach (var expr in this.expressions)
                result.Add(expr.Evaluate(context));

            return result;
        }
    }
}
