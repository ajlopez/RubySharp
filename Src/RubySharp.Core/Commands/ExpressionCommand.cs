namespace RubySharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Expressions;

    public class ExpressionCommand : ICommand
    {
        private IExpression expression;

        public ExpressionCommand(IExpression expression)
        {
            this.expression = expression;
        }

        public IExpression Expression { get { return this.expression; } }

        public object Execute(Context context)
        {
            return this.expression.Evaluate(context);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is ExpressionCommand)
            {
                var expr = (ExpressionCommand)obj;

                return this.Expression.Equals(expr.Expression);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.Expression.GetHashCode();
        }
    }
}
