using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Tests.Rules.Expressions
{
    [TestFixture]
    public class DaysFromEasterTest
    {
        [Test]
        public void DaysFromEaster_Creature()
        {
            string xmlString = @"<daysfromeaster><date>--04-07</date></daysfromeaster>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            DaysFromEaster element = new DaysFromEaster(xmlDoc.FirstChild);

            BypassHandler handler = new BypassHandler();

            element.Interpret(DateTime.Today, handler);

            Assert.AreEqual(-9, element.ValueCalculated);
            
        }
    }
}
