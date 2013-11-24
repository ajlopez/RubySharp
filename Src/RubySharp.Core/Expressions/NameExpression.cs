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
            bool isglobal = char.IsUpper(this.name[0]);

            if (!isglobal)
            {
                if (context.HasLocalValue(this.name))
                    return context.GetLocalValue(this.name);

                if (context.Self != null)
                {
                    var method = context.Self.GetMethod(this.name);

                    if (method != null)
                        return method.Apply(context.Self, emptyvalues);
                }

                throw new NameError(string.Format("undefined local variable or method '{0}'", this.name));
            }

            if (context.HasValue(this.name))
                return context.GetValue(this.name);
            
            throw new NameError(string.Format("unitialized constant {0}", this.name));
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
