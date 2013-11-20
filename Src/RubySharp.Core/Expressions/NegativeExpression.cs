namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class NegativeExpression : IExpression
    {
        private static int hashcode = typeof(NegativeExpression).GetHashCode();

        private IExpression expression;

        public NegativeExpression(IExpression expression)
        {
            this.expression = expression;
        }

        public IExpression Expression { get { return this.expression; } }

        public object Evaluate(Context context)
        {
            var value = this.expression.Evaluate(context);
            return -(int)value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is NegativeExpression)
            {
                var expr = (NegativeExpression)obj;

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
