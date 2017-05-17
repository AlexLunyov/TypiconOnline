using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules.Schedule;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class NoticeTest
    {
        [Test]
        public void Notice_WrongAttributes()
        {
            string xmlString = @"<notice time=""11.00""/> ";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            Notice element = new Notice(xmlDoc.FirstChild);

            Assert.IsFalse(element.IsValid);
        }
    }
}
