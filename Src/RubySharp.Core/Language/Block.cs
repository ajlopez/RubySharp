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
            if (this.argumentnames == null || this.argumentnames.Count == 0)
                return this.expression.Evaluate(context);

            Context newcontext = new Context(context);

            for (int k = 0; k < this.argumentnames.Count; k++)
                if (arguments != null && k < arguments.Count)
                    newcontext.SetValue(argumentnames[k], arguments[k]);
                else
                    newcontext.SetValue(argumentnames[k], null);

            return this.expression.Evaluate(newcontext);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Block))
                return false;

            Block blk = (Block)obj;

            if (blk.argumentnames != null && this.argumentnames != null)
            {
                if (blk.argumentnames.Count != this.argumentnames.Count)
                    return false;

                for (int k = 0; k < blk.argumentnames.Count; k++)
                    if (!blk.argumentnames[k].Equals(this.argumentnames[k]))
                        return false;
            }
            else if (blk.argumentnames != null || this.argumentnames != null)
                return false;

            return blk.expression.Equals(this.expression);
        }


        public override int GetHashCode()
        {
            int result = typeof(Block).GetHashCode();

            if (this.argumentnames != null) 
                for (int k = 0; k < this.argumentnames.Count; k++) 
                {
                    result *= 17;
                    result += this.argumentnames[k].GetHashCode();
                }

            result += this.expression.GetHashCode();

            return result;
        }
    }
}
