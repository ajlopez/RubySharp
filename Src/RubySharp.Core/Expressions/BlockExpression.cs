namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Language;

    public class BlockExpression : BaseExpression
    {
        private IList<string> paramnames;
        private IExpression expression;

        public BlockExpression(IList<string> paramnames, IExpression expression)
        {
            this.paramnames = paramnames;
            this.expression = expression;
        }

        public override object Evaluate(Context context)
        {
            return new Block(this.paramnames, this.expression, context);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is BlockExpression))
                return false;

            var bexpr = (BlockExpression)obj;

            if (this.paramnames == null && bexpr.paramnames != null)
                return false;

            if (this.paramnames != null && bexpr.paramnames == null)
                return false;

            if (this.paramnames != null && this.paramnames.Count != bexpr.paramnames.Count)
                return false;

            if (!this.expression.Equals(bexpr.expression))
                return false;

            if (this.paramnames != null)
                for (int k = 0; k < this.paramnames.Count; k++)
                    if (!this.paramnames[k].Equals(bexpr.paramnames[k]))
                        return false;

            return true;
        }

        public override int GetHashCode()
        {
            int result = typeof(BlockExpression).GetHashCode() + this.expression.GetHashCode();

            foreach (var paramname in this.paramnames) 
            {
                result *= 7;
                result += paramname.GetHashCode();
            }

            return result;
        }
    }
}

