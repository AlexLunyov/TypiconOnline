using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Tests.Rules.Expressions
{
    [TestFixture]
    public class GetDayOfWeekTest
    {
        [Test]
        public void GetDayOfWeek_Simple()
        {
            string xmlString = "<dayofweek>воскресенье</dayofweek>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            GetDayOfWeek date = new GetDayOfWeek(xmlDoc.FirstChild);

            date.Interpret(new DateTime(1900, 05, 23), BypassHandler.Instance);

            Assert.AreEqual((DayOfWeek)date.ValueCalculated, DayOfWeek.Sunday);
        }

        [Test]
        public void GetDayOfWeek_FromDate()
        {
            string xmlString = "<getdayofweek><date>--05-20</date></getdayofweek>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            GetDayOfWeek date = new GetDayOfWeek(xmlDoc.FirstChild);

            date.Interpret(new DateTime(2017, 05, 23), BypassHandler.Instance);

            Assert.AreEqual((DayOfWeek)date.ValueCalculated, DayOfWeek.Saturday);
        }

        [Test]
        public void GetDayOfWeek_FromGetClosestDay()
        {
            string xmlString = @"<getdayofweek><getclosestday dayofweek=""суббота"" weekcount=""0""><date>--05-18</date></getclosestday></getdayofweek>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            GetDayOfWeek date = new GetDayOfWeek(xmlDoc.FirstChild);

            date.Interpret(new DateTime(2017, 05, 23), BypassHandler.Instance);

            Assert.AreEqual((DayOfWeek)date.ValueCalculated, DayOfWeek.Saturday);
        }
    }
}
