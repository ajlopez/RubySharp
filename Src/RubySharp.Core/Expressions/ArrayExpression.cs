namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Language;

    public class ArrayExpression : BaseExpression
    {
        private static int hashcode = typeof(DotExpression).GetHashCode();

        private IList<IExpression> expressions;

        public ArrayExpression(IList<IExpression> expressions)
        {
            this.expressions = expressions;
        }

        public override object Evaluate(Context context)
        {
            IList result = new DynamicArray();

            foreach (var expr in this.expressions)
                result.Add(expr.Evaluate(context));

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is ArrayExpression)
            {
                var expr = (ArrayExpression)obj;

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
