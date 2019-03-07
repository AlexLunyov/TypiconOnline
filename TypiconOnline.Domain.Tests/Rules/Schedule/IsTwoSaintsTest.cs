using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class IsTwoSaintsTest : TypiconHavingTestBase
    {
        [Test]
        public void IsTwoSaints_Test()
        {
            ServiceSequenceHandler handler = new ServiceSequenceHandler()
            {
                Settings = new RuleHandlerSettings() { Language = LanguageSettingsFactory.Create("cs-ru"), Date = DateTime.Today }
            };

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.Read("IsTwoSaints_Simple.xml");

            //Ektenis element = RuleFactory.CreateElement(xml) as Ektenis;


            //Минея - попразднество, 1 святой
            MenologyRule rule = TypiconVersion.GetMenologyRule(new DateTime(2017, 09, 28));
            rule.RuleDefinition = xml;

            handler.Settings.DayWorships = rule.DayWorships;

            rule.GetRule<ExecContainer>(Serializer).Interpret(handler);

            var model = handler.GetResult();

            //EktenisViewModel model = rule.GetRule<EktenisRule>(TestRuleSerializer.Root).CreateViewModel(handler) as EktenisViewModel;

            Assert.AreEqual(1, model.FirstOrDefault()?.ChildElements.Count);

            //Триодь, Минея - 1 святой
            rule = TypiconVersion.GetMenologyRule(new DateTime(2017, 3, 16));
            rule.RuleDefinition = xml;

            TriodionRule triodRule = TypiconVersion.GetTriodionRule(-20);

            rule.DayWorships.AddRange(triodRule.DayWorships);

            handler.Settings.DayWorships = rule.DayWorships;

            handler.ClearResult();
            rule.GetRule<ExecContainer>(Serializer).Interpret(handler);

            model = handler.GetResult();

            //model = rule.GetRule<EktenisRule>(TestRuleSerializer.Root).CreateViewModel(handler) as EktenisViewModel;

            Assert.AreEqual(1, model.FirstOrDefault()?.ChildElements.Count);

            //а теперь находим правило НЕ праздничное, 2 святых
            rule = TypiconVersion.GetMenologyRule(new DateTime(2017, 5, 31));
            rule.RuleDefinition = xml;

            handler.Settings.DayWorships = rule.DayWorships;

            handler.ClearResult();
            rule.GetRule<ExecContainer>(Serializer).Interpret(handler);

            model = handler.GetResult();

            //model = rule.GetRule<EktenisRule>(TestRuleSerializer.Root).CreateViewModel(handler) as EktenisViewModel;

            Assert.AreEqual(2, model.FirstOrDefault()?.ChildElements.Count);
        }
    }
}
