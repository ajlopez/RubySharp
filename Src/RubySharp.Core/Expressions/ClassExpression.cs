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
        private IExpression command;

        public ClassExpression(string name, IExpression command)
        {
            this.name = name;
            this.command = command;
        }

        public object Evaluate(Context context)
        {
            var value = context.GetValue(this.name);

            if (value == null || !(value is DefinedClass))
            {
                var newclass = new DefinedClass(this.name);
                context.SetValue(this.name, newclass);
                value = newclass;
            }

            var dclass = (DefinedClass)value;

            Context classcontext = new Context(dclass, null, context);

            this.command.Evaluate(classcontext);

            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is ClassExpression)
            {
                var cmd = (ClassExpression)obj;

                return this.name == cmd.name && this.command.Equals(cmd.command);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.name.GetHashCode() + this.command.GetHashCode() + hashcode;
        }
    }
}
