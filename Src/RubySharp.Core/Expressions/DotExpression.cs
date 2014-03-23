namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RubySharp.Core.Exceptions;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;
    using RubySharp.Core.Utilities;

    public class DotExpression : INamedExpression
    {
        private static int hashcode = typeof(DotExpression).GetHashCode();

        private IExpression expression;
        private string name;
        private IList<IExpression> arguments;
        private string qname;

        public DotExpression(IExpression expression, string name, IList<IExpression> arguments)
        {
            this.expression = expression;
            this.name = name;
            this.arguments = arguments;

            this.qname = this.AsQualifiedName();
        }

        public IExpression TargetExpression { get { return this.expression; } }

        public string Name { get { return this.name; } }

        public object Evaluate(Context context)
        {
            if (this.qname != null)
            {
                Type type = TypeUtilities.AsType(this.qname);

                if (type != null)
                    return type;
            }

            IList<object> values = new List<object>();
            var result = this.expression.Evaluate(context);

            if (!(result is DynamicObject))
            {
                NativeClass nclass = (NativeClass)context.GetValue("Fixnum");
                nclass = (NativeClass)nclass.MethodClass(result, null);
                Func<object, IList<object>, object> nmethod = null;
                
                if (nclass != null)
                    nmethod = nclass.GetInstanceMethod(this.name);

                if (this.arguments != null)
                    foreach (var argument in this.arguments)
                        values.Add(argument.Evaluate(context));

                if (nmethod == null)
                {
                    if (result is Type && this.name == "new")
                        return Activator.CreateInstance((Type)result, values.ToArray());

                    if (result is Type)
                        return TypeUtilities.InvokeTypeMember((Type)result, this.name, values);

                    return ObjectUtilities.GetValue(result, this.name, values);
                }

                return nmethod(result, values);
            }

            var obj = (DynamicObject)result;

            foreach (var argument in this.arguments)
                values.Add(argument.Evaluate(context));

            var method = obj.GetMethod(this.name);

            if (method == null)
            {
                if (Predicates.IsConstantName(this.name))
                    try
                    {
                        return ObjectUtilities.GetNativeValue(obj, this.name, values);
                    }
                    catch
                    {
                    }

                throw new NoMethodError(this.name);
            }

            return method.Apply(obj, context, values);
        }

        public string AsQualifiedName()
        {
            if (!char.IsUpper(this.name[0]))
                return null;

            if (this.expression is NameExpression)
            {
                string prefix = ((NameExpression)this.expression).AsQualifiedName();

                if (prefix == null)
                    return null;

                return prefix + "." + this.name;
            }

            if (this.expression is DotExpression)
            {
                string prefix = ((DotExpression)this.expression).AsQualifiedName();

                if (prefix == null)
                    return null;

                return prefix + "." + this.name;
            }

            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is DotExpression)
            {
                var expr = (DotExpression)obj;

                if (this.arguments.Count != expr.arguments.Count)
                    return false;

                for (var k = 0; k < this.arguments.Count; k++)
                    if (!this.arguments[k].Equals(expr.arguments[k]))
                        return false;

                return this.name.Equals(expr.name) && this.expression.Equals(expr.expression);
            }

            return false;
        }

        public override int GetHashCode()
        {
            int result = this.name.GetHashCode() + this.expression.GetHashCode() + hashcode;

            foreach (var argument in this.arguments)
                result += argument.GetHashCode();

            return result;
        }
    }
}
