using NUnit.Framework;
using System;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Rules.Executables;
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

            var settings = factory.CreateSettings(1, new DateTime(2017, 01, 16), xml);

            //Дата --01-16 exists - false
            var handler = new ServiceSequenceHandler() { Settings = settings };

            KekragariaRule rule = (settings.RuleContainer as ExecContainer).ChildElements[0] as KekragariaRule;

            rule.Interpret(handler);

            Assert.AreEqual(3, rule.Structure.YmnosStructureCount);
            Assert.AreEqual(1, rule.Structure.Theotokion[0].Ymnis.Count);
        }
    }
}
