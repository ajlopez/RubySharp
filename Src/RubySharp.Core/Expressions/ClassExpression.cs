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
        private INamedExpression namedexpression;
        private IExpression expression;

        public ClassExpression(INamedExpression namedexpression, IExpression expression)
        {
            this.namedexpression = namedexpression;
            this.expression = expression;
        }

        public object Evaluate(Context context)
        {
            object value = null;
            ModuleObject target = null;

            if (this.namedexpression.TargetExpression == null)
            {
                if (context.HasValue(this.namedexpression.Name))
                    value = context.GetValue(this.namedexpression.Name);
            }
            else
            {
                target = (ModuleObject)this.namedexpression.TargetExpression.Evaluate(context);
                value = target.Constants.GetLocalValue(this.namedexpression.Name);
            }

            if (value == null || !(value is DynamicClass))
            {
                var classclass = (DynamicClass)context.RootContext.GetLocalValue("Class");
                var newclass = (DynamicClass)classclass.CreateInstance();

                if (target == null)
                {
                    if (context.Module != null)
                    {
                        newclass.Name = context.Module.Name + "::" + this.namedexpression.Name;
                        context.Module.Constants.SetLocalValue(this.namedexpression.Name, newclass);
                    }
                    else
                    {
                        newclass.Name = this.namedexpression.Name;
                        context.RootContext.SetLocalValue(this.namedexpression.Name, newclass);
                    }
                }
                else
                {
                    newclass.Name = target.Name + "::" + this.namedexpression.Name;
                    target.Constants.SetLocalValue(this.namedexpression.Name, newclass);
                }

                value = newclass;
            }

            var dclass = (DynamicClass)value;

            Context classcontext = new Context((ModuleObject)dclass, context);
            classcontext.Self = dclass;

            this.expression.Evaluate(classcontext);

            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is ClassExpression)
            {
                var expr = (ClassExpression)obj;

                return this.namedexpression.Equals(expr.namedexpression) && this.expression.Equals(expr.expression);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.namedexpression.GetHashCode() + this.expression.GetHashCode() + hashcode;
        }
    }
}
