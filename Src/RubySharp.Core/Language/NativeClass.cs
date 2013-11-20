namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;

    public class NativeClass : BaseObject
    {
        private string name;
        private IDictionary<string, INativeFunction> methods = new Dictionary<string, INativeFunction>();

        public NativeClass(string name)
            : base(null)
        {
            this.name = name;
            this.SetInstanceMethod("class", new NativeClassFunction());
        }

        public string Name { get { return this.name; } }

        public void SetInstanceMethod(string name, INativeFunction method)
        {
            this.methods[name] = method;
        }

        public INativeFunction GetInstanceMethod(string name)
        {
            if (this.methods.ContainsKey(name))
                return this.methods[name];

            return null;
        }
    }
}
