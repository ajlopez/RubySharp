namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;

    public class ClassVarExpression : IExpression
    {
        private static int hashcode = typeof(ClassVarExpression).GetHashCode();
        private string name;

        public ClassVarExpression(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public object Evaluate(Context context)
        {
            var result = context.Self.Class.GetValue(this.name);

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is ClassVarExpression) 
            {
                var expr = (ClassVarExpression)obj;

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
