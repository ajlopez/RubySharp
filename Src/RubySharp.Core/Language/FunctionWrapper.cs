namespace RubySharp.Core.Language
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using RubySharp.Core.Functions;

    public class FunctionWrapper
    {
        private IFunction function;
        private Context context;

        public FunctionWrapper(IFunction function, Context context)
        {
            this.function = function;
            this.context = context;
        }

        protected IFunction Function { get { return this.function; } }

        protected Context Context { get { return this.context; } }

        protected DynamicObject Self { get { return this.context == null ? null : this.context.Self; } }

        public virtual ThreadStart CreateThreadStart()
        {
            return new ThreadStart(this.DoAction);
        }

        public virtual Delegate CreateActionDelegate()
        {
            return Delegate.CreateDelegate(typeof(Action), this, "DoAction");
        }

        public virtual Delegate CreateFunctionDelegate()
        {
            return Delegate.CreateDelegate(typeof(Func<object>), this, "DoFunction");
        }

        private object DoFunction()
        {
            return this.function.Apply(null, this.context, null);
        }

        private void DoAction()
        {
            this.function.Apply(null, this.context, null);
        }
    }

    public class FunctionWrapper<TR, TD> : FunctionWrapper
    {
        public FunctionWrapper(IFunction function, Context context)
            : base(function, context)
        {
        }

        public override Delegate CreateFunctionDelegate()
        {
            return Delegate.CreateDelegate(typeof(TD), this, "DoFunction");
        }

        public override Delegate CreateActionDelegate()
        {
            return Delegate.CreateDelegate(typeof(TD), this, "DoAction");
        }

        public TR DoFunction()
        {
            return (TR)this.Function.Apply(null, this.Context, null);
        }

        public void DoAction()
        {
            this.Function.Apply(null, this.Context, null);
        }
    }

    public class FunctionWrapper<T1, TR, TD> : FunctionWrapper
    {
        public FunctionWrapper(IFunction function, Context context)
            : base(function, context)
        {
        }

        public override Delegate CreateFunctionDelegate()
        {
            return Delegate.CreateDelegate(typeof(TD), this, "DoFunction");
        }

        public override Delegate CreateActionDelegate()
        {
            return Delegate.CreateDelegate(typeof(TD), this, "DoAction");
        }

        public TR DoFunction(T1 t1)
        {
            return (TR)this.Function.Apply(this.Self, this.Context, new object[] { t1 });
        }

        public void DoAction(T1 t1)
        {
            this.Function.Apply(this.Self, this.Context, new object[] { t1 });
        }
    }
    
    public class FunctionWrapper<T1, T2, TR, TD> : FunctionWrapper
    {
        public FunctionWrapper(IFunction function, Context context)
            : base(function, context)
        {
        }

        public override Delegate CreateFunctionDelegate()
        {
            return Delegate.CreateDelegate(typeof(TD), this, "DoFunction");
        }

        public override Delegate CreateActionDelegate()
        {
            return Delegate.CreateDelegate(typeof(TD), this, "DoAction");
        }

        public TR DoFunction(T1 t1, T2 t2)
        {
            return (TR)this.Function.Apply(this.Self, this.Context, new object[] { t1, t2 });
        }

        public void DoAction(T1 t1, T2 t2)
        {
            this.Function.Apply(this.Self, this.Context, new object[] { t1, t2 });
        }
    }
    
    public class FunctionWrapper<T1, T2, T3, TR, TD> : FunctionWrapper
    {
        public FunctionWrapper(IFunction function, Context context)
            : base(function, context)
        {
        }

        public override Delegate CreateFunctionDelegate()
        {
            return Delegate.CreateDelegate(typeof(TD), this, "DoFunction");
        }

        public override Delegate CreateActionDelegate()
        {
            return Delegate.CreateDelegate(typeof(TD), this, "DoAction");
        }

        public TR DoFunction(T1 t1, T2 t2, T3 t3)
        {
            return (TR)this.Function.Apply(this.Self, this.Context, new object[] { t1, t2, t3 });
        }

        public void DoAction(T1 t1, T2 t2, T3 t3)
        {
            this.Function.Apply(this.Self, this.Context, new object[] { t1, t2, t3 });
        }
    }
}
