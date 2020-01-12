using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.AppServices.Migration.Books;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Migration
{
    [TestFixture]
    public class BooksXmlExportManagerTest
    {
        [Test]
        public void BooksXmlExportManagerTest_Export()
        {
            var dbContext = TypiconDbContextFactory.Create();

            var manager = new BooksXmlExportManager(dbContext);

            var result = manager.Export();

            Assert.IsNotNull(result.Value);
        }

        [Test]
        public void BooksXmlExportManagerTest_Serialize()
        {
            var dbContext = TypiconDbContextFactory.Create();

            var booksProjector = new BooksProjector(dbContext);

            var projection = booksProjector.Project();

            var serializer = new TypiconSerializer();

            var xml = serializer.Serialize(projection);

            Assert.IsNotNull(xml);
        }
    }
}
