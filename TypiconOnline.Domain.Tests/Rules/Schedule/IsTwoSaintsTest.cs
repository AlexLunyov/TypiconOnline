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
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Repository.EF;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class IsTwoSaintsTest
    {
        [Test]
        public void IsTwoSaints_Test()
        {
            var unitOfWork = UnitOfWorkFactory.Create();

            var typiconEntity = unitOfWork.Repository<TypiconEntity>().Get(c => c.Id == 1);

            ServiceSequenceHandler handler = new ServiceSequenceHandler()
            {
                Settings = new RuleHandlerSettings() { Language = LanguageSettingsFactory.Create("cs-ru"), Date = DateTime.Today }
            };

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.Read("IsTwoSaints_Simple.xml");

            //Ektenis element = RuleFactory.CreateElement(xml) as Ektenis;


            //Минея - попразднество, 1 святой
            MenologyRule rule = typiconEntity.GetMenologyRule(new DateTime(2017, 09, 28));
            rule.RuleDefinition = xml;

            handler.Settings.TypiconRule = rule;
            handler.Settings.DayWorships = rule.DayWorships;

            rule.GetRule(TestRuleSerializer.Root).Interpret(handler);

            var model = handler.GetResult();

            //EktenisViewModel model = rule.GetRule<EktenisRule>(TestRuleSerializer.Root).CreateViewModel(handler) as EktenisViewModel;

            Assert.AreEqual(1, model.FirstOrDefault()?.ChildElements.Count);

            //Триодь, Минея - 1 святой
            rule = typiconEntity.GetMenologyRule(new DateTime(2017, 3, 16));
            rule.RuleDefinition = xml;

            TriodionRule triodRule = typiconEntity.GetTriodionRule(-20);

            rule.DayWorships.AddRange(triodRule.DayWorships);

            handler.Settings.TypiconRule = rule;
            handler.Settings.DayWorships = rule.DayWorships;

            handler.ClearResult();
            rule.GetRule(TestRuleSerializer.Root).Interpret(handler);

            model = handler.GetResult();

            //model = rule.GetRule<EktenisRule>(TestRuleSerializer.Root).CreateViewModel(handler) as EktenisViewModel;

            Assert.AreEqual(1, model.FirstOrDefault()?.ChildElements.Count);

            //а теперь находим правило НЕ праздничное, 2 святых
            rule = typiconEntity.GetMenologyRule(new DateTime(2017, 5, 31));
            rule.RuleDefinition = xml;

            handler.Settings.TypiconRule = rule;
            handler.Settings.DayWorships = rule.DayWorships;

            handler.ClearResult();
            rule.GetRule(TestRuleSerializer.Root).Interpret(handler);

            model = handler.GetResult();

            //model = rule.GetRule<EktenisRule>(TestRuleSerializer.Root).CreateViewModel(handler) as EktenisViewModel;

            Assert.AreEqual(2, model.FirstOrDefault()?.ChildElements.Count);
        }
    }
}
