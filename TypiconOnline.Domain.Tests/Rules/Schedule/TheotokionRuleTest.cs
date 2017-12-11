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

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class TheotokionRuleTest
    {
        [Test]
        public void TheotokionRule_SourceANDplaceMismatch()
        {
            string xmlString = @"<theotokionrule source=""irmologion"" place=""kekragaria"" count=""3"" startfrom=""2""/>";

            var element = TestRuleSerializer.Deserialize<TheotokionRule>(xmlString);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void TheotokionRule_SourceANDplaceMismatch2()
        {
            string xmlString = @"<theotokionrule source=""item1"" place=""app1_aposticha"" count=""3"" startfrom=""2""/>";

            var element = TestRuleSerializer.Deserialize<TheotokionRule>(xmlString);

            Assert.IsFalse(element.IsValid);
            Assert.AreEqual(2, element.GetBrokenConstraints().Count);
        }

        [Test]
        public void TheotokionRule_Valid()
        {
            string xmlString = @"<theotokionrule source=""item1"" place=""kekragaria_theotokion"" count=""3"" startfrom=""2""/>";

            var element = TestRuleSerializer.Deserialize<TheotokionRule>(xmlString);

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void TheotokionRule_ChildRequired()
        {
            string xmlString = @"<theotokionrule source=""irmologion"" place=""app1_aposticha"" count=""3"" startfrom=""2""/>";

            var element = TestRuleSerializer.Deserialize<TheotokionRule>(xmlString);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void TheotokionRule_ValidWithChild()
        {
            string xmlString = @"<theotokionrule source=""irmologion"" place=""app1_aposticha"" count=""3"" startfrom=""2"">
                                    <ymnosrule source=""item1"" place=""kekragaria"" count=""3"" startfrom=""2""/>
                                 </theotokionrule>";

            var element = TestRuleSerializer.Deserialize<TheotokionRule>(xmlString);

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void TheotokionRule_FromRealDB()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();
            BookStorage.Instance = BookStorageFactory.Create();
            GetTypiconEntityResponse resp = new TypiconEntityService(_unitOfWork).GetTypiconEntity(1);
            TypiconEntity typiconEntity = resp.TypiconEntity;

            ServiceSequenceHandler handler = new ServiceSequenceHandler()
            {
                Settings = new RuleHandlerSettings() { Language = LanguageSettingsFactory.Create("cs-ru") }
            };

            string folderPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"TestData");
            FileReader reader = new FileReader(folderPath);
            string xml = reader.Read("TheotokionRuleTest.xml");

            //Дата --01-16 exists - false
            DateTime date = new DateTime(2017, 01, 16);

            MenologyRule rule = typiconEntity.GetMenologyRule(date);
            rule.RuleDefinition = xml;

            handler.Settings.Rule = rule;
            handler.Settings.DayWorships = rule.DayWorships;
            handler.Settings.Date = date;

            rule.GetRule(TestRuleSerializer.Root).Interpret(handler);

            var model = handler.GetResult();

            //KekragariaRuleViewModel model = rule.GetRule<KekragariaRule>(TestRuleSerializer.Root).CreateViewModel(handler) as KekragariaRuleViewModel;
            
            Assert.Pass(model.ToString());
            Assert.IsNotNull(model);
        }
    }
}
