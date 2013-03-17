namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Language;

    public class DotExpression : IExpression
    {
        private IExpression expression;
        private string name;

        public DotExpression(IExpression expression, string name)
        {
            this.expression = expression;
            this.name = name;
        }

        public object Evaluate(Context context)
        {
            var obj = (BaseObject)this.expression.Evaluate(context);
            return obj.GetMethod(this.name).Apply(null);
        }
    }
}
