using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations.Books;
using TypiconOnline.AppServices.Migration.Psalter;
using TypiconOnline.Repository.EF;

namespace TypiconOnline.AppServices.Tests.Migration
{
    [TestFixture]
    public class PsalmsMigrationManagerTest
    {
        [Test]
        public void PsalmsMigrationManager_Test()
        {
            var uof = new EFUnitOfWork();
            var service = new PsalterService(uof);

            var manager = new PsalmsMigrationManager(service);

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\PsalterMigration");

            manager.MigratePsalms(new PsalterRuReader(folderPath, "cs-ru"));

            //uof.Commit();
        }
    }
}
