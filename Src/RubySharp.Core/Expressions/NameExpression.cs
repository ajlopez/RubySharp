namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Exceptions;
    using RubySharp.Core.Functions;

    public class NameExpression : IExpression
    {
        private static int hashcode = typeof(NameExpression).GetHashCode();
        private static IList<object> emptyvalues = new object[] { };
        private string name;

        public NameExpression(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public object Evaluate(Context context)
        {
            object result = null;

            if (context.HasLocalValue(this.name))
                result = context.GetLocalValue(this.name);
            else
            {
                result = context.GetValue(this.name);

                if (result == null || !(result is IFunction))
                    throw new NameError(string.Format("undefined local variable or method '{0}'", this.name));
            }

            if (result is IFunction)
                return ((IFunction)result).Apply(null, emptyvalues);

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is NameExpression) 
            {
                var expr = (NameExpression)obj;

                return this.Name.Equals(expr.Name);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.name.GetHashCode() + hashcode;
        }
    }
}
