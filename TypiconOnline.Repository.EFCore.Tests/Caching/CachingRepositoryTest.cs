using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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

        private string Lambda(Expression<Func<TypiconEntity, bool>> predicate)
        {
            return predicate.Body.ToString();
        }
    }
}
