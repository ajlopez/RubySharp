namespace RubySharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Expressions;

    public class WhileCommand : IExpression
    {
        private static int hashcode = typeof(WhileCommand).GetHashCode();

        private IExpression condition;
        private IExpression command;

        public WhileCommand(IExpression condition, IExpression command)
        {
            this.condition = condition;
            this.command = command;
        }

        public object Evaluate(Context context)
        {
            for (object value = this.condition.Evaluate(context); value != null && !false.Equals(value); value = this.condition.Evaluate(context))
                this.command.Evaluate(context);

            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is WhileCommand)
            {
                var cmd = (WhileCommand)obj;

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
