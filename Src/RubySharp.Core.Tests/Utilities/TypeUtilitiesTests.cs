namespace RubySharp.Core.Tests.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RubySharp.Core;
    using RubySharp.Core.Compiler;
    using RubySharp.Core.Exceptions;
    using RubySharp.Core.Tests.Classes;
    using RubySharp.Core.Utilities;

    [TestClass]
    public class TypeUtilitiesTests
    {
        [TestMethod]
        public void GetTypeByName()
        {
            Type type = TypeUtilities.GetType("System.Int32");

            Assert.IsNotNull(type);
            Assert.AreEqual(type, typeof(int));
        }

        [TestMethod]
        public void GetTypeStoredInEnvironment()
        {
            Context environment = new Context();

            environment.SetLocalValue("int", typeof(int));

            Type type = TypeUtilities.GetType(environment, "int");

            Assert.IsNotNull(type);
            Assert.AreEqual(type, typeof(int));
        }

        [TestMethod]
        public void GetTypeInAnotherAssembly()
        {
            Type type = TypeUtilities.GetType(new Context(), "System.Data.DataSet");

            Assert.IsNotNull(type);
            Assert.AreEqual(type, typeof(System.Data.DataSet));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Unknown Type 'Foo.Bar'")]
        public void RaiseIfUnknownType()
        {
            TypeUtilities.GetType(new Context(), "Foo.Bar");
        }

        [TestMethod]
        public void AsType()
        {
            Assert.IsNotNull(TypeUtilities.AsType("System.IO.File"));
            Assert.IsNotNull(TypeUtilities.AsType("RubySharp.Core.Tests.Classes.Person"));
            Assert.IsNull(TypeUtilities.AsType("Foo.Bar"));
        }

        [TestMethod]
        public void IsNamespace()
        {
            Assert.IsTrue(TypeUtilities.IsNamespace("System"));
            Assert.IsTrue(TypeUtilities.IsNamespace("RubySharp"));
            Assert.IsTrue(TypeUtilities.IsNamespace("RubySharp.Core"));
            Assert.IsTrue(TypeUtilities.IsNamespace("RubySharp.Core.Language"));
            Assert.IsTrue(TypeUtilities.IsNamespace("System.IO"));
            Assert.IsTrue(TypeUtilities.IsNamespace("System.Data"));

            Assert.IsFalse(TypeUtilities.IsNamespace("Foo.Bar"));
        }

        [TestMethod]
        public void GetTypesByNamespace()
        {
            var types = TypeUtilities.GetTypesByNamespace("System.IO");

            Assert.IsNotNull(types);

            Assert.IsTrue(types.Contains(typeof(System.IO.File)));
            Assert.IsTrue(types.Contains(typeof(System.IO.Directory)));
            Assert.IsTrue(types.Contains(typeof(System.IO.FileInfo)));
            Assert.IsTrue(types.Contains(typeof(System.IO.DirectoryInfo)));

            Assert.IsFalse(types.Contains(typeof(string)));
            Assert.IsFalse(types.Contains(typeof(System.Data.DataSet)));
        }

        [TestMethod]
        public void GetValueFromType()
        {
            Assert.IsFalse((bool)TypeUtilities.InvokeTypeMember(typeof(System.IO.File), "Exists", new object[] { "unknown.txt" }));
        }

        [TestMethod]
        public void GetValueFromEnum()
        {
            Assert.AreEqual(System.UriKind.RelativeOrAbsolute, TypeUtilities.InvokeTypeMember(typeof(System.UriKind), "RelativeOrAbsolute", null));
        }

        [TestMethod]
        public void ParseTokenTypeValues()
        {
            Type type = typeof(RubySharp.Core.Compiler.TokenType);

            Assert.AreEqual(RubySharp.Core.Compiler.TokenType.Name, TypeUtilities.ParseEnumValue(type, "Name"));
            Assert.AreEqual(RubySharp.Core.Compiler.TokenType.Integer, TypeUtilities.ParseEnumValue(type, "Integer"));
            Assert.AreEqual(RubySharp.Core.Compiler.TokenType.Separator, TypeUtilities.ParseEnumValue(type, "Separator"));
        }

        [TestMethod]
        public void RaiseWhenUnknownEnumValue()
        {
            Type type = typeof(RubySharp.Core.Compiler.TokenType);

            try
            {
                TypeUtilities.ParseEnumValue(type, "Spam");
                Assert.Fail("Exception expected");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(ValueError));
                Assert.AreEqual("'Spam' is not a valid value of 'TokenType'", ex.Message);
            }
        }

        [TestMethod]
        public void GetTypeMethod()
        {
            Type type = typeof(TokenType);
            var result = TypeUtilities.GetValue(type, "GetType");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(MethodInfo));
        }

        [TestMethod]
        public void GetTypeStaticMethod()
        {
            Type type = typeof(System.IO.File);
            var result = TypeUtilities.GetValue(type, "Exists");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(MethodInfo));
        }

        [TestMethod]
        public void GetNamesNativeType()
        {
            var result = TypeUtilities.GetNames(typeof(Person));

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IList<string>));

            var names = (IList<string>)result;

            Assert.IsTrue(names.Contains("FirstName"));
            Assert.IsTrue(names.Contains("LastName"));
            Assert.IsTrue(names.Contains("GetName"));
            Assert.IsTrue(names.Contains("NameEvent"));
        }
    }
}
