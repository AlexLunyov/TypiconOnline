using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using TypiconOnline.Domain.Days;
using TypiconOnline.Repository.EFCore.Tests.Common;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Books.Easter;

namespace TypiconOnline.Repository.EFCore.Tests.Caching
{
    [TestClass]
    public class EFCacheRepositoryTest
    {
        [TestMethod]
        public void EFCacheRepository_Lambda()
        {
            var uof = EFCacheUOFFactory.Create();

            var items = uof.Repository<MenologyDay>().GetAll(c => (c.Id < 200)).ToList();

            var items2 = uof.Repository<MenologyDay>().GetAll(c => (c.Id > 180 && c.Id < 220)).ToList();

            Assert.IsNotNull(items2);
            Assert.IsTrue(items2.Count > 0);
        }

        [TestMethod]
        public void EFCacheRepository_TypiconEntity_Twice()
        {
            var uof = EFCacheUOFFactory.Create();

            var typiconEntity = uof.Repository<TypiconEntity>().GetAll(c => c.Name == "Типикон").ToList();

            typiconEntity = uof.Repository<TypiconEntity>().GetAll(c => c.Name == "Типикон").ToList();

            typiconEntity = uof.Repository<TypiconEntity>().GetAll(c => c.Id == 1).ToList();

            Assert.IsNotNull(typiconEntity);
        }

        [TestMethod]
        public void EFCacheRepository_EasterItems()
        {
            var uof = EFCacheUOFFactory.Create();

            var response = uof.Repository<EasterItem>().GetAll().ToList();

            Assert.IsNotNull(response);
        }
    }
}
