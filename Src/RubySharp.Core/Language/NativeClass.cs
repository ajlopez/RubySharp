namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;

    public class NativeClass : DynamicObject
    {
        private string name;
        private IDictionary<string, Func<object, IList<object>, object>> methods = new Dictionary<string, Func<object, IList<object>, object>>();

        public NativeClass(string name)
            : base(null)
        {
            this.name = name;
            this.SetInstanceMethod("class", MethodClass);
        }

        public string Name { get { return this.name; } }

        public void SetInstanceMethod(string name, Func<object, IList<object>, object> method)
        {
            this.methods[name] = method;
        }

        public Func<object, IList<object>, object> GetInstanceMethod(string name)
        {
            if (this.methods.ContainsKey(name))
                return this.methods[name];

            return null;
        }

        public static object MethodClass(object self, IList<object> values)
        {
            if (self == null)
                return NilClass.Instance;

            if (self is int)
                return FixnumClass.Instance;

            if (self is double)
                return FloatClass.Instance;

            if (self is string)
                return StringClass.Instance;

            if (self is bool)
                if ((bool)self)
                    return TrueClass.Instance;
                else
                    return FalseClass.Instance;

            throw new NotImplementedException();
        }
    }
}
