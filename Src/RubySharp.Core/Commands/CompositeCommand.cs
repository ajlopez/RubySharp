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

            foreach (var command in commands)
                result = command.Execute(context);

            return result;
        }
    }
}
