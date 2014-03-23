namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class BinaryExpression : BaseExpression
    {
        private IExpression left;
        private IExpression right;

        public BinaryExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public IExpression LeftExpression { get { return this.left; } }

        public IExpression RightExpression { get { return this.right; } }

        public override object Evaluate(Context context)
        {
            var lvalue = this.left.Evaluate(context);
            var rvalue = this.right.Evaluate(context);

            return this.Apply(lvalue, rvalue);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() == this.GetType())
            {
                var expr = (BinaryExpression)obj;

                return this.left.Equals(expr.left) && this.right.Equals(expr.right);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.LeftExpression.GetHashCode() + this.RightExpression.GetHashCode() + this.GetType().GetHashCode();
        }

        public abstract object Apply(object leftvalue, object rightvalue);
    }
}
