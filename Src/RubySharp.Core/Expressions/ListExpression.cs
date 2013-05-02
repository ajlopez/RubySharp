namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ListExpression : IExpression
    {
        private static int hashcode = typeof(DotExpression).GetHashCode();

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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is ListExpression)
            {
                var expr = (ListExpression)obj;

                if (this.expressions.Count != expr.expressions.Count)
                    return false;

                for (int k = 0; k < this.expressions.Count; k++)
                    if (!this.expressions[k].Equals(expr.expressions[k]))
                        return false;

                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int result = hashcode;

            foreach (var expr in this.expressions)
            {
                result += expr.GetHashCode();
                result *= 3;
            }

            return result;
        }
    }
}
