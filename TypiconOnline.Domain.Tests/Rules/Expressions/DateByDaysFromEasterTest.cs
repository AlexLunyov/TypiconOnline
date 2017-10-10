using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TypiconOnline.Domain.Books;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Repository.EF;

namespace TypiconOnline.Domain.Tests.Rules.Expressions
{
    [TestFixture]
    public class DateByDaysFromEasterTest
    {
        [Test]
        public void DateByDaysFromEaster_Test()
        {
            BookStorage.Instance = BookStorageFactory.Create();

            string xmlString = "<datebydaysfromeaster><int>-1</int></datebydaysfromeaster>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            DateByDaysFromEaster date = new DateByDaysFromEaster(xmlDoc.FirstChild);

            date.Interpret(new DateTime(2017, 05, 23), BypassHandler.Instance);

            string result = ((DateTime)date.ValueCalculated).ToString("dd-MM-yyyy");

            Assert.AreEqual(result, "15-04-2017");
        }

        [Test]
        public void DateByDaysFromEaster_Circle()
        {
            BookStorage.Instance = BookStorageFactory.Create();

            string xmlString = "<datebydaysfromeaster><daysfromeaster><date>--04-07</date></daysfromeaster></datebydaysfromeaster>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            DateByDaysFromEaster date = new DateByDaysFromEaster(xmlDoc.FirstChild);

            date.Interpret(new DateTime(2017, 05, 23), BypassHandler.Instance);

            string result = ((DateTime)date.ValueCalculated).ToString("dd-MM-yyyy");

            Assert.AreEqual(result, "07-04-2017");
        }
    }
}
