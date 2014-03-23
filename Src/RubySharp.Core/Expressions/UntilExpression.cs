namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class UntilExpression : IExpression
    {
        private static int hashcode = typeof(UntilExpression).GetHashCode();

        private IExpression condition;
        private IExpression command;

        public UntilExpression(IExpression condition, IExpression command)
        {
            this.condition = condition;
            this.command = command;
        }

        public object Evaluate(Context context)
        {
            for (object value = this.condition.Evaluate(context); value == null || false.Equals(value); value = this.condition.Evaluate(context))
                this.command.Evaluate(context);

            return null;
        }

        public IList<string> GetLocalVariables()
        {
            var list = new List<IExpression>() { this.condition, this.command };

            return BaseExpression.GetLocalVariables(list);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is UntilExpression)
            {
                var cmd = (UntilExpression)obj;

                return this.condition.Equals(cmd.condition) && this.command.Equals(cmd.command);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.condition.GetHashCode() + this.command.GetHashCode() + hashcode;
        }
    }
}
