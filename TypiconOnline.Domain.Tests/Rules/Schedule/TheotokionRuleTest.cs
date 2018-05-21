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

using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class TheotokionRuleTest
    {
        [Test]
        public void TheotokionRule_FromRealDB()
        {
            var factory = new RuleHandlerSettingsTestFactory();

            string xml = TestDataXmlReader.GetXmlString("TheotokionRuleTest.xml");

            var settings = factory.CreateSettings(new DateTime(2017, 01, 16), xml);

            //Дата --01-16 exists - false
            var handler = new ServiceSequenceHandler() { Settings = settings };

            KekragariaRule rule = settings.RuleContainer.ChildElements[0] as KekragariaRule;

            rule.Interpret(handler);

            Assert.AreEqual(3, rule.Structure.YmnosStructureCount);
            Assert.AreEqual(1, rule.Structure.Theotokion[0].Ymnis.Count);
        }
    }
}
