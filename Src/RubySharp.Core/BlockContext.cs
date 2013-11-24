namespace RubySharp.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class BlockContext : Context
    {
        public BlockContext(Context parent)
            : base(parent)
        {
        }

        public override bool HasLocalValue(string name)
        {
            if (base.HasLocalValue(name))
                return true;

            return this.Parent.HasLocalValue(name);
        }

        public override object GetLocalValue(string name)
        {
            if (base.HasLocalValue(name))
                return base.GetLocalValue(name);

            return this.Parent.GetLocalValue(name);
        }

        public override void SetLocalValue(string name, object value)
        {
            if (this.Parent.HasLocalValue(name))
                this.Parent.SetLocalValue(name, value);
            else
                base.SetLocalValue(name, value);
        }
    }
}
