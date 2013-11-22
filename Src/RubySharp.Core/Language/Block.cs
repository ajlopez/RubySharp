namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Expressions;

    public class Block
    {
        private IList<string> argumentnames;
        private IExpression expression;

        public Block(IList<string> argumentnames, IExpression expression)
        {
            this.argumentnames = argumentnames;
            this.expression = expression;
        }

        public object Apply(Context context, IList<object> arguments)
        {
            if (arguments == null || arguments.Count == 0)
                return this.expression.Evaluate(context);

            Context newcontext = new Context(context);

            for (int k = 0; k < this.argumentnames.Count; k++)
                if (k < arguments.Count)
                    newcontext.SetValue(argumentnames[k], arguments[k]);
                else
                    newcontext.SetValue(argumentnames[k], null);

            return this.expression.Evaluate(newcontext);
        }
    }
}
