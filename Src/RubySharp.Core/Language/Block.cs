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
        private Context context;

        public Block(IList<string> argumentnames, IExpression expression, Context context)
        {
            this.argumentnames = argumentnames;
            this.expression = expression;
            this.context = context;
        }

        public object Apply(IList<object> arguments)
        {
            if (this.argumentnames == null || this.argumentnames.Count == 0)
                return this.expression.Evaluate(this.context);

            Context newcontext = new Context(this.context);

            for (int k = 0; k < this.argumentnames.Count; k++)
                if (arguments != null && k < arguments.Count)
                    newcontext.SetLocalValue(this.argumentnames[k], arguments[k]);
                else
                    newcontext.SetLocalValue(this.argumentnames[k], null);

            return this.expression.Evaluate(newcontext);
        }
    }
}
