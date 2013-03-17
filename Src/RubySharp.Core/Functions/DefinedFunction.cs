namespace RubySharp.Core.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Commands;
    using RubySharp.Core.Language;

    public class DefinedFunction : IFunction
    {
        private ICommand body;
        private IList<string> parameters;
        private Context context;

        public DefinedFunction(ICommand body, IList<string> parameters, Context context)
        {
            this.body = body;
            this.context = context;
            this.parameters = parameters;
        }

        public object Apply(BaseObject self, IList<object> values)
        {
            Context newcontext = new Context(null, self, this.context);

            int k = 0;
            int cv = values.Count;

            foreach (var parameter in this.parameters) 
            {
                newcontext.SetValue(parameter, values[k]);
                k++;
            }

            return this.body.Execute(newcontext);
        }
    }
}
