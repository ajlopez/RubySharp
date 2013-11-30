namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Exceptions;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;

    public class ClassExpression : IExpression
    {
        private static int hashcode = typeof(ClassExpression).GetHashCode();
        private INamedExpression namedexpression;
        private IExpression expression;
        private INamedExpression superclassexpression;

        public ClassExpression(INamedExpression namedexpression, IExpression expression, INamedExpression superclassexpression = null)
        {
            this.namedexpression = namedexpression;
            this.expression = expression;
            this.superclassexpression = superclassexpression;
        }

        public object Evaluate(Context context)
        {
            object value = null;
            DynamicClass target = null;

            if (this.namedexpression.TargetExpression == null)
            {
                if (context.Module != null)
                {
                    if (context.Module.Constants.HasLocalValue(this.namedexpression.Name))
                        value = context.Module.Constants.GetLocalValue(this.namedexpression.Name);
                }
                else if (context.HasValue(this.namedexpression.Name))
                    value = context.GetValue(this.namedexpression.Name);
            }
            else
            {
                object targetvalue = this.namedexpression.TargetExpression.Evaluate(context);

                if (!(targetvalue is DynamicClass))
                    throw new TypeError(string.Format("{0} is not a class/module", targetvalue.ToString()));

                target = (DynamicClass)targetvalue;

                if (target.Constants.HasLocalValue(this.namedexpression.Name))
                    value = target.Constants.GetLocalValue(this.namedexpression.Name);
            }

            if (value == null || !(value is DynamicClass))
            {
                var classclass = (DynamicClass)context.RootContext.GetLocalValue("Class");
                var objectclass = (DynamicClass)context.RootContext.GetLocalValue("Object");
                var newclass = new DynamicClass(classclass, this.namedexpression.Name, objectclass);

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

                if (this.superclassexpression != null)
                {
                    var superclass = (DynamicClass)this.superclassexpression.Evaluate(context);
                    newclass.SetSuperClass(superclass);
                }

                value = newclass;
            }

            var dclass = (DynamicClass)value;

            Context classcontext = new Context(dclass, context);
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
