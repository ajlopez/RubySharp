namespace RubySharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Compiler;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;

    public class Machine
    {
        private Context rootcontext = new Context();

        public Machine()
        {
            var basicobjectclass = new DynamicClass("BasicObject", null);
            var objectclass = new DynamicClass("Object", basicobjectclass);
            var moduleclass = new DynamicClass("Module", objectclass);
            var classclass = new DynamicClass("Class", moduleclass);

            this.rootcontext.SetLocalValue("BasicObject", basicobjectclass);
            this.rootcontext.SetLocalValue("Object", objectclass);
            this.rootcontext.SetLocalValue("Module", moduleclass);
            this.rootcontext.SetLocalValue("Class", classclass);

            this.rootcontext.SetLocalValue("Fixnum", new FixnumClass(this));
            this.rootcontext.SetLocalValue("Float", new FloatClass(this));
            this.rootcontext.SetLocalValue("String", new StringClass(this));
            this.rootcontext.SetLocalValue("NilClass", new NilClass(this));
            this.rootcontext.SetLocalValue("FalseClass", new FalseClass(this));
            this.rootcontext.SetLocalValue("TrueClass", new TrueClass(this));

            this.rootcontext.SetLocalValue("puts", new PutsFunction(System.Console.Out));
            this.rootcontext.SetLocalValue("print", new PrintFunction(System.Console.Out));
        }

        public Context RootContext { get { return this.rootcontext; } }

        public object ExecuteText(string text)
        {
            Parser parser = new Parser(text);
            object result = null;

            for (var command = parser.ParseCommand(); command != null; command = parser.ParseCommand())
                result = command.Evaluate(this.rootcontext);

            return result;
        }

        public object ExecuteFile(string filename)
        {
            return this.ExecuteText(System.IO.File.ReadAllText(filename));
        }
    }
}
