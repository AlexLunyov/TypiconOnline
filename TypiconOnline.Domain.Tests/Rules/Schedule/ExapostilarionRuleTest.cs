using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Query.Books;
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
            var dbContext = TypiconDbContextFactory.Create();

            var typiconEntity = dbContext.Set<TypiconVersion>().First(c => c.Id == 1);

            ServiceSequenceHandler handler = new ServiceSequenceHandler();

            string xml = TestDataXmlReader.GetXmlString("ExapostilarionRuleTest.xml");

            DateTime date = new DateTime(2017, 11, 09);

            MenologyRule rule = typiconEntity.GetMenologyRule(date);
            rule.RuleDefinition = xml;

            handler.Settings.Menologies = rule.DayWorships.ToList();
            handler.Settings.Date = date;

            OktoikhDay oktoikhDay = DataQueryProcessorFactory.Instance.Process(new OktoikhDayQuery(date));

            handler.Settings.OktoikhDay = oktoikhDay;

            var ruleContainer = TestRuleSerializer.Deserialize<ExapostilarionRule>(xml);
            ruleContainer.Interpret(handler);

            Assert.AreEqual(3, ruleContainer.Structure.Ymnis.Count);
            Assert.IsNotNull(ruleContainer.Structure.Theotokion);
        }
    }
}
