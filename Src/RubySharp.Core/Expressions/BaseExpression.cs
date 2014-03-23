namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class BaseExpression : IExpression
    {
        public abstract object Evaluate(Context context);

        public IList<string> GetLocalVariables()
        {
            return null;
        }

        internal static IList<string> GetLocalVariables(IList<IExpression> expressions)
        {
            IList<string> varnames = new List<string>();

            foreach (var expression in expressions)
            {
                if (expression == null)
                    continue;

                var vars = expression.GetLocalVariables();

                if (vars == null || vars.Count == 0)
                    continue;

                foreach (var name in vars)
                    if (!varnames.Contains(name))
                        varnames.Add(name);
            }

            return varnames;
        }
    }
}
