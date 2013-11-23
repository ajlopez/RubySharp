namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ModuleObject : DynamicObject
    {
        private Context constants;

        public ModuleObject(DynamicClass @class)
            : base(@class)
        {
            this.constants = new Context();
        }

        public string Name { get; internal set; }

        public Context Constants { get { return this.constants; } }
    }
}
