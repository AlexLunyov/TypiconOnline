﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations.Books;
using TypiconOnline.AppServices.Migration.Psalter;
using TypiconOnline.Domain.Serialization;
using TypiconOnline.Repository.EFCore;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Migration
{
    [TestFixture]
    public class PsalmsMigrationManagerTest
    {
        [Test]
        public void PsalmsMigrationManager_Test()
        {
            var uof = UnitOfWorkFactory.Create();
            var service = new FakePsalterService(uof);

            var manager = new PsalmsMigrationManager(service, new TypiconSerializer());

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\PsalterMigration");

            manager.MigratePsalms(new PsalterRuReader(folderPath, "cs-ru"));

            Assert.That(service.Psalms.Count(), Is.EqualTo(150));

            //uof.Commit();
        }

        [Test]
        public void PsalmsMigrationManager_CsTest()
        {
            var uof = UnitOfWorkFactory.Create();
            var service = new FakePsalterService(uof);

            var manager = new PsalmsMigrationManager(service, new TypiconSerializer());

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData\PsalterMigration\1");

            manager.MigratePsalms(new PsalterRuReader(folderPath, "cs-ru"));

            manager.MigratePsalms(new PsalterCsReader(folderPath, "cs-cs"));

            //uof.Commit();
        }
    }
}
