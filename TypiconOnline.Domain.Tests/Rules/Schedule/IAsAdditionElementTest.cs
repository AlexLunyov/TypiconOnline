using NUnit.Framework;
using System;
using System.Linq;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Extensions;
using TypiconOnline.Domain.Rules.Output.Factories;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Tests.Common;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class IAsAdditionElementTest
    {
        /// <summary>
        /// Создаем ситуацию с наличием Addition в настройках handler.
        /// В Addition-правиле должен быть элемент, перезаписывающий свойства в Шаблоне-правиле.
        /// </summary>
        [Test]
        public void IAsAdditionElement_Rewrite()
        {
            var factory = new RuleHandlerSettingsTestFactory();

            string xml = TestDataXmlReader.GetXmlString("AsAdditionRewrite1.xml");

            var additionalSettings = factory.CreateSettings(1, new DateTime(2017, 11, 09), xml);

            xml = TestDataXmlReader.GetXmlString("AsAdditionRewrite2.xml");

            var mainSettings = factory.CreateSettings(1, new DateTime(2017, 11, 09), xml, additionalSettings);

            var handler = new IsAdditionTestHandler() { Settings = mainSettings };

            //mainSettings.RuleContainer.Interpret(handler);

            WorshipRule worshipRule = (mainSettings.RuleContainer as ExecContainer).ChildElements[0] as WorshipRule;

            var kanonasRule = worshipRule.Sequence.GetChildElements<KanonasRule>(mainSettings).FirstOrDefault();

            Assert.IsNotNull(kanonasRule);

            kanonasRule.AfterRules[0].Interpret(handler);

            var result = handler.GetResult();

            Assert.IsNotNull(result.FirstOrDefault( c => c is EktenisRule));
        }

        [Test]
        public void IAsAdditionElement_Append()
        {
            var factory = new RuleHandlerSettingsTestFactory();

            string xml = TestDataXmlReader.GetXmlString("AsAdditionAppend2.xml");

            var additionalSettings = factory.CreateSettings(1, new DateTime(2017, 11, 09), xml);

            xml = TestDataXmlReader.GetXmlString("AsAdditionAppend1.xml");

            var mainSettings = factory.CreateSettings(1, new DateTime(2017, 11, 09), xml, additionalSettings);

            var handler = new IsAdditionTestHandler() { Settings = mainSettings };

            mainSettings.RuleContainer.Interpret(handler);

            var result = handler.GetResult();

            Assert.AreEqual(3, result.Count(c => c is KOdiRule));
        }

        [Test]
        public void IAsAdditionElement_Remove()
        {
            var factory = new RuleHandlerSettingsTestFactory();

            string xml = TestDataXmlReader.GetXmlString("AsAdditionRemove2.xml");

            var additionalSettings = factory.CreateSettings(1, new DateTime(2017, 11, 09), xml);

            xml = TestDataXmlReader.GetXmlString("AsAdditionRemove1.xml");

            var mainSettings = factory.CreateSettings(1, new DateTime(2017, 11, 09), xml, additionalSettings);

            var handler = new IsAdditionTestHandler() { Settings = mainSettings };

            mainSettings.RuleContainer.Interpret(handler);

            var result = handler.GetResult();

            Assert.AreEqual(4, result.Count(c => c is KOdiRule));
            Assert.IsNull(result.FirstOrDefault(c => c is KOdiRule && (c as KOdiRule).Number == 1));
        }

        [Test]
        public void IAsAdditionElement_IsMatchContinue()
        {
            // rule/worship
            var worship = new WorshipRule("worship", new RootContainer("rule"));

            // rule/worship/kanonasrule/odi
            var kanonasRule = new KanonasRule("kanonasrule"
                , new KanonasRuleVMFactory(TestRuleSerializer.Create())
                , new WorshipRule("worship", new RootContainer("rule")));
            kanonasRule.ChildElements.Add(new KOdiRule("odi", kanonasRule));

            var result = kanonasRule.IsMatch(worship);

            Assert.AreEqual(AsAdditionMatchingResult.Continue, result);
        }

        [Test]
        public void IAsAdditionElement_IsMatchSuccess()
        {
            // rule/worship
            var worship = new WorshipRule("worship", new RootContainer("rule"));

            // rule/worship
            var toMatch = new WorshipRule("worship", new RootContainer("rule"));

            var result = worship.IsMatch(toMatch);

            Assert.AreEqual(AsAdditionMatchingResult.Success, result);
        }

        [Test]
        public void IAsAdditionElement_IsMatchFail()
        {
            // rule/worship
            var worship = new WorshipRule("worship", new RootContainer("rule"));

            // rule/worship?id=someId
            var toMatch = new WorshipRule("worship", new RootContainer("rule"))
            {
                Id = "someId"
            };

            var result = worship.IsMatch(toMatch);

            Assert.AreEqual(AsAdditionMatchingResult.Fail, result);
        }

        [Test]
        public void IAsAdditionElement_IsMatchFail2()
        {
            // rule/worship/kanonasrule/odi
            var element = new KanonasRule("kanonasrule"
                , new KanonasRuleVMFactory(TestRuleSerializer.Create())
                , new WorshipRule("worship", new RootContainer("rule")));

            var odi = new KOdiRule("odi", element);
            element.ChildElements.Add(odi);

            // rule/worship/kanonasrule/after
            var toMatch = new KanonasRule("kanonasrule"
                , new KanonasRuleVMFactory(TestRuleSerializer.Create())
                , new WorshipRule("worship", new RootContainer("rule")));
            var after = new KAfterRule("after", element);
            element.ChildElements.Add(after);

            var result = odi.IsMatch(after);

            Assert.AreEqual(AsAdditionMatchingResult.Fail, result);
        }
    }
}
