namespace RubySharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using RubySharp.Core.Compiler;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;

    public class Machine
    {
        private Context rootcontext = new Context();
        private IList<string> requirepaths = new List<string>();
        private IList<string> required = new List<string>();

        public Machine()
        {
            this.requirepaths.Add(".");
            var basicobjectclass = new DynamicClass("BasicObject", null);
            var objectclass = new ModuleObject("Object", basicobjectclass);
            var moduleclass = new ModuleClass(objectclass);
            var classclass = new ClassClass(moduleclass, objectclass);

            this.rootcontext.SetLocalValue("BasicObject", basicobjectclass);
            this.rootcontext.SetLocalValue("Object", objectclass);
            this.rootcontext.SetLocalValue("Module", moduleclass);
            this.rootcontext.SetLocalValue("Class", classclass);

            basicobjectclass.SetClass(classclass);
            objectclass.SetClass(classclass);
            moduleclass.SetClass(classclass);
            classclass.SetClass(classclass);

            this.rootcontext.SetLocalValue("Fixnum", new FixnumClass(this));
            this.rootcontext.SetLocalValue("Float", new FloatClass(this));
            this.rootcontext.SetLocalValue("String", new StringClass(this));
            this.rootcontext.SetLocalValue("NilClass", new NilClass(this));
            this.rootcontext.SetLocalValue("FalseClass", new FalseClass(this));
            this.rootcontext.SetLocalValue("TrueClass", new TrueClass(this));
            this.rootcontext.SetLocalValue("Array", new ArrayClass(this));
            this.rootcontext.SetLocalValue("Hash", new HashClass(this));
            this.rootcontext.SetLocalValue("Range", new RangeClass(this));

            this.rootcontext.Self = objectclass.CreateInstance();

            this.rootcontext.Self.Class.SetInstanceMethod("puts", new PutsFunction(System.Console.Out));
            this.rootcontext.Self.Class.SetInstanceMethod("print", new PrintFunction(System.Console.Out));
            this.rootcontext.Self.Class.SetInstanceMethod("require", new RequireFunction(this));
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
            string path = Path.GetDirectoryName(filename);

            this.requirepaths.Insert(0, path);

            try
            {
                return this.ExecuteText(System.IO.File.ReadAllText(filename));
            }
            finally
            {
                this.requirepaths.RemoveAt(0);
            }
        }

        public bool RequireFile(string filename)
        {
            if (!Path.IsPathRooted(filename))
            {
                foreach (var path in this.requirepaths)
                {
                    string newfilename = Path.Combine(path, filename);
                    if (!File.Exists(newfilename))
                        if (File.Exists(newfilename + ".rb"))
                            newfilename += ".rb";
                        else if (File.Exists(newfilename + ".dll"))
                            newfilename += ".dll";

                    if (File.Exists(newfilename))
                    {
                        filename = newfilename;
                        break;
                    }
                }
            }
            else
            {
                string newfilename = Path.GetFullPath(filename);

                if (!File.Exists(newfilename))
                    if (File.Exists(newfilename + ".rb"))
                        newfilename += ".rb";
                    else if (File.Exists(newfilename + ".dll"))
                        newfilename += ".dll";

                filename = newfilename;
            }

            if (this.required.Contains(filename))
                return false;

            if (filename.EndsWith(".dll"))
            {
                Assembly.LoadFrom(filename);
                return true;
            }

            this.ExecuteFile(filename);
            this.required.Add(filename);

            return true;
        }

        public object ExecuteReader(TextReader reader)
        {
            Parser parser = new Parser(reader);
            object result = null;

            for (var command = parser.ParseCommand(); command != null; command = parser.ParseCommand())
                result = command.Evaluate(this.rootcontext);

            return result;
        }
    }
}
