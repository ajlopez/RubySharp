namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ConstantExpression : IExpression
    {
        private object value;

        public ConstantExpression(object value)
        {
            this.value = value;
        }

        public object Value { get { return this.value; } }

        public object Evaluate(Context context)
        {
            return this.value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is ConstantExpression)
            {
                var expr = (ConstantExpression)obj;

                if (this.value == null)
                    return expr.value == null;

                return this.Value.Equals(expr.Value);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (this.value == null)
                return 0;

            return this.value.GetHashCode();
        }
    }
}
