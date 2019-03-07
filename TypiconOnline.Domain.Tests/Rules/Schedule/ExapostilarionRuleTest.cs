using NUnit.Framework;
using System;
using System.IO;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class ExapostilarionRuleTest
    {
        [Test]
        public void ExapostilarionRuleTest_FromRealDB()
        {
            var unitOfWork = UnitOfWorkFactory.Create();

            var typiconEntity = unitOfWork.Repository<TypiconVersion>().Get(c => c.Id == 1);

            ServiceSequenceHandler handler = new ServiceSequenceHandler()
            {
                Settings = new RuleHandlerSettings() { Language = LanguageSettingsFactory.Create("cs-ru") }
            };

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.Read("ExapostilarionRuleTest.xml");

            DateTime date = new DateTime(2017, 11, 09);

            MenologyRule rule = typiconEntity.GetMenologyRule(date);
            rule.RuleDefinition = xml;

            handler.Settings.DayWorships = rule.DayWorships;
            handler.Settings.Date = date;

            var bookStorage = BookStorageFactory.Create();

            OktoikhDay oktoikhDay = bookStorage.Oktoikh.Get(date);

            handler.Settings.OktoikhDay = oktoikhDay;

            var ruleContainer = TestRuleSerializer.Deserialize<ExapostilarionRule>(xml);
            ruleContainer.Interpret(handler);

            Assert.AreEqual(3, ruleContainer.Structure.Ymnis.Count);
            Assert.IsNotNull(ruleContainer.Structure.Theotokion);
        }
    }
}
