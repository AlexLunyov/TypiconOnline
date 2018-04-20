using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Repository.EF;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class YmnosStructureRuleTest
    {
        [Test]
        public void YmnosStructureRule_FromRealDB()
        {
            var unitOfWork = UnitOfWorkFactory.Create();

            var typiconEntity = unitOfWork.Repository<TypiconEntity>().Get(c => c.Id == 1);

            ServiceSequenceHandler handler = new ServiceSequenceHandler()
            {
                Settings = new RuleHandlerSettings() { Language = LanguageSettingsFactory.Create("cs-ru") }
            };

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.Read("YmnosStructureRuleTest.xml");

            //Дата --11-09 exists - true
            DateTime date = new DateTime(2017, 11, 09);

            MenologyRule rule = typiconEntity.GetMenologyRule(date);
            rule.RuleDefinition = xml;

            handler.Settings.TypiconRule = rule;
            handler.Settings.DayWorships = rule.DayWorships;
            handler.Settings.Date = date;

            var bookStorage = BookStorageFactory.Create();

            OktoikhDay oktoikhDay = bookStorage.Oktoikh.Get(date);

            handler.Settings.OktoikhDay = oktoikhDay;

            var ruleContainer = rule.GetRule<SedalenRule>(TestRuleSerializer.Root);
            ruleContainer.Interpret(handler);

            Assert.AreEqual(3, ruleContainer.Structure.YmnosStructureCount);
            Assert.Pass(rule.GetRule<SedalenRule>(TestRuleSerializer.Root).Structure.YmnosStructureCount.ToString());
        }
    }
}
