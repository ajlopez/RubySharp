namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;

    public class ArrayClass : NativeClass
    {
        public ArrayClass(Machine machine)
            : base("Array", machine)
        {
            this.SetInstanceMethod("each", DoEach);
        }

        private static object DoEach(object obj, IList<object> values)
        {
            var block = (Block)values[0];
            IList list = (IList)obj;

            foreach (var value in list)
                block.Apply(new object[] { value });

            return obj;
        }
    }
}
