namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DynamicHash : Dictionary<object, object>
    {
        public override string ToString()
        {
            var result = "{";

            foreach (var key in this.Keys)
            {
                var value = this[key];

                if (result.Length > 1)
                    result += ", ";

                result += key.ToString();
                result += "=>";
                result += value.ToString();
            }

            result += "}";

            return result;
        }
    }
}
