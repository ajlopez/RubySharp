﻿namespace RubySharp.Core.Expressions
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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is CallExpression)
            {
                var expr = (CallExpression)obj;

                if (this.name != expr.name)
                    return false;

                if (this.arguments.Count != expr.arguments.Count)
                    return false;

                for (var k = 0; k < this.arguments.Count; k++)
                    if (!this.arguments[k].Equals(expr.arguments[k]))
                        return false;

                return true;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            int result = this.name.GetHashCode();

            foreach (var argument in arguments)
                result += argument.GetHashCode();

            return result;
        }

    }
}