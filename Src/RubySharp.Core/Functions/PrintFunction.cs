﻿namespace RubySharp.Core.Functions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Language;

    public class PrintFunction : IFunction
    {
        private TextWriter writer;

        public PrintFunction(TextWriter writer)
        {
            this.writer = writer;
        }

        public object Apply(DynamicObject self, Context context, IList<object> values)
        {
            foreach (var value in values)
                this.writer.Write(value);

            return null;
        }
    }
}
