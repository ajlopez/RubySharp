namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class SubtractExpression : IExpression
    {
        private IExpression left;
        private IExpression right;

        public SubtractExpression(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public IExpression LeftExpression { get { return this.left; } }

        public IExpression RightExpression { get { return this.right; } }

        public object Evaluate(Context context)
        {
            int lvalue = (int)this.left.Evaluate(context);
            int rvalue = (int)this.right.Evaluate(context);

            return lvalue - rvalue;
        }
    }
}
