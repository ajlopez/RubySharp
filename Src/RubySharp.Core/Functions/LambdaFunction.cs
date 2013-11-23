namespace RubySharp.Core.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using RubySharp.Core.Language;

    public class LambdaFunction : IFunction
    {
        private Func<DynamicObject, IList<object>, object> lambda;

        public LambdaFunction(Func<DynamicObject, IList<object>, object> lambda)
        {
            this.lambda = lambda;
        }

        public object Apply(DynamicObject self, IList<object> values)
        {
            return this.lambda(self, values);
        }
    }
}
