using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Tests.Common;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Expressions
{
    [TestFixture]
    public class LessDateTest
    {
        [Test]
        public void LessDate()
        {
            string xml = TestDataXmlReader.GetXmlString("LessDateTest.xml");

            var settings = new RuleHandlerSettings()
            {
                Date = new DateTime(2017, 02, 08)
            };

            var handler = new IsAdditionTestHandler() { Settings = settings };

            var ruleContainer = TestRuleSerializer.Deserialize<RootContainer>(xml);

            ruleContainer.Interpret(handler);

            var result = handler.GetResult();

            Assert.AreEqual(1, result.Count());
        }
    }
}
