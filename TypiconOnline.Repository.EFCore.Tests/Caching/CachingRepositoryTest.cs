using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Repository.EFCore.Tests.Common;

namespace TypiconOnline.Repository.EFCore.Tests.Caching
{
    [TestFixture]
    public class CachingRepositoryTest
    {
        [TestCase]
        public void CachingRepository_TypiconEntity()
        {
            var uof = CachingUOFFactory.Create();
            var typiconEntity = uof.Repository<TypiconEntity>().Get(c => c.Id == 1);

            typiconEntity = uof.Repository<TypiconEntity>().Get(c => c.Name == "Типикон");

            Assert.IsNotNull(typiconEntity);
        }

        [TestCase]
        public void CachingRepository_MenologyDay()
        {
            var uof = CachingUOFFactory.Create();

            var items = uof.Repository<MenologyDay>().GetAll(c => c.Id < 200);

            var items2 = uof.Repository<MenologyDay>().GetAll(c => (c.Id > 180 && c.Id < 220));

            Assert.Pass($"MenologyDays count: {items2.Count()}");
            Assert.IsNotNull(items2);
        }
    }
}
