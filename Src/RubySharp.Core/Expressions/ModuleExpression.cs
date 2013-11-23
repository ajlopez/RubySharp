namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Language;

    public class ModuleExpression : IExpression
    {
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
            this.expression.Evaluate(newcontext);
            return null;
        }
    }
}
