using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.AppServices.Messaging.Typicon;
using TypiconOnline.Domain.Books.Oktoikh;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.ViewModels;

using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class YmnosStructureRuleTest
    {
        [Test]
        public void YmnosStructureRule_FromRealDB()
        {
            DateTime date = new DateTime(2017, 11, 09);
            string xml = TestDataXmlReader.GetXmlString("YmnosStructureRuleTest.xml");

            ServiceSequenceHandler handler = new ServiceSequenceHandler()
            {
                Settings = RuleHandlerSettingsTestFactory.Create(1, date, xml)
            };

            var ruleContainer = TestRuleSerializer.Deserialize<SedalenRule>(xml);
            ruleContainer.Interpret(handler);

            Assert.AreEqual(3, ruleContainer.Structure.YmnosStructureCount);
            Assert.Pass(ruleContainer.Structure.YmnosStructureCount.ToString());
        }
    }
}
