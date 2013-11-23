namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ModuleClass : DynamicClass
    {
        public ModuleClass(DynamicClass superclass)
            : base("Module", superclass)
        {
        }
    }
}
