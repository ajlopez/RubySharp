namespace RubySharp.Core.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BlockContextTests
    {
        [TestMethod]
        public void UndefinedLocalVariable()
        {
            Context parent = new Context();
            BlockContext context = new BlockContext(parent);

            Assert.IsFalse(context.HasLocalValue("foo"));
        }

        [TestMethod]
        public void ParentLocalVariable()
        {
            Context parent = new Context();
            parent.SetLocalValue("foo", "bar");
            BlockContext context = new BlockContext(parent);

            Assert.IsTrue(context.HasLocalValue("foo"));
            Assert.AreEqual("bar", context.GetLocalValue("foo"));
        }

        [TestMethod]
        public void ChangeParentLocalVariable()
        {
            Context parent = new Context();
            parent.SetLocalValue("foo", "bar");
            BlockContext context = new BlockContext(parent);
            context.SetLocalValue("foo", "newbar");

            Assert.IsTrue(context.HasLocalValue("foo"));
            Assert.AreEqual("newbar", context.GetLocalValue("foo"));
            Assert.AreEqual("newbar", parent.GetLocalValue("foo"));
        }

        [TestMethod]
        public void BlockContextLocalVariable()
        {
            Context parent = new Context();
            BlockContext context = new BlockContext(parent);
            context.SetLocalValue("foo", "bar");

            Assert.IsTrue(context.HasLocalValue("foo"));
            Assert.IsFalse(parent.HasLocalValue("foo"));
            Assert.AreEqual("bar", context.GetLocalValue("foo"));
        }
    }
}
