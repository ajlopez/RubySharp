namespace RubySharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;

    public class DefCommand : ICommand
    {
        private string name;
        private ICommand command;

        public DefCommand(string name, ICommand command)
        {
            this.name = name;
            this.command = command;
        }

        public object Execute(Context context)
        {
            var result = new DefinedFunction(this.command, context);
            context.SetValue(this.name, result);
            return result;
        }
    }
}
