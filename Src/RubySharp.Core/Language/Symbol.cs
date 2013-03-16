namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Symbol
    {
        private string name;

        public Symbol(string name)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return ":" + this.name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is Symbol)
            {
                var symbol = (Symbol)obj;

                return this.name == symbol.name;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.name.GetHashCode() + 17;
        }
    }
}
