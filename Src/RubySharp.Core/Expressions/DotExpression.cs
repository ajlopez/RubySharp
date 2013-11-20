namespace RubySharp.Core.Expressions
{
    using System.Collections.Generic;
    using RubySharp.Core.Exceptions;
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
            var result = this.expression.Evaluate(context);

            if (!(result is BaseObject) && this.name == "class")
            {
                if (result is int)
                    return FixnumClass.Instance;
            }

            var obj = (BaseObject)this.expression.Evaluate(context);

            IList<object> values = new List<object>();

            foreach (var argument in this.arguments)
                values.Add(argument.Evaluate(context));

            var method = obj.GetMethod(this.name);

            if (method == null)
                throw new NoMethodError(this.name);

            return method.Apply(obj, values);
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
