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

            var manager = new KathismasMigrationManager(EFCoreBookStorageFactory.Create().Psalter);
            manager.MigrateKathismas(new PsalterRuReader(folderPath, "cs-ru"), typicon);

            Assert.AreEqual(1, typicon.Kathismas.Count);
        }

        [Test]
        public void KathismasMigration_CS_RU_Full()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\PsalterMigration");

            var typicon = new TypiconEntity();

            var manager = new KathismasMigrationManager(EFCoreBookStorageFactory.Create().Psalter);
            manager.MigrateKathismas(new PsalterRuReader(folderPath, "cs-ru"), typicon);

            Assert.AreEqual(20, typicon.Kathismas.Count);
        }

        /// <summary>
        /// Проверяем ссылки на стартовый и конечный стихи в 17 кафизме
        /// </summary>
        [Test]
        public void KathismasMigration_CS_RU_17()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\PsalterMigration\2");

            var typicon = new TypiconEntity();

            var manager = new KathismasMigrationManager(EFCoreBookStorageFactory.Create().Psalter);
            manager.MigrateKathismas(new PsalterRuReader(folderPath, "cs-ru"), typicon);

            var psalmLink = typicon.Kathismas[0].SlavaElements[0].PsalmLinks[0];

            Assert.AreEqual(1, psalmLink.StartStihos);
            Assert.AreEqual(72, psalmLink.EndStihos);

            psalmLink = typicon.Kathismas[0].SlavaElements[1].PsalmLinks[0];

            Assert.AreEqual(73, psalmLink.StartStihos);
            Assert.AreEqual(131, psalmLink.EndStihos);

            psalmLink = typicon.Kathismas[0].SlavaElements[2].PsalmLinks[0];

            Assert.AreEqual(132, psalmLink.StartStihos);
            Assert.AreEqual(176, psalmLink.EndStihos);
        }

        [Test]
        public void KathismasMigration_CS_RU_SaveChanges()
        {
            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\PsalterMigration\1");

            var typicon = new TypiconEntity();

            var unitOfWork = UnitOfWorkFactory.Create();

            var manager = new KathismasMigrationManager(EFCoreBookStorageFactory.Create(unitOfWork).Psalter);
            manager.MigrateKathismas(new PsalterRuReader(folderPath, "cs-ru"), typicon);

            unitOfWork.Repository<TypiconEntity>().Insert(typicon);

            unitOfWork.SaveChanges();

            Assert.AreEqual(1, typicon.Kathismas.Count);
        }

        /// <summary>
        /// Проверяет, все ли ссылки на Псалмы имеют нулевые стартовые и конечные стихи
        /// </summary>
        //[Test]
        //public void KathismasMigration_CS_RU_PsalmLinksNullStihos()
        //{
        //    var typicon = new TypiconEntity();

        //    string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\PsalterMigration\1");

        //    var manager = new KathismasMigrationManager(EFCoreBookStorageFactory.Create().Psalter);

        //    manager.MigrateKathismas(new PsalterRuReader(folderPath, "cs-ru"), typicon, true);

        //    //вычисляем количество Ссылок на Псалмы в кафизме
        //    bool isClear = true;
        //    typicon.Kathismas.ForEach(c =>
        //    {
        //        c.SlavaElements.ForEach(d =>
        //        {
        //            d.PsalmLinks.ForEach(e =>
        //            {
        //                isClear &= e.StartStihos == null && e.EndStihos == null;
        //            });
        //        });
        //    });

        //    Assert.IsTrue(isClear);
        //}

        [Test]
        public void KathismasMigration_CS_RU_CheckFirstPsalmLinks()
        {
            var typicon = new TypiconEntity();

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\PsalterMigration\1");

            var manager = new KathismasMigrationManager(EFCoreBookStorageFactory.Create().Psalter);

            manager.MigrateKathismas(new PsalterRuReader(folderPath, "cs-ru"), typicon);

            //вычисляем количество Ссылок на Псалмы в кафизме
            int count = 0;
            typicon.Kathismas.ForEach(c =>
            {
                c.SlavaElements.ForEach(d =>
                {
                    count += d.PsalmLinks.Count;
                });
            });

            Assert.AreEqual(8, count);
        }

        [Test]
        public void KathismasMigration_CS_RU_DoubleMigration()
        {
            var typicon = new TypiconEntity();

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\PsalterMigration\1");

            var manager = new KathismasMigrationManager(EFCoreBookStorageFactory.Create().Psalter);

            manager.MigrateKathismas(new PsalterRuReader(folderPath, "cs-ru"), typicon);

            manager.MigrateKathismas(new PsalterCsReader(folderPath, "cs-cs"), typicon, true);

            //вычисляем количество Ссылок на Псалмы в кафизме
            int count = 0;
            typicon.Kathismas.ForEach(c =>
            {
                c.SlavaElements.ForEach(d =>
                {
                    count += d.PsalmLinks.Count;
                });
            });

            Assert.AreEqual(8, count);

            //Проверяем наличие двух языков у наименования Номера кафизм
            Assert.AreEqual(2, typicon.Kathismas[0].NumberName.Languages.Count());
        }
    }
}
