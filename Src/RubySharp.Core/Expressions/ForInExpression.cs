namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Expressions;

    public class ForInExpression : IExpression
    {
        private static int hashcode = typeof(ForInExpression).GetHashCode();

        private string name;
        private IExpression expression;
        private IExpression command;

        public ForInExpression(string name, IExpression expression, IExpression command)
        {
            this.name = name;
            this.expression = expression;
            this.command = command;
        }

        public object Evaluate(Context context)
        {
            IEnumerable elements = (IEnumerable)this.expression.Evaluate(context);

            foreach (var element in elements) 
            {
                context.SetLocalValue(this.name, element);
                this.command.Evaluate(context);
            }

            return null;
        }

        public IList<string> GetLocalVariables()
        {
            var list = new List<IExpression>() { new AssignExpression(this.name, null), this.expression, this.command };

            return BaseExpression.GetLocalVariables(list);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is ForInExpression)
            {
                var cmd = (ForInExpression)obj;
                if (!this.name.Equals(cmd.name))
                    return false;
                if (!this.expression.Equals(cmd.expression))
                    return false;
                return this.command.Equals(cmd.command);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return hashcode + this.name.GetHashCode() + this.expression.GetHashCode() + this.command.GetHashCode();
        }
    }
}
