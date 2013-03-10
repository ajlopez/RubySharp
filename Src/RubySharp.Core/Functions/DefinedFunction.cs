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
        private Context context;

        public DefinedFunction(ICommand body, Context context)
        {
            this.body = body;
            this.context = context;
        }

        public object Apply(IList<object> values)
        {
            return this.body.Execute(this.context);
        }
    }
}
