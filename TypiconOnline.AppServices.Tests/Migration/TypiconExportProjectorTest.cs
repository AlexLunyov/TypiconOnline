using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Migration.Psalter;
using TypiconOnline.AppServices.Migration.Typicon;
using TypiconOnline.Domain;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.Rules.Serialization;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Migration
{
    [TestFixture]
    public class TypiconExportProjectorTest
    {
        [Test]
        public void TypiconExportProjectorTest_Export()
        {
            var dbContext = TypiconDbContextFactory.Create();

            var typicon = dbContext.Set<TypiconVersion>().FirstOrDefault();

            var manager = new TypiconExportProjector();

            var projection = manager.Project(typicon);

            Assert.IsNotNull(projection.Value);
        }

        [Test]
        public void TypiconExportProjectorTest_SaveToFile()
        {
            var dbContext = TypiconDbContextFactory.Create();

            var typicon = dbContext.Set<TypiconVersion>().FirstOrDefault();

            var manager = new TypiconExportProjector();

            var projection = manager.Project(typicon);

            var xml = new TypiconSerializer().Serialize(projection.Value);

            Assert.IsNotNull(xml);
        }

        [Test]
        public void TypiconImportProjectorTest_Import()
        {
            var manager = new TypiconImportProjector(
                new CollectorSerializerRoot(
                    QueryProcessorFactory.Create()
                    , new TypiconSerializer()));

            var xml = TestDataXmlReader.GetXmlString("TypiconVersion.xml");

            var projection = new TypiconSerializer().Deserialize<TypiconVersionProjection>(xml);

            var entity = manager.Project(projection);

            var vars = entity.Value.Versions.First().TypiconVariables;

            Assert.AreEqual(3, vars.Count);
            Assert.Pass(string.Join('\n', vars.Select(c => c.Name)));
        }

        [Test]
        public void TypiconImportProjectorTest_Import_To_Db()
        {
            var manager = new TypiconImportProjector(
                new CollectorSerializerRoot(
                    QueryProcessorFactory.Create()
                    , new TypiconSerializer()));

            var xml = TestDataXmlReader.GetXmlString("TypiconVersion.xml");

            var projection = new TypiconSerializer().Deserialize<TypiconVersionProjection>(xml);

            var entity = manager.Project(projection);

            var dbContext = TypiconDbContextFactory.Create();

            dbContext.Set<TypiconEntity>().Add(entity.Value);
            int i = dbContext.SaveChanges();

            Assert.Greater(i, 0);
            Assert.Pass(i.ToString());
        }
    }
}
