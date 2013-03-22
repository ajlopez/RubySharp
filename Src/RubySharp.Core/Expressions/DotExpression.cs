namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Language;

    public class DotExpression : IExpression
    {
        private static int hashcode = typeof(DotExpression).GetHashCode();
        private IExpression expression;
        private string name;
        private IList<IExpression> arguments;

        public DotExpression(IExpression expression, string name, IList<IExpression> arguments)
        {
            this.expression = expression;
            this.name = name;
            this.arguments = arguments;
        }

        public IExpression Expression { get { return this.expression; } }

        public string Name { get { return this.name; } }

        public object Evaluate(Context context)
        {
            var obj = (BaseObject)this.expression.Evaluate(context);

            IList<object> values = new List<object>();

            foreach (var argument in this.arguments)
                values.Add(argument.Evaluate(context));

            return obj.GetMethod(this.name).Apply(obj, values);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is DotExpression)
            {
                var expr = (DotExpression)obj;

                if (this.arguments.Count != expr.arguments.Count)
                    return false;

                for (var k = 0; k < this.arguments.Count; k++)
                    if (!this.arguments[k].Equals(expr.arguments[k]))
                        return false;

                return this.name.Equals(expr.name) && this.expression.Equals(expr.expression);
            }

            return false;
        }

        public override int GetHashCode()
        {
            int result = this.name.GetHashCode() + this.expression.GetHashCode() + hashcode;

            foreach (var argument in this.arguments)
                result += argument.GetHashCode();

            return result;
        }
    }
}
