namespace RubySharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Language;

    public class AssignDotCommand : IExpression
    {
        private DotExpression leftvalue;
        private IExpression expression;

        public AssignDotCommand(DotExpression leftvalue, IExpression expression)
        {
            this.leftvalue = leftvalue;
            this.expression = expression;
        }

        public DotExpression LeftValue { get { return this.leftvalue; } }

        public IExpression Expression { get { return this.expression; } }

        public object Evaluate(Context context)
        {
            var obj = (BaseObject)this.leftvalue.Expression.Evaluate(context);
            var method = obj.GetMethod(this.leftvalue.Name + "=");
            return method.Apply(obj, new object[] { this.expression.Evaluate(context) });
        }
    }
}
