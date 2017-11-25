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
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Repository.EF;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class YmnosStructureRuleTest
    {
        

        [Test]
        public void YmnosStructureRule_FromRealDB()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();
            BookStorage.Instance = BookStorageFactory.Create();
            GetTypiconEntityResponse resp = new TypiconEntityService(_unitOfWork).GetTypiconEntity(1);
            TypiconEntity typiconEntity = resp.TypiconEntity;

            ServiceSequenceHandler handler = new ServiceSequenceHandler() { Settings = new RuleHandlerSettings() { Language = "cs-ru" } };

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.Read("YmnosStructureRuleTest.xml");

            //Дата --11-09 exists - true
            DateTime date = new DateTime(2017, 11, 09);

            MenologyRule rule = typiconEntity.GetMenologyRule(date);
            rule.RuleDefinition = xml;

            handler.Settings.Rule = rule;
            handler.Settings.DayWorships = rule.DayWorships;

            OktoikhDay oktoikhDay = BookStorage.Instance.Oktoikh.Get(date);

            handler.Settings.OktoikhDay = oktoikhDay;

            rule.GetRule(TestRuleSerializer.Root).Interpret(date, handler);

            SedalenRuleViewModel model = rule.GetRule<SedalenRule>(TestRuleSerializer.Root).CreateViewModel(handler) as SedalenRuleViewModel;

            Assert.AreEqual(3, rule.GetRule<SedalenRule>(TestRuleSerializer.Root).Structure.YmnosStructureCount);
            Assert.Pass(rule.GetRule<SedalenRule>(TestRuleSerializer.Root).Structure.YmnosStructureCount.ToString());
        }
    }
}
