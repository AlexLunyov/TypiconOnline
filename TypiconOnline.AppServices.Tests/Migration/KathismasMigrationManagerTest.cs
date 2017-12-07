using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Migration.Psalter;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.AppServices.Tests.Migration
{
    [TestFixture]
    public class KathismasMigrationManagerTest
    {
        [Test]
        public void KathismasMigration_CS_RU()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\PsalterMigration\1");

            var typicon = new TypiconEntity();

            var manager = new KathismasMigrationManager(BookStorageFactory.Create().Psalter);
            manager.MigrateKathismas(new PsalterRuReader(folderPath, "cs-ru"), typicon);

            Assert.AreEqual(1, typicon.Kathismas.Count);
        }

        [Test]
        public void KathismasMigration_CS_RU_Full()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\PsalterMigration");

            var typicon = new TypiconEntity();

            var manager = new KathismasMigrationManager(BookStorageFactory.Create().Psalter);
            manager.MigrateKathismas(new PsalterRuReader(folderPath, "cs-ru"), typicon);

            Assert.AreEqual(20, typicon.Kathismas.Count);
        }
    }
}
