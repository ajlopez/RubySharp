namespace RubySharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Expressions;

    public class IfCommand : IExpression
    {
        private static int hashcode = typeof(IfCommand).GetHashCode();

        private IExpression condition;
        private IExpression thencommand;
        private IExpression elsecommand;

        public IfCommand(IExpression condition, IExpression thencommand)
            : this(condition, thencommand, null)
        {
        }

        public IfCommand(IExpression condition, IExpression thencommand, IExpression elsecommand)
        {
            this.condition = condition;
            this.thencommand = thencommand;
            this.elsecommand = elsecommand;
        }

        public object Evaluate(Context context)
        {
            object value = this.condition.Evaluate(context);

            if (value == null || false.Equals(value))
                if (this.elsecommand == null)
                    return null;
                else
                    return this.elsecommand.Evaluate(context);

            return this.thencommand.Evaluate(context);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is IfCommand)
            {
                var cmd = (IfCommand)obj;

                if (this.elsecommand == null)
                {
                    if (cmd.elsecommand != null)
                        return false;
                }
                else if (!this.elsecommand.Equals(cmd.elsecommand))
                    return false;

                return this.condition.Equals(cmd.condition) && this.thencommand.Equals(cmd.thencommand);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.condition.GetHashCode() + this.thencommand.GetHashCode() + hashcode;
        }
    }
}
