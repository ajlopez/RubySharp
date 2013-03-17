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

        [TestMethod]
        public void SetValueAtParentGetValue()
        {
            Context parent = new Context();
            Context context = new Context(parent);

            parent.SetValue("one", 1);
            Assert.AreEqual(1, context.GetValue("one"));
        }

        [TestMethod]
        public void GetLocalNames()
        {
            Context parent = new Context();
            Context context = new Context(parent);

            parent.SetValue("one", 1);
            context.SetValue("two", 2);
            context.SetValue("three", 3);

            var result = context.GetLocalNames();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Contains("two"));
            Assert.IsTrue(result.Contains("three"));
        }
    }
}
