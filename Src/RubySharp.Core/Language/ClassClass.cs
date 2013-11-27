namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;

    public class ClassClass : ModuleClass
    {
        private DynamicClass instancesuperclass;

        public ClassClass(DynamicClass superclass, DynamicClass instancesuperclass)
            : base("Class", superclass)
        {
            this.instancesuperclass = instancesuperclass;
            this.SetInstanceMethod("superclass", new LambdaFunction(GetSuperClass));
        }

        public override DynamicObject CreateInstance()
        {
            var obj = base.CreateInstance();
            ((DynamicClass)obj).SetSuperClass(this.instancesuperclass);
            return obj;
        }

        private static object GetSuperClass(DynamicObject obj, IList<object> values)
        {
            return ((DynamicClass)obj).SuperClass;
        }
    }
}
