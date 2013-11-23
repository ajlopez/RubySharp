namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Language;

    public class BlockExpression : IExpression
    {
        private Block block;

        public BlockExpression(Block block)
        {
            this.block = block;
        }

        public object Evaluate(Context context)
        {
            return this.block;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is BlockExpression))
                return false;

            return this.block.Equals(((BlockExpression)obj).block);
        }

        public override int GetHashCode()
        {
            return typeof(BlockExpression).GetHashCode() + this.block.GetHashCode();
        }
    }
}

