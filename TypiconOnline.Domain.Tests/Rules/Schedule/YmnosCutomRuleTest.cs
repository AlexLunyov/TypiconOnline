using NUnit.Framework;
using System;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Tests.Common;
using TypiconOnline.Domain.Interfaces;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class YmnosCutomRuleTest
    {
        [TestCase("YmnosCutomRuleTest_Deserialize.xml")]
        [TestCase("YmnosCutomRuleTest_Deserialize1.xml")]
        [TestCase("YmnosCutomRuleTest_Deserialize2.xml")]
        public void YmnosCutomRule_Deserialize(string filePath)
        {
            string xml = TestDataXmlReader.GetXmlString(filePath);

            var element = TestRuleSerializer.Deserialize<YmnosCustomRule>(xml);

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void YmnosCutomRule_InStructureGroup()
        {
            string xml = TestDataXmlReader.GetXmlString("YmnosCutomRuleTest_InStructureGroup.xml");

            var settings = new RuleHandlerSettingsTestFactory().CreateSettings(1, new DateTime(2017, 11, 09), xml);

            var mockFactory = new Mock<IRuleHandler>();
            mockFactory.Setup(c => c.Execute((It.IsAny<ICustomInterpreted>())));
            mockFactory.Setup(c => c.IsAuthorized<ICustomInterpreted>()).Returns(true);
            mockFactory.SetupProperty(c => c.Settings, settings);

            KekragariaRule element = TestRuleSerializer.Deserialize<KekragariaRule>(xml);

            element.Interpret(mockFactory.Object);

            Assert.AreEqual(4, element.Structure.YmnosStructureCount);
        }

        [Test]
        public void YmnosCutomRule_InStructureDoxastichon()
        {
            string xml = TestDataXmlReader.GetXmlString("YmnosCutomRuleTest_InStructureDoxastichon.xml");

            var settings = new RuleHandlerSettingsTestFactory().CreateSettings(1, new DateTime(2017, 11, 09), xml);

            var mockFactory = new Mock<IRuleHandler>();
            mockFactory.Setup(c => c.Execute((It.IsAny<ICustomInterpreted>())));
            mockFactory.Setup(c => c.IsAuthorized<ICustomInterpreted>()).Returns(true);
            mockFactory.SetupProperty(c => c.Settings, settings);

            KekragariaRule element = TestRuleSerializer.Deserialize<KekragariaRule>(xml);

            element.Interpret(mockFactory.Object);

            Assert.AreEqual(1, element.Structure.Doxastichon.Ymnis.Count);
        }

        [Test]
        public void YmnosCutomRule_InStructureTheotokion()
        {
            string xml = TestDataXmlReader.GetXmlString("YmnosCutomRuleTest_InStructureTheotokion.xml");

            var settings = new RuleHandlerSettingsTestFactory().CreateSettings(1, new DateTime(2017, 11, 09), xml);

            var mockFactory = new Mock<IRuleHandler>();
            mockFactory.Setup(c => c.Execute((It.IsAny<ICustomInterpreted>())));
            mockFactory.Setup(c => c.IsAuthorized<ICustomInterpreted>()).Returns(true);
            mockFactory.SetupProperty(c => c.Settings, settings);

            KekragariaRule element = TestRuleSerializer.Deserialize<KekragariaRule>(xml);

            element.Interpret(mockFactory.Object);

            Assert.AreEqual(1, element.Structure.Theotokion[0].Ymnis.Count);
        }
    }
}
