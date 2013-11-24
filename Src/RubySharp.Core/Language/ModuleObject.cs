namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ModuleObject : DynamicClass
    {
        private Context constants;

        public ModuleObject(DynamicClass @class)
            : base(@class, null)
        {
            this.constants = new Context();
        }

        public Context Constants { get { return this.constants; } }
    }
}
