namespace RubySharp.Core.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Commands;

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

        public object Apply(IList<object> values)
        {
            return this.body.Execute(this.context);
        }
    }
}
