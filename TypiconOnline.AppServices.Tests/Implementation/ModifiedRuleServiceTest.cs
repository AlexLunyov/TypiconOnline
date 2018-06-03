using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Implementation
{
    [TestFixture]
    public class ModifiedRuleServiceTest
    {
        /// <summary>
        /// Создаем TypiconEntity
        /// Добавляем один Sign с определением правил для ModifyDay
        /// Добавляем MenologyRule с определением правила как AsAddition=true
        /// </summary>
        [Test]
        public void ModifiedRuleService_Test()
        {
            //var typicon = new TypiconEntity();

            //var sign = new Sign
            //{
            //    Owner = typicon,
            //    RuleDefinition = TestDataXmlReader.GetXmlString("ModifiedRuleService_Test1.xml")
            //};

            //typicon.Signs.Add(sign);

            //var menologyRule = new MenologyRule()
            //{
            //    Owner = typicon,
            //    Template = sign,
            //    IsAddition = true,
            //    Date = new ItemDate(1, 1),
            //    DateB = new ItemDate(1, 1),
            //    RuleDefinition = TestDataXmlReader.GetXmlString("ModifiedRuleService_Test2.xml")
            //};

            //typicon.MenologyRules.Add(menologyRule);

            //var service = new ModifiedRuleService(GetUnitOfWork(), new ModifiedYearFactory());

            //service.GetModifiedRules(typicon.Id, new DateTime(2018, 1, 1));

            //Assert.AreEqual(3, typicon.ModifiedYears.FirstOrDefault().ModifiedRules.Count);
        }

        private IUnitOfWork GetUnitOfWork()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(c => c.Repository<TypiconEntity>().Update(It.IsAny<TypiconEntity>()));
            mockUnitOfWork.Setup(c => c.SaveChanges());

            return mockUnitOfWork.Object;
        }
    }
}
