namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Functions;

    public class DynamicClass : DynamicObject
    {
        private static IFunction newfunction = new NewFunction();

        private string name;
        private DynamicClass superclass;
        private IDictionary<string, IFunction> methods = new Dictionary<string, IFunction>();

        public DynamicClass(string name, DynamicClass superclass = null)
            : this(null, name, superclass)
        {
        }

        public DynamicClass(DynamicClass @class, string name, DynamicClass superclass = null)
            : base(@class)
        {
            this.name = name;
            this.superclass = superclass;

            // TODO Hack for singleton class
            if (name == null || !name.StartsWith("#")) 
            {
                this.SetInstanceMethod("class", new LambdaFunction(GetClass));
                this.SetInstanceMethod("methods", new LambdaFunction(GetMethods));
                this.SetInstanceMethod("singleton_methods", new LambdaFunction(GetSingletonMethods));
            }
        }

        public string Name { get { return this.name; } internal set { this.name = value; } }

        public DynamicClass SuperClass { get { return this.superclass; } }

        public void SetInstanceMethod(string name, IFunction method)
        {
            this.methods[name] = method;
        }

        public IFunction GetInstanceMethod(string name)
        {
            if (this.methods.ContainsKey(name))
                return this.methods[name];

            if (this.superclass != null)
                return this.superclass.GetInstanceMethod(name);

            return null;
        }

        public virtual DynamicObject CreateInstance()
        {
            return new DynamicObject(this);
        }

        public override IFunction GetMethod(string name)
        {
            if (name == "new")
                return newfunction;

            return base.GetMethod(name);
        }

        public IList<string> GetOwnInstanceMethodNames()
        {
            return this.methods.Keys.ToList();
        }

        public override string ToString()
        {
            return this.Name;
        }

        internal void SetSuperClass(DynamicClass superclass)
        {
            this.superclass = superclass;
        }

        private static object GetClass(DynamicObject obj, IList<object> values)
        {
            return obj.Class;
        }

        private static object GetMethods(DynamicObject obj, IList<object> values)
        {
            var result = new DynamicArray();

            for (var @class = obj.SingletonClass; @class != null; @class = @class.SuperClass)
            {
                var names = @class.GetOwnInstanceMethodNames();

                foreach (var name in names)
                {
                    Symbol symbol = new Symbol(name);

                    if (!result.Contains(symbol))
                        result.Add(symbol);
                }
            }

            return result;
        }

        private static object GetSingletonMethods(DynamicObject obj, IList<object> values)
        {
            var result = new DynamicArray();

            var names = obj.SingletonClass.GetOwnInstanceMethodNames();

            foreach (var name in names)
            {
                Symbol symbol = new Symbol(name);

                if (!result.Contains(symbol))
                    result.Add(symbol);
            }

            return result;
        }
    }
}
