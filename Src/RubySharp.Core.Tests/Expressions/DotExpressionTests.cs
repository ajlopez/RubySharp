namespace RubySharp.Core.Tests.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core.Expressions;
    using RubySharp.Core.Language;

    [TestClass]
    public class DotExpressionTests
    {
        [TestMethod]
        public void EvaluateObjectMethod()
        {
            Machine machine = new Machine();
            machine.ExecuteText("class MyClass;def foo;3;end;end");

            var dclass = (DefinedClass)machine.RootContext.GetValue("MyClass");

            var myobj = dclass.CreateInstance();

            DotExpression expression = new DotExpression(new ConstantExpression(myobj), "foo");

            var result = expression.Evaluate(null);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result);
        }
    }
}
