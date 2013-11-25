namespace RubySharp.Core.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Language;

    public class RequireFunction : IFunction
    {
        private Machine machine;

        public RequireFunction(Machine machine)
        {
            this.machine = machine;
        }

        public object Apply(DynamicObject self, IList<object> values)
        {
            string filename = (string)values[0];
            return this.machine.RequireFile(filename);
        }
    }
}
