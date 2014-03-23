namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Expressions;

    public class AssignInstanceVarExpression : BaseExpression
    {
        private static int hashtag = typeof(AssignInstanceVarExpression).GetHashCode();

        private string name;
        private IExpression expression;

        public AssignInstanceVarExpression(string name, IExpression expression)
        {
            this.name = name;
            this.expression = expression;
        }

        public string Name { get { return this.name; } }

        public IExpression Expression { get { return this.expression; } }

        public override object Evaluate(Context context)
        {
            object value = this.expression.Evaluate(context);
            context.Self.SetValue(this.name, value);
            return value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is AssignInstanceVarExpression)
            {
                var cmd = (AssignInstanceVarExpression)obj;

                return this.Name.Equals(cmd.name) && this.Expression.Equals(cmd.Expression);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode() + this.Expression.GetHashCode() + hashtag;
        }
    }
}
