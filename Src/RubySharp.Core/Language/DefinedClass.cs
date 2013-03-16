namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DefinedClass
    {
        private string name;

        public DefinedClass(string name)
        {
            this.name = name;
        }

        public string Name { get { return this.name; } }
    }
}
