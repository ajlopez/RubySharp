namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NegationExpression : IExpression
    {
        private static int hashcode = typeof(NegationExpression).GetHashCode();

        private IExpression expression;

        public NegationExpression(IExpression expression)
        {
            this.expression = expression;
        }

        public IExpression Expression { get { return this.expression; } }

        public object Evaluate(Context context)
        {
            var value = this.expression.Evaluate(context);

            if (value == null || false.Equals(value))
                return true;

            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is NegationExpression)
            {
                var expr = (NegationExpression)obj;

                return this.expression.Equals(expr.expression);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.Expression.GetHashCode() + hashcode;
        }
    }
}
