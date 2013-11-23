namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using RubySharp.Core.Exceptions;
    using RubySharp.Core.Functions;
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
            IList<object> values = new List<object>();
            var result = this.expression.Evaluate(context);

            if (!(result is DynamicObject))
            {
                NativeClass nclass = (NativeClass)NativeClass.MethodClass(result, null);
                Func<object, IList<object>, object> nmethod = nclass.GetInstanceMethod(this.name);

                if (nmethod == null)
                    throw new NoMethodError(this.name);

                if (this.arguments != null)
                    foreach (var argument in this.arguments)
                        values.Add(argument.Evaluate(context));

                return nmethod(result, values);
            }

            var obj = (DynamicObject)result;

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
