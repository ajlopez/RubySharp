namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Language;

    public class ModuleExpression : IExpression
    {
        private static int hashcode = typeof(ClassExpression).GetHashCode();

        private string name;
        private IExpression expression;

        public ModuleExpression(string name, IExpression expression)
        {
            this.name = name;
            this.expression = expression;
        }

        public object Evaluate(Context context)
        {
            ModuleClass modclass = (ModuleClass)context.GetValue("Module");
            ModuleObject module = (ModuleObject)modclass.CreateInstance();
            module.Name = this.name;
            context.SetLocalValue(this.name, module);
            Context newcontext = new Context(module, context.RootContext);
            return this.expression.Evaluate(newcontext);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is ModuleExpression)
            {
                var expr = (ModuleExpression)obj;

                return this.name == expr.name && this.expression.Equals(expr.expression);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.name.GetHashCode() + this.expression.GetHashCode() + hashcode;
        }
    }
}
