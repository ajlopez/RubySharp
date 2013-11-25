namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;

    public class DynamicObject
    {
        private DynamicClass @class;
        private DynamicClass singletonclass;
        private IDictionary<string, object> values = new Dictionary<string, object>();

        public DynamicObject(DynamicClass @class)
        {
            this.@class = @class;
        }

        public DynamicClass @Class { get { return this.@class; } }

        public DynamicClass SingletonClass
        {
            get
            {
                if (this.singletonclass == null)
                    this.singletonclass = new DynamicClass(string.Format("#<Class:{0}>", this.ToString()), this.@class);

                return this.singletonclass;
            }
        }

        public void SetValue(string name, object value)
        {
            this.values[name] = value;
        }

        public object GetValue(string name)
        {
            if (this.values.ContainsKey(name))
                return this.values[name];

            return null;
        }

        public virtual IFunction GetMethod(string name)
        {
            if (this.singletonclass != null)
                return this.singletonclass.GetInstanceMethod(name);

            if (this.@class != null)
                return this.@class.GetInstanceMethod(name);

            return null;
        }

        public override string ToString()
        {
            return string.Format("#<{0}:0x{1}>", this.Class.Name, this.GetHashCode().ToString("x"));
        }

        internal void SetClass(DynamicClass @class)
        {
            this.@class = @class;
        }
    }
}
