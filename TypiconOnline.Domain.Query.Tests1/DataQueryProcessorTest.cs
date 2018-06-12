using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Query.Tests
{
    [TestClass]
    public class DataQueryProcessorTest
    {
        [TestMethod]
        public void DataQueryProcessor_Create()
        {
            var processor = DataQueryProcessorFactory.Create();

            var easterDate = processor.Process(new CurrentEasterQuery(2018));

            Assert.AreEqual(2018, easterDate.Year);
        }
    }
}
