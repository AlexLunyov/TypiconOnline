using NUnit.Framework;
using System;
using System.Collections.Generic;
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
            var service = new PsalterService(new EFUnitOfWork());

            var manager = new PsalmsMigrationManager(service);


        }
    }
}
