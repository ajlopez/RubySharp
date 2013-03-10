namespace RubySharp.Core.Functions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class PutsFunction
    {
        private TextWriter writer;

        public PutsFunction(TextWriter writer)
        {
            this.writer = writer;
        }

        public object Apply(IList<object> arguments)
        {
            foreach (var argument in arguments)
                this.writer.WriteLine(argument);

            return null;
        }
    }
}
