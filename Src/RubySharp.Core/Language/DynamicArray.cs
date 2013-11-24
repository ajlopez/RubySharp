namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;

    public class DynamicArray : ArrayList
    {
        public override object this[int index]
        {
            get
            {
                if (index < 0 || index >= this.Count)
                    return null;

                return base[index];
            }
            set
            {
                while (index >= this.Count)
                    this.Add(null);

                base[index] = value;
            }
        }

        public override string ToString()
        {
            var result = "[";

            foreach (var value in this)
            {
                if (result.Length > 1)
                    result += ", ";

                if (value == null)
                    result += "nil";
                else
                    result += value.ToString();
            }

            result += "]";

            return result;
        }
    }
}
