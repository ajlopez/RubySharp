namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;

    public class CallExpression : IExpression
    {
        private string name;
        private IList<IExpression> arguments;

        public CallExpression(string name, IList<IExpression> arguments)
        {
            this.name = name;
            this.arguments = arguments;
        }

        public object Evaluate(Context context)
        {
            IFunction function = (IFunction)context.GetValue(this.name);

            IList<object> values = new List<object>();

            foreach (var argument in this.arguments)
                values.Add(argument.Evaluate(context));

            return function.Apply(values);
        }
    }
}
