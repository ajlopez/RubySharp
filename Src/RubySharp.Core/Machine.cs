namespace RubySharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Compiler;
    using RubySharp.Core.Functions;

    public class Machine
    {
        private Context rootcontext = new Context();

        public Machine()
        {
            this.rootcontext.SetValue("puts", new PutsFunction(System.Console.Out));
        }

        public Context RootContext { get { return this.rootcontext; } }

        public object ExecuteText(string text)
        {
            Parser parser = new Parser(text);
            object result = null;

            for (var command = parser.ParseCommand(); command != null; command = parser.ParseCommand())
                result = command.Execute(this.rootcontext);

            return result;
        }

        public object ExecuteFile(string filename)
        {
            return this.ExecuteText(System.IO.File.ReadAllText(filename));
        }
    }
}
