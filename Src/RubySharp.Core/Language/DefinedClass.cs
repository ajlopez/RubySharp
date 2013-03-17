namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DefinedClass
    {
        private string name;
        private Context parent;
        private Context context;

        public DefinedClass(string name, Context parent)
        {
            this.name = name;
            this.parent = parent;
            this.context = new Context(parent);
        }

        public string Name { get { return this.name; } }

        public Context Context { get { return this.context; } }
    }
}
