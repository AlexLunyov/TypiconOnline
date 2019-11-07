using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Interfaces;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Variables
{
    [TestFixture]
    public class VariablesTest
    {
        [Test]
        public void Variables_Test()
        {
            DateTime date = new DateTime(2017, 11, 09);
            string xml = TestDataXmlReader.GetXmlString("VariablesTest.xml");

            var handler = new CollectorRuleHandler<IHavingVariables>()
            {
                Settings = RuleHandlerSettingsTestFactory.Create(1, date, xml)
            };

            var ruleContainer = TestRuleSerializer.Deserialize<RootContainer>(xml);
            ruleContainer.Interpret(handler);

            var result = handler.GetResult();

            var variables = new List<string>();

            foreach (var r in result)
            {
                variables.AddRange(r.GetVariableNames().Select(c => c.Item1));
            }

            variables = variables.Distinct().ToList();

            Assert.AreEqual(2, variables.Count);
        }
    }
}
