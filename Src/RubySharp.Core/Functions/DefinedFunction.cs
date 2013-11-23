namespace RubySharp.Core.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Language;

    public class DefinedFunction : IFunction
    {
        private IExpression body;
        private IList<string> parameters;
        private Context context;

        public DefinedFunction(IExpression body, IList<string> parameters, Context context)
        {
            this.body = body;
            this.context = context;
            this.parameters = parameters;
        }

        public object Apply(DynamicObject self, IList<object> values)
        {
            Context newcontext = new Context(null, self, this.context);

            int k = 0;
            int cv = values.Count;

            foreach (var parameter in this.parameters) 
            {
                newcontext.SetLocalValue(parameter, values[k]);
                k++;
            }

            return this.body.Evaluate(newcontext);
        }
    }
}
