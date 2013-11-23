namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;

    public class ClassExpression : IExpression
    {
        private static int hashcode = typeof(ClassExpression).GetHashCode();
        private string name;
        private IExpression expression;

        public ClassExpression(string name, IExpression expression)
        {
            this.name = name;
            this.expression = expression;
        }

        public object Evaluate(Context context)
        {
            object value = null;
            
            if (context.HasLocalValue(this.name))
                value = context.GetLocalValue(this.name);

            if (value == null || !(value is DynamicClass))
            {
                var newclass = new DynamicClass(this.name);
                context.SetLocalValue(this.name, newclass);
                value = newclass;

                if (context.Module != null && char.IsUpper(this.name[0]))
                    context.Module.Constants.SetLocalValue(this.name, newclass);
            }

            var dclass = (DynamicClass)value;

            Context classcontext = new Context(dclass, null, context);

            this.expression.Evaluate(classcontext);

            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is ClassExpression)
            {
                var cmd = (ClassExpression)obj;

                return this.name == cmd.name && this.expression.Equals(cmd.expression);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.name.GetHashCode() + this.expression.GetHashCode() + hashcode;
        }
    }
}
