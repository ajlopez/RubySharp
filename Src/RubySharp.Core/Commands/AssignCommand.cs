namespace RubySharp.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Expressions;

    public class AssignCommand
    {
        private string name;
        private IExpression expression;

        public AssignCommand(string name, IExpression expression)
        {
            this.name = name;
            this.expression = expression;
        }

        public string Name { get { return this.name; } }

        public IExpression Expression { get { return this.expression; } }

        public void Execute(Context context)
        {
            context.SetValue(this.name, this.expression.Evaluate(context));
        }
    }
}
