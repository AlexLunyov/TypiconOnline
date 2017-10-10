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
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;
using TypiconOnline.Repository.EF;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class CommonRuleElementTest
    {
        [Test]
        public void CommonRuleElement_SimplePassing()
        {
            //находим первый попавшийся MenologyRule
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();
            BookStorage.Instance = BookStorageFactory.Create();
            GetTypiconEntityResponse resp = new TypiconEntityService(_unitOfWork).GetTypiconEntity(1);
            TypiconEntity typiconEntity = resp.TypiconEntity;
            MenologyRule rule = typiconEntity.MenologyRules[0];
            ServiceSequenceHandler handler = new ServiceSequenceHandler();
            handler.Settings = new RuleHandlerSettings() { Language = "cs-ru", Rule = rule };

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.GetXml("CommonRuleElement_Simple.xml");

            ServiceSequence element = RuleFactory.CreateElement(xml) as ServiceSequence;

            element.Interpret(DateTime.Today, handler);

            ServiceSequenceViewModel model = new ServiceSequenceViewModel(element, handler);

            Assert.AreEqual(4, model.ChildElements.Count);
        }
    }
}
