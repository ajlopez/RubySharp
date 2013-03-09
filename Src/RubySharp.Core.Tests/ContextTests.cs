namespace RubySharp.Core.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ContextTests
    {
        [TestMethod]
        public void GetUndefinedValueAsNull()
        {
            Context context = new Context();

            Assert.IsNull(context.GetValue("foo"));
        }

        [TestMethod]
        public void SetAndGetValue()
        {
            Context context = new Context();

            context.SetValue("one", 1);
            Assert.AreEqual(1, context.GetValue("one"));
        }
    }
}
