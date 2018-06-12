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
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Rules.Days;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;

using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class KanonasRuleTest
    {

        [Test]
        public void KanonasRule_FromDB()
        {
            DateTime date = new DateTime(2017, 11, 11);
            string xml = TestDataXmlReader.GetXmlString("KanonasRuleTest.xml");

            ServiceSequenceHandler handler = new ServiceSequenceHandler()
            {
                Settings = RuleHandlerSettingsTestFactory.Create(1, date, xml)
            };

            handler.ClearResult();
            KanonasRule kanonasRule = TestRuleSerializer.Deserialize<KanonasRule>(xml);
            kanonasRule.Interpret(handler);

            Assert.AreEqual(4, kanonasRule.Odes[0].Kanones.Count()); 
        }
    }
}
