using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Domain.Tests.Rules;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Repository.EF;

namespace TypiconOnline.Domain.Tests.Days
{
    [TestFixture]
    public class DayServiceTest
    {
        [Test]
        public void DayServiceTest_Working()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            //BookStorage.Instance = BookStorageFactory.Create();

            TypiconEntity typiconEntity = _unitOfWork.Repository<TypiconEntity>().Get(c => c.Name == "Типикон");

            MenologyRule menologyRule = typiconEntity.GetMenologyRule(new DateTime(2017, 09, 28));

            Assert.NotNull(menologyRule.GetRule(TestRuleSerializer.Root));
        }
    }
}
