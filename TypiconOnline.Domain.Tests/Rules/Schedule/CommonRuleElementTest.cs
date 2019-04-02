using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class CommonRuleElementTest
    {
        [Test]
        public void CommonRuleElement_SimplePassing()
        {
            //находим первый попавшийся MenologyRule
            var dbContext = TypiconDbContextFactory.Create();

            var typiconEntity = dbContext.Set<TypiconVersion>().First(c => c.Id == 1);

            MenologyRule rule = typiconEntity.MenologyRules[0];
            ServiceSequenceHandler handler = new ServiceSequenceHandler
            {
                Settings = new RuleHandlerSettings() { Date = DateTime.Today, TypiconVersionId = 1 }
            };

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.Read("CommonRuleElement_Simple.xml");

            var element = TestRuleSerializer.Deserialize<WorshipSequence>(xml);

            element.Interpret(handler);

            var model = handler.GetResult();

            //WorshipSequenceViewModel model = new WorshipSequenceViewModel(element, handler);

            Assert.AreEqual(30, model.FirstOrDefault()?.ChildElements.Count);
        }
    }
}
