namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Expression
    {
        private object value;

        public Expression(object value)
        {
            this.value = value;
        }

        public object Value { get { return this.value; } }
    }
}
