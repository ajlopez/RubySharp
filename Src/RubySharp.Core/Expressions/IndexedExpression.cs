namespace RubySharp.Core.Expressions
{
    using System.Collections.Generic;
    using RubySharp.Core.Exceptions;
    using RubySharp.Core.Language;
    using System.Collections;

    public class IndexedExpression : IExpression
    {
        private static int hashcode = typeof(IndexedExpression).GetHashCode();
        private IExpression expression;
        private IExpression indexexpression;

        public IndexedExpression(IExpression expression, IExpression indexexpression)
        {
            this.expression = expression;
            this.indexexpression = indexexpression;
        }

        public object Evaluate(Context context)
        {
            var list = (IList)this.expression.Evaluate(context);
            int index = (int)this.indexexpression.Evaluate(context);

            if (index >= list.Count)
                return null;

            if (index < 0)
            {
                index = list.Count + index;

                if (index < 0)
                    return null;
            }

            return list[index];
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is IndexedExpression)
            {
                var expr = (IndexedExpression)obj;

                return this.expression.Equals(expr.expression) && this.indexexpression.Equals(expr.indexexpression);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.expression.GetHashCode() + this.indexexpression.GetHashCode() + hashcode;
        }
    }
}
