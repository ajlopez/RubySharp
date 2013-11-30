namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;

    public class HashExpression : IExpression
    {
        private static int hashtag = typeof(HashExpression).GetHashCode();

        private IList<IExpression> keyexpressions;
        private IList<IExpression> valueexpressions;

        public HashExpression(IList<IExpression> keyexpressions, IList<IExpression> valueexpressions)
        {
            this.keyexpressions = keyexpressions;
            this.valueexpressions = valueexpressions;
        }

        public object Evaluate(Context context)
        {
            IDictionary result = new DynamicHash();

            for (var k = 0; k < this.keyexpressions.Count; k++)
                result[this.keyexpressions[k].Evaluate(context)] = this.valueexpressions[k].Evaluate(context);

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is HashExpression)
            {
                var expr = (HashExpression)obj;

                if (this.keyexpressions.Count != expr.keyexpressions.Count)
                    return false;

                if (this.valueexpressions.Count != expr.valueexpressions.Count)
                    return false;

                for (var k = 0; k < this.keyexpressions.Count; k++)
                    if (!this.keyexpressions[k].Equals(expr.keyexpressions[k]))
                        return false;

                for (var k = 0; k < this.valueexpressions.Count; k++)
                    if (!this.valueexpressions[k].Equals(expr.valueexpressions[k]))
                        return false;

                return true;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            int result = hashtag;

            foreach (var expr in this.keyexpressions)
            {
                result *= 17;
                result += expr.GetHashCode();
            }

            foreach (var expr in this.valueexpressions)
            {
                result *= 7;
                result += expr.GetHashCode();
            }

            return result;
        }
    }
}
