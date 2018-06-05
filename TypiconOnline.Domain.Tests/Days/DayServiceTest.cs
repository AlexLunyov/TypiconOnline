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
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Days
{
    [TestFixture]
    public class DayServiceTest
    {
        [Test]
        public void DayServiceTest_Working()
        {
            var unitOfWork = UnitOfWorkFactory.Create();

            var typiconEntity = unitOfWork.Repository<TypiconEntity>().Get(c => c.Id == 1);

            MenologyRule menologyRule = typiconEntity.GetMenologyRule(new DateTime(2017, 09, 28));

            Assert.NotNull(menologyRule.GetRule(TestRuleSerializer.Create()));
        }
    }
}
