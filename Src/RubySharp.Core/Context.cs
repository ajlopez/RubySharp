namespace RubySharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Language;

    public class Context
    {
        private Context parent;
        private IDictionary<string, object> values = new Dictionary<string, object>();
        private DefinedClass @class;

        public Context()
            : this(null)
        {
        }

        public Context(Context parent)
        {
            this.parent = parent;
        }

        public Context(DefinedClass @class, Context parent)
        {
            this.@class = @class;
            this.parent = parent;
        }

        public DefinedClass Class { get { return this.@class; } }

        public void SetValue(string name, object value)
        {
            this.values[name] = value;
        }

        public object GetValue(string name)
        {
            if (this.values.ContainsKey(name))
                return this.values[name];

            if (this.parent != null)
                return this.parent.GetValue(name);

            return null;
        }

        public IList<string> GetLocalNames()
        {
            return this.values.Keys.ToList();
        }
    }
}
