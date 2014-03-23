namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Language;

    public class RangeExpression : IExpression
    {
        private static int hashcode = typeof(RangeExpression).GetHashCode();

        private IExpression fromexpression;
        private IExpression toexpression;

        public RangeExpression(IExpression fromexpression, IExpression toexpression)
        {
            this.fromexpression = fromexpression;
            this.toexpression = toexpression;
        }

        public object Evaluate(Context context)
        {
            int from = (int)this.fromexpression.Evaluate(context);
            int to = (int)this.toexpression.Evaluate(context);
            return new Range(from, to);
        }

        public IList<string> GetLocalVariables()
        {
            var list = new List<IExpression>() { this.fromexpression, this.toexpression };
            return BaseExpression.GetLocalVariables(list);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is RangeExpression))
                return false;

            var rexpr = (RangeExpression)obj;

            return this.fromexpression.Equals(rexpr.fromexpression) && this.toexpression.Equals(rexpr.toexpression);
        }

        public override int GetHashCode()
        {
            return hashcode + this.fromexpression.GetHashCode() + (7 * this.toexpression.GetHashCode());
        }
    }
}
