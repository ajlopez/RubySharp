namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;

    public class NameExpression : IExpression
    {
        private static int hashcode = typeof(NameExpression).GetHashCode();
        private string name;

        public NameExpression(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public object Evaluate(Context context)
        {
            var result = context.GetValue(this.name);

            if (result is IFunction)
                return ((IFunction)result).Apply(null);

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
