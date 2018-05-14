using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class YmnosRuleTest
    {
        [Test]
        public void YmnosRule_Creature()
        {
            string xmlString = @"<ymnosrule source=""item1"" place=""kekragaria"" count=""3"" startfrom=""2""/>";

            var element = TestRuleSerializer.Deserialize<YmnosRule>(xmlString);

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void YmnosRule_InvalidSource()
        {
            string xmlString = @"<ymnosrule source=""item"" place=""kekragaria"" count=""3"" startfrom=""2""/>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            var element = TestRuleSerializer.Deserialize<YmnosRule>(xmlString);

            Assert.IsFalse(element.IsValid);
            Assert.AreEqual(element.GetBrokenConstraints().Count, 1);
        }

        [Test]
        public void YmnosRule_NoSource()
        {
            string xmlString = @"<ymnosrule place=""kekragaria"" count=""3"" startfrom=""2""/>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            var element = TestRuleSerializer.Deserialize<YmnosRule>(xmlString);

            Assert.IsFalse(element.IsValid);
            Assert.AreEqual(element.GetBrokenConstraints().Count, 1);
        }

        [Test]
        public void YmnosRule_InvalidPlace()
        {
            string xmlString = @"<ymnosrule source=""item1"" place=""kekragaria11"" count=""3"" startfrom=""2""/>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            var element = TestRuleSerializer.Deserialize<YmnosRule>(xmlString);

            Assert.IsFalse(element.IsValid);
            Assert.AreEqual(element.GetBrokenConstraints().Count, 1);
        }

        //[Test]
        //public void YmnosRule_InvalidCount()
        //{
        //    string xmlString = @"<ymnosrule source=""item1"" place=""kekragaria"" count=""3ss"" startfrom=""2""/>";

        //    XmlDocument xmlDoc = new XmlDocument();

        //    xmlDoc.LoadXml(xmlString);

        //    var element = TestRuleSerializer.Deserialize<YmnosRule>(xmlString);

        //    Assert.IsFalse(element.IsValid);
        //    Assert.AreEqual(element.GetBrokenConstraints().Count, 1);
        //}

        //[Test]
        //public void YmnosRule_InvalidStartFrom()
        //{
        //    string xmlString = @"<ymnosrule source=""item1"" place=""kekragaria"" count=""3"" startfrom=""2s""/>";

        //    XmlDocument xmlDoc = new XmlDocument();

        //    xmlDoc.LoadXml(xmlString);

        //    var element = TestRuleSerializer.Deserialize<YmnosRule>(xmlString);

        //    Assert.IsFalse(element.IsValid);
        //    Assert.AreEqual(element.GetBrokenConstraints().Count, 1);
        //}
    }
}
