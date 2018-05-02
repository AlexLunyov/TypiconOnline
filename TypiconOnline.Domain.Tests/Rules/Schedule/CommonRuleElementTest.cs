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
    public class CommonRuleElementTest
    {
        [Test]
        public void CommonRuleElement_SimplePassing()
        {
            //находим первый попавшийся MenologyRule
            var unitOfWork = UnitOfWorkFactory.Create();

            var typiconEntity = unitOfWork.Repository<TypiconEntity>().Get(c => c.Id == 1);

            MenologyRule rule = typiconEntity.MenologyRules[0];
            ServiceSequenceHandler handler = new ServiceSequenceHandler
            {
                Settings = new RuleHandlerSettings() { Language = LanguageSettingsFactory.Create("cs-ru"), TypiconRule = rule, Date = DateTime.Today }
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
