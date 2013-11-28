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

        public IExpression Expression { get { return this.expression; } }

        public IExpression IndexExpression { get { return this.indexexpression; } }

        public object Evaluate(Context context)
        {
            object value = this.expression.Evaluate(context);
            object indexvalue = this.indexexpression.Evaluate(context);

            if (indexvalue is int)
            {
                int index = (int)indexvalue;

                if (value is string)
                {
                    string text = (string)value;

                    if (index >= text.Length)
                        return null;

                    if (index < 0)
                    {
                        index = text.Length + index;

                        if (index < 0)
                            return null;
                    }

                    return text[index].ToString();
                }

                var list = (IList)this.expression.Evaluate(context);

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

            var dict = (IDictionary)value;

            if (dict.Contains(indexvalue))
                return dict[indexvalue];

            return null;
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
