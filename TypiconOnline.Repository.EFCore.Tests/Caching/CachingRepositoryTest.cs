using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Days;
using TypiconOnline.Repository.EFCore.DataBase;
using TypiconOnline.Repository.EFCore.Tests.Common;

namespace TypiconOnline.Repository.EFCore.Tests.Caching
{
    [TestClass]
    public class CachingRepositoryTest
    {
        [TestMethod]
        public void CachingRepository_FromCache_Twice()
        {
            var uof = CachingUOFFactory.Create();

            var items = uof.Repository<MenologyDay>().GetAll(c => (c.Id < 200)).ToList();

            items = uof.Repository<MenologyDay>().GetAll(c => (c.Id < 200)).ToList();

            Assert.IsNotNull(items);
        }

        [TestMethod]
        public void CachingRepository_SaveChanges()
        {
            var uof = CachingUOFFactory.Create();

            var items = uof.Repository<MenologyDay>().GetAll(c => (c.Id < 200)).ToList();

            items = uof.Repository<MenologyDay>().GetAll(c => (c.Id < 200)).ToList();

            Assert.IsNotNull(items);
        }

        //[TestMethod]
        //public void CachingRepository_MockEasters()
        //{
        //    var uof = EFCacheUOFFactory.Create(MockEastersRepositoryFactory.Create());

        //    var items = uof.Repository<EasterItem>().GetAll(c => c.Date > DateTime.MinValue).ToList();

        //    items.ForEach(c =>
        //    {
        //        c.Date = DateTime.Now;
        //        uof.Repository<EasterItem>().Update(c);
        //    });

        //    uof.SaveChanges();

        //    items = uof.Repository<EasterItem>().GetAll().ToList();

        //    var found = items.FirstOrDefault(c => c.Date.Year != DateTime.Now.Year);

        //    Assert.IsNotNull(found);
        //}

        //[TestMethod]
        //public void CachingRepository_MockEaster()
        //{
        //    var uof = EFCacheUOFFactory.Create(MockEastersRepositoryFactory.Create());

        //    var response = uof.Repository<EasterItem>().Get(c => c.Date.Year == 2018);

        //    response.Date = DateTime.MinValue;

        //    uof.SaveChanges();

        //    response = uof.Repository<EasterItem>().Get(c => c.Date > DateTime.MinValue);

        //    Assert.AreNotEqual(response.Date, DateTime.MinValue);
        //}

        [TestMethod]
        public void CachingRepository_DetectChanges()
        {
            var uof = CachingUOFFactory.Create();

            var item = uof.Repository<MenologyDay>().Get(c => (c.Id == 200));

            string exp = item.Date.Expression;

            item.Date.Expression = exp + "11";

            uof.Repository<MenologyDay>().Update(item);

            uof.SaveChanges();

            Assert.IsNotNull(item);
        }
    }
}
