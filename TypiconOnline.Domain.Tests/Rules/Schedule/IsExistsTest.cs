using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using TypiconOnline.AppServices.Migration;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class IsExistsTest : TypiconHavingTestBase
    {
        [Test]
        public void IsExists_Test()
        {
            ServiceSequenceHandler handler = new ServiceSequenceHandler();

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            var reader = new FileReader(folderPath);
            string xml = reader.Read("IsExists.xml");

            //Дата --01-16 exists - false
            DateTime date = new DateTime(2017, 01, 16);

            MenologyRule rule = TypiconVersion.GetMenologyRule(date);
            rule.RuleDefinition = xml;

            handler.Settings.Menologies = rule.DayWorships.ToList();
            handler.Settings.Date = date;

            rule.GetRule<ExecContainer>(Serializer).Interpret(handler);

            var model = handler.ActualWorshipChildElements;

            //EktenisViewModel model = rule.GetRule<EktenisRule>(TestRuleSerializer.Root).CreateViewModel(handler) as EktenisViewModel;

            Assert.AreEqual(1, model.Count);

            //Дата --02-09 exists - true
            date = new DateTime(2017, 02, 09);

            rule = TypiconVersion.GetMenologyRule(date);
            rule.RuleDefinition = xml;

            handler.Settings.Menologies = rule.DayWorships.ToList();
            handler.Settings.Date = date;

            rule.GetRule<ExecContainer>(Serializer).Interpret(handler);

            model = handler.ActualWorshipChildElements;

            //model = rule.GetRule<EktenisRule>(TestRuleSerializer.Root).CreateViewModel(handler) as EktenisViewModel;

            Assert.AreEqual(2, model.Count);
        }
    }
}
