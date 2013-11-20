namespace RubySharp.Core.Tests.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Functions;
    using RubySharp.Core.Language;

    [TestClass]
    public class NativeClassFunctionTests
    {
        private INativeFunction func = new NativeClassFunction();

        [TestMethod]
        public void GetFixnumForInteger()
        {
            var result = func.Apply(1, null);

            Assert.IsNotNull(result);
            Assert.AreEqual(FixnumClass.Instance, result);
        }
    }
}
