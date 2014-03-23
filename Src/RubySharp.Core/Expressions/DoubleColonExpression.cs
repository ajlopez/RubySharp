namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using RubySharp.Core.Exceptions;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;
    using RubySharp.Core.Utilities;

    public class DoubleColonExpression : BaseExpression, INamedExpression
    {
        private static int hashcode = typeof(DoubleColonExpression).GetHashCode();

        private IExpression expression;
        private string name;

        public DoubleColonExpression(IExpression expression, string name)
        {
            this.expression = expression;
            this.name = name;
        }

        public IExpression TargetExpression { get { return this.expression; } }

        public string Name { get { return this.name; } }

        public override object Evaluate(Context context)
        {
            IList<object> values = new List<object>();
            var result = this.expression.Evaluate(context);

            if (result is Type)
                return TypeUtilities.ParseEnumValue((Type)result, this.name);

            var obj = (DynamicClass)result;

            if (!obj.Constants.HasLocalValue(this.name))
                throw new NameError(string.Format("unitialized constant {0}::{1}", obj.Name, this.name));

            return obj.Constants.GetLocalValue(this.name);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is DoubleColonExpression)
            {
                var expr = (DoubleColonExpression)obj;

                return this.name.Equals(expr.name) && this.expression.Equals(expr.expression);
            }

            return false;
        }

        public override int GetHashCode()
        {
            int result = this.name.GetHashCode() + this.expression.GetHashCode() + hashcode;

            return result;
        }
    }
}
