using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JetBrains.Annotations;

namespace TypiconOnline.Domain.Query.Tests
{
    /// <summary>
    /// Summary description for AnnotationsTest
    /// </summary>
    [TestClass]
    public class AnnotationsTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            TestClass test = new TestClass(null);

            Assert.IsNotNull(test);
        }

        private class TestClass
        {
            public string Name { get; }
            public TestClass([NotNull] string name)
            {
                Name = name;
            }
        }
    }

    
}
