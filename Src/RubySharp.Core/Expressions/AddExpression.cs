namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AddExpression : BinaryExpression
    {
        public AddExpression(IExpression left, IExpression right)
            : base(left, right)
        {
        }

        public override object Apply(object leftvalue, object rightvalue)
        {
            return (int)leftvalue + (int)rightvalue;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is AddExpression)
            {
                var expr = (AddExpression)obj;

                return this.LeftExpression.Equals(expr.LeftExpression) && this.RightExpression.Equals(expr.RightExpression);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.LeftExpression.GetHashCode() + this.RightExpression.GetHashCode() + 13;
        }
    }
}
