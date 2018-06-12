using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypiconOnline.Tests.Common;
using TypiconOnline.Domain.Query.Books;

namespace TypiconOnline.Domain.Query.Tests
{
    /// <summary>
    /// Summary description for DataQueryProcessorTest
    /// </summary>
    [TestClass]
    public class DataQueryProcessorTest
    {

        [TestMethod]
        public void DataQueryProcessorTest_Create()
        {
            var processor = DataQueryProcessorFactory.Create();

            var easterDate = processor.Process(new CurrentEasterQuery(2018));

            Assert.AreEqual(2018, easterDate.Year);
        }
    }
}
