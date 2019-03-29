using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Xml;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Tests.Common;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class YmnosRuleTest
    {
        [Test]
        public void YmnosRule_Creature()
        {
            string xmlString = @"<ymnosrule source=""menology1"" place=""kekragaria"" count=""3"" startfrom=""2""/>";

            var element = TestRuleSerializer.Deserialize<YmnosRule>(xmlString);

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void YmnosRule_InvalidSource_WeekDay()
        {
            string xmlString = @"<ymnosrule source=""weekday"" place=""kekragaria"" count=""3"" startfrom=""2""/>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            var element = TestRuleSerializer.Deserialize<YmnosRule>(xmlString);

            Assert.IsFalse(element.IsValid);
            Assert.AreEqual(element.GetBrokenConstraints().Count, 1);
        }

        [Test]
        public void YmnosRule_AllPlaces()
        {
            string xmlRule = TestDataXmlReader.GetXmlString("YmnosRuleTest_AllPlaces.xml");
            string xmlText = TestDataXmlReader.GetXmlString("YmnosRuleTest_AllPlaces_Worship.xml");

            var handler = new IsAdditionTestHandler();

            foreach (PlaceYmnosSource place in Enum.GetValues(typeof(PlaceYmnosSource)))
            {
                var xmlModRule = xmlRule.Replace("[place]", place.ToString());
                handler.Settings = CreateFakeSettings(xmlModRule, xmlText);

                var rule = (handler.Settings.RuleContainer as ExecContainer).ChildElements[0] as KekragariaRule;

                rule.Interpret(handler);

                Assert.AreEqual(1, rule.Structure.YmnosStructureCount, $"Groups. place={place.ToString()}");
                Assert.AreEqual(1, rule.Structure.Doxastichon.Ymnis.Count, $"Doxastichon. place={place.ToString()}");
                Assert.AreEqual(1, rule.Structure.Theotokion[0].Ymnis.Count, $"Theotokion. place={place.ToString()}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rule">Правило</param>
        /// <param name="text">Текст службы</param>
        /// <returns></returns>
        private RuleHandlerSettings CreateFakeSettings(string rule, string text)
        {
            var menologyRule = new MenologyRule
            {
                RuleDefinition = rule
            };

            var dayWorships = new List<DayWorship>() { new DayWorship() { Definition = text } };

            var ruleContainer = TestRuleSerializer.Deserialize<RootContainer>(rule);// menologyRule.GetRule<ExecContainer>(TestRuleSerializer.Root);

            return new RuleHandlerSettings
            {
                Date = DateTime.Today,
                //TypiconRule = menologyRule,
                Menologies = dayWorships,
                RuleContainer = ruleContainer
            };
        }
    }
}
