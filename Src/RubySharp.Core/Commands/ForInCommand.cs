namespace RubySharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Expressions;
    using System.Collections;

    public class ForInCommand : ICommand
    {
        private static int hashcode = typeof(ForInCommand).GetHashCode();

        private string name;
        private IExpression expression;
        private ICommand command;

        public ForInCommand(string name,IExpression expression, ICommand command)
        {
            this.name = name;
            this.expression = expression;
            this.command = command;
        }

        public object Execute(Context context)
        {
            IEnumerable elements = (IEnumerable)this.expression.Evaluate(context);

            foreach (var element in elements) 
            {
                context.SetValue(this.name, element);
                this.command.Execute(context);
            }

            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is ForInCommand)
            {
                var cmd = (ForInCommand)obj;
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
