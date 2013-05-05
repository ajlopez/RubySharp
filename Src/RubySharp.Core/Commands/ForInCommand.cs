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
    }
}
