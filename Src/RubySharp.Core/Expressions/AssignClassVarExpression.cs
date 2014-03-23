namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Expressions;

    public class AssignClassVarExpression : BaseExpression
    {
        private static int hashtag = typeof(AssignClassVarExpression).GetHashCode();

        private string name;
        private IExpression expression;

        public AssignClassVarExpression(string name, IExpression expression)
        {
            this.name = name;
            this.expression = expression;
        }

        public string Name { get { return this.name; } }

        public IExpression Expression { get { return this.expression; } }

        public override object Evaluate(Context context)
        {
            object value = this.expression.Evaluate(context);
            context.Self.Class.SetValue(this.name, value);
            return value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is AssignClassVarExpression)
            {
                var cmd = (AssignClassVarExpression)obj;

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
