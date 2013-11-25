namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;

    public class DefDotExpression : IExpression
    {
        private static int hashcode = typeof(DefDotExpression).GetHashCode();

        private IExpression expression;
        private string name;
        private IList<string> parameters;
        private IExpression command;

        public DefDotExpression(IExpression expression, string name, IList<string> parameters, IExpression command)
        {
            this.expression = expression;
            this.name = name;
            this.parameters = parameters;
            this.command = command;
        }

        public object Evaluate(Context context)
        {
            var target = (DynamicObject)this.expression.Evaluate(context);
            var result = new DefinedFunction(this.command, this.parameters, context);
            target.SingletonClass.SetInstanceMethod(this.name, result);

            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is DefDotExpression)
            {
                var expr = (DefDotExpression)obj;

                if (this.parameters.Count != expr.parameters.Count)
                    return false;

                for (int k = 0; k < this.parameters.Count; k++)
                    if (this.parameters[k] != expr.parameters[k])
                        return false;

                return this.expression.Equals(expr.expression) && this.name == expr.name && this.command.Equals(expr.command);
            }

            return false;
        }

        public override int GetHashCode()
        {
            int result = hashcode + this.expression.GetHashCode() + this.name.GetHashCode() + this.command.GetHashCode();

            foreach (var parameter in this.parameters)
                result += parameter.GetHashCode();

            return result;
        }
    }
}
