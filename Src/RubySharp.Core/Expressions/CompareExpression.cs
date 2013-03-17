namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualBasic.CompilerServices;

    public class CompareExpression : BinaryExpression
    {
        private static IDictionary<CompareOperator, Func<object, object, object>> functions = new Dictionary<CompareOperator, Func<object, object, object>>();
        private CompareOperator @operator;

        static CompareExpression()
        {
            functions[CompareOperator.Equal] = (left, right) => Operators.CompareObjectEqual(left, right, false);
            functions[CompareOperator.NotEqual] = (left, right) => Operators.CompareObjectNotEqual(left, right, false);
            functions[CompareOperator.Less] = (left, right) => Operators.CompareObjectLess(left, right, false);
            functions[CompareOperator.Greater] = (left, right) => Operators.CompareObjectGreater(left, right, false);
            functions[CompareOperator.LessOrEqual] = (left, right) => Operators.CompareObjectLessEqual(left, right, false);
            functions[CompareOperator.GreaterOrEqual] = (left, right) => Operators.CompareObjectGreaterEqual(left, right, false);
        }

        public CompareExpression(IExpression left, IExpression right, CompareOperator @operator)
            : base(left, right)
        {
            this.@operator = @operator;
        }

        public override object Apply(object leftvalue, object rightvalue)
        {
            return functions[this.@operator](leftvalue, rightvalue);
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj))
                return false;

            return this.@operator == ((CompareExpression)obj).@operator;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() + (int)this.@operator;
        }
    }
}
