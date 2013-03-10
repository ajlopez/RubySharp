namespace RubySharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Expressions;

    public class IfCommand : ICommand
    {
        private IExpression condition;
        private ICommand thencommand;

        public IfCommand(IExpression condition, ICommand thencommand)
        {
            this.condition = condition;
            this.thencommand = thencommand;
        }

        public IExpression Condition { get { return this.condition; } }

        public ICommand ThenCommand { get { return this.thencommand; } }

        public object Execute(Context context)
        {
            object value = this.condition.Evaluate(context);

            if (value == null || false.Equals(value))
                return null;

            return this.thencommand.Execute(context);
        }
    }
}
