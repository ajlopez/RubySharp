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
        private Machine machine;
        private IDictionary<string, Func<object, IList<object>, object>> methods = new Dictionary<string, Func<object, IList<object>, object>>();

        private NativeClass fixnumclass;
        private NativeClass floatclass;
        private NativeClass stringclass;
        private NativeClass nilclass;
        private NativeClass falseclass;
        private NativeClass trueclass;

        public NativeClass(string name, Machine machine)
            : base(null)
        {
            this.name = name;
            this.machine = machine;
            this.SetInstanceMethod("class", this.MethodClass);
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

        public object MethodClass(object self, IList<object> values)
        {
            if (self == null)
            {
                if (this.nilclass == null)
                    this.nilclass = (NativeClass)this.machine.RootContext.GetLocalValue("NilClass");

                return this.nilclass;
            }

            if (self is int)
            {
                if (this.fixnumclass == null)
                    this.fixnumclass = (NativeClass)this.machine.RootContext.GetLocalValue("Fixnum");

                return this.fixnumclass;
            }

            if (self is double)
            {
                if (this.floatclass == null)
                    this.floatclass = (NativeClass)this.machine.RootContext.GetLocalValue("Float");

                return this.floatclass;
            }

            if (self is string)
            {
                if (this.stringclass == null)
                    this.stringclass = (NativeClass)this.machine.RootContext.GetLocalValue("String");

                return this.stringclass;
            }

            if (self is bool)
                if ((bool)self)
                {
                    if (this.trueclass == null)
                        this.trueclass = (NativeClass)this.machine.RootContext.GetLocalValue("TrueClass");

                    return this.trueclass;
                }
                else
                {
                    if (this.falseclass == null)
                        this.falseclass = (NativeClass)this.machine.RootContext.GetLocalValue("FalseClass");

                    return this.falseclass;
                }

            throw new NotImplementedException();
        }
    }
}
