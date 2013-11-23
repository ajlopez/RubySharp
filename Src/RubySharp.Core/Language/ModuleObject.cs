namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ModuleObject : DynamicObject
    {
        public ModuleObject(DynamicClass @class)
            : base(@class)
        {
        }
    }
}
