namespace RubySharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;

    public class ClassCommand : ICommand
    {
        private static int hashcode = typeof(ClassCommand).GetHashCode();
        private string name;
        private ICommand command;

        public ClassCommand(string name, ICommand command)
        {
            this.name = name;
            this.command = command;
        }

        public object Execute(Context context)
        {
            var value = context.GetValue(this.name);

            if (value == null || !(value is DefinedClass))
            {
                var newclass = new DefinedClass(this.name);
                context.SetValue(this.name, newclass);
                value = newclass;
            }

            var dclass = (DefinedClass)value;

            Context classcontext = new Context(dclass, context);

            this.command.Execute(classcontext);

            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is ClassCommand)
            {
                var cmd = (ClassCommand)obj;

                return this.name == cmd.name && this.command.Equals(cmd.command);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.name.GetHashCode() + this.command.GetHashCode() + hashcode;
        }
    }
}
