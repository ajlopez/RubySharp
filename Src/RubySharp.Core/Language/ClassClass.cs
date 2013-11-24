namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;

    public class ClassClass : ModuleClass
    {
        public ClassClass(DynamicClass superclass)
            : base("Class", superclass)
        {
            this.SetInstanceMethod("superclass", new LambdaFunction(GetSuperClass));
        }

        private static object GetSuperClass(DynamicObject obj, IList<object> values)
        {
            return ((DynamicClass)obj).SuperClass;
        }
    }
}
