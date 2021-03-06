﻿namespace RubySharp.Core.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using RubySharp.Core.Exceptions;
    using RubySharp.Core.Functions;

    public class SelfExpression : BaseExpression, INamedExpression
    {
        private static int hashcode = typeof(SelfExpression).GetHashCode();

        public SelfExpression()
        {
        }

        public IExpression TargetExpression { get { return null; } }

        public string Name { get { return "self"; } }

        public override object Evaluate(Context context)
        {
            return context.Self;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return obj is SelfExpression;
        }

        public override int GetHashCode()
        {
            return hashcode;
        }
    }
}
