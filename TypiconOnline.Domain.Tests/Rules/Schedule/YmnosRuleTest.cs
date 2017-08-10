using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class YmnosRuleTest
    {
        [Test]
        public void YmnosRule_Creature()
        {
            string xmlString = @"<ymnos source=""item1"" place=""kekragaria"" count=""3"" startfrom=""2""/>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            YmnosRule element = RuleFactory.CreateYmnosRule(xmlDoc.FirstChild);

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void YmnosRule_InvalidSource()
        {
            string xmlString = @"<ymnos source=""item"" place=""kekragaria"" count=""3"" startfrom=""2""/>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            YmnosRule element = RuleFactory.CreateYmnosRule(xmlDoc.FirstChild);

            Assert.IsFalse(element.IsValid);
            Assert.AreEqual(element.GetBrokenConstraints().Count, 1);
        }

        [Test]
        public void YmnosRule_NoSource()
        {
            string xmlString = @"<ymnos place=""kekragaria"" count=""3"" startfrom=""2""/>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            YmnosRule element = RuleFactory.CreateYmnosRule(xmlDoc.FirstChild);

            Assert.IsFalse(element.IsValid);
            Assert.AreEqual(element.GetBrokenConstraints().Count, 1);
        }

        [Test]
        public void YmnosRule_InvalidPlace()
        {
            string xmlString = @"<ymnos source=""item1"" place=""kekragaria11"" count=""3"" startfrom=""2""/>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            YmnosRule element = RuleFactory.CreateYmnosRule(xmlDoc.FirstChild);

            Assert.IsFalse(element.IsValid);
            Assert.AreEqual(element.GetBrokenConstraints().Count, 1);
        }

        [Test]
        public void YmnosRule_InvalidCount()
        {
            string xmlString = @"<ymnos source=""item1"" place=""kekragaria"" count=""3ss"" startfrom=""2""/>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            YmnosRule element = RuleFactory.CreateYmnosRule(xmlDoc.FirstChild);

            Assert.IsFalse(element.IsValid);
            Assert.AreEqual(element.GetBrokenConstraints().Count, 1);
        }

        [Test]
        public void YmnosRule_InvalidStartFrom()
        {
            string xmlString = @"<ymnos source=""item1"" place=""kekragaria"" count=""3"" startfrom=""2s""/>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            YmnosRule element = RuleFactory.CreateYmnosRule(xmlDoc.FirstChild);

            Assert.IsFalse(element.IsValid);
            Assert.AreEqual(element.GetBrokenConstraints().Count, 1);
        }
    }
}
