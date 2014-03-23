namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AssignExpression : IExpression
    {
        private static int hashtag = typeof(AssignExpression).GetHashCode();

        private string name;
        private IExpression expression;

        public AssignExpression(string name, IExpression expression)
        {
            this.name = name;
            this.expression = expression;
        }

        public string Name { get { return this.name; } }

        public IExpression Expression { get { return this.expression; } }

        public object Evaluate(Context context)
        {
            object value = this.expression.Evaluate(context);
            context.SetLocalValue(this.name, value);

            if (char.IsUpper(this.name[0]) && context.Module != null)
                context.Module.Constants.SetLocalValue(this.name, value);

            return value;
        }

        public IList<string> GetLocalVariables()
        {
            return new string[] { this.name };
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is AssignExpression)
            {
                var cmd = (AssignExpression)obj;

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
