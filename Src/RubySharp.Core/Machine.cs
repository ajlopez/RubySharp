namespace RubySharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Compiler;

    public class Machine
    {
        private Context rootcontext = new Context();

        public Context RootContext { get { return this.rootcontext; } }

        public object ExecuteText(string text)
        {
            Parser parser = new Parser(text);
            object result = null;

            for (var command = parser.ParseCommand(); command != null; command = parser.ParseCommand())
                result = command.Execute(this.rootcontext);

            return result;
        }
    }
}
