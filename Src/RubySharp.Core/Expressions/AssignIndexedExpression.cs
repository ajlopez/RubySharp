namespace RubySharp.Core.Expressions
{
    using System.Collections;
    using System.Collections.Generic;
    using RubySharp.Core.Exceptions;
    using RubySharp.Core.Language;

    public class AssignIndexedExpression : IExpression
    {
        private static int hashcode = typeof(IndexedExpression).GetHashCode();
        private IExpression leftexpression;
        private IExpression indexexpression;
        private IExpression expression;

        public AssignIndexedExpression(IExpression leftexpression, IExpression indexexpression, IExpression expression)
        {
            this.leftexpression = leftexpression;
            this.indexexpression = indexexpression;
            this.expression = expression;
        }

        public object Evaluate(Context context)
        {
            var leftvalue = (IList)this.leftexpression.Evaluate(context);
            int index = (int)this.indexexpression.Evaluate(context);
            var newvalue = this.expression.Evaluate(context);

            leftvalue[index] = newvalue;

            return newvalue;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is AssignIndexedExpression)
            {
                var expr = (AssignIndexedExpression)obj;

                return this.leftexpression.Equals(expr.leftexpression) && this.expression.Equals(expr.expression) && this.indexexpression.Equals(expr.indexexpression);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.leftexpression.GetHashCode() + this.expression.GetHashCode() + this.indexexpression.GetHashCode() + hashcode;
        }
    }
}
