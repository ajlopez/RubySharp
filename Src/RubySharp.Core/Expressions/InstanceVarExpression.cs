namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;

    public class InstanceVarExpression : BaseExpression
    {
        private static int hashcode = typeof(InstanceVarExpression).GetHashCode();
        private string name;

        public InstanceVarExpression(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }

        public override object Evaluate(Context context)
        {
            var result = context.Self.GetValue(this.name);

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is InstanceVarExpression) 
            {
                var expr = (InstanceVarExpression)obj;

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
