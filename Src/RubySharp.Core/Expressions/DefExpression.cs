namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;

    public class DefExpression : IExpression
    {
        private static int hashcode = typeof(DefExpression).GetHashCode();

        private INamedExpression namedexpression;
        private IList<string> parameters;
        private IExpression expression;

        public DefExpression(INamedExpression namedexpression, IList<string> parameters, IExpression expression)
        {
            this.namedexpression = namedexpression;
            this.parameters = parameters;
            this.expression = expression;
        }

        public object Evaluate(Context context)
        {
            var result = new DefinedFunction(this.expression, this.parameters, context);

            if (this.namedexpression.TargetExpression == null)
            {
                if (context.Module != null)
                    context.Module.SetInstanceMethod(this.namedexpression.Name, result);
                else
                    context.Self.Class.SetInstanceMethod(this.namedexpression.Name, result);
            }
            else
            {
                var target = (DynamicObject)this.namedexpression.TargetExpression.Evaluate(context);
                target.SingletonClass.SetInstanceMethod(this.namedexpression.Name, result);
            }

            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is DefExpression)
            {
                var expr = (DefExpression)obj;

                if (this.parameters.Count != expr.parameters.Count)
                    return false;

                for (int k = 0; k < this.parameters.Count; k++)
                    if (this.parameters[k] != expr.parameters[k])
                        return false;

                return this.namedexpression.Equals(expr.namedexpression) && this.expression.Equals(expr.expression);
            }

            return false;
        }

        public override int GetHashCode()
        {
            int result = hashcode + this.namedexpression.GetHashCode() + this.expression.GetHashCode();

            foreach (var parameter in this.parameters)
                result += parameter.GetHashCode();

            return result;
        }
    }
}
