namespace RubySharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Expressions;

    public class CompositeCommand : IExpression
    {
        private IList<IExpression> commands;

        public CompositeCommand(IList<IExpression> commands)
        {
            this.commands = commands;
        }

        public object Evaluate(Context context)
        {
            object result = null;

            foreach (var command in this.commands)
                result = command.Evaluate(context);

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is CompositeCommand)
            {
                var cmd = (CompositeCommand)obj;

                if (this.commands.Count != cmd.commands.Count)
                    return false;

                for (int k = 0; k < this.commands.Count; k++)
                    if (!this.commands[k].Equals(cmd.commands[k]))
                        return false;

                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int result = 0;

            foreach (var command in this.commands)
            {
                result *= 17;
                result += command.GetHashCode();
            }

            return result;
        }
    }
}
