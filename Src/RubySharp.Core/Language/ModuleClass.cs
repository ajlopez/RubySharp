namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;

    public class ModuleClass : DynamicClass
    {
        public ModuleClass(DynamicClass superclass)
            : this("Module", superclass)
        {
        }

        public ModuleClass(string name, DynamicClass superclass)
            : base(name, superclass)
        {
            this.SetInstanceMethod("name", new LambdaFunction(this.GetName));
        }

        public override DynamicObject CreateInstance()
        {
            return new ModuleObject(this);
        }

        private object GetName(DynamicObject obj, IList<object> values)
        {
            return ((ModuleObject)obj).Name;
        }
    }
}
