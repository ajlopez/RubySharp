namespace RubySharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CompositeCommand : ICommand
    {
        private IList<ICommand> commands;

        public CompositeCommand(IList<ICommand> commands)
        {
            this.commands = commands;
        }

        public object Execute(Context context)
        {
            object result = null;

            foreach (var command in this.commands)
                result = command.Execute(context);

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
