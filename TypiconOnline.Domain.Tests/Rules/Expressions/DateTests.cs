using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Tests.Rules.Expressions
{
    [TestFixture]
    class DateTests
    {
        [Test]
        public void Rules_Expressions_Date_Creature()
        {
            string xmlString = "<date>--04-06</date>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            Date date = new Date(xmlDoc.FirstChild);

            date.Interpret(new DateTime(1900, 05, 23), BypassHandler.Instance);

            string result = ((DateTime)date.ValueCalculated).ToString("dd-MM-yyyy");

            Assert.AreEqual("06-04-1900", result);
        }
        
        [Test]
        public void Rules_Expressions_Date_WrongInputDate()
        {
            string xmlString = "<date>--13-06</date>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            Date date = new Date(xmlDoc.FirstChild);
            //date.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.IsFalse(date.IsValid);

            //bool isValid = true;

            //try
            //{
            //    Date date = new Date(xmlDoc.FirstChild);
            //    date.Interpret(DateTime.Today);
            //}
            //catch (DefinitionsParsingException ex)
            //{
            //    isValid = false;
            //}

            //Assert.IsFalse(isValid);
        }

        [Test]
        public void Rules_Expressions_Date_WrongInputDateFormat()
        {
            string xmlString = "<date>-13-06</date>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            Date date = new Date(xmlDoc.FirstChild);
            //date.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.IsFalse(date.IsValid);

            //bool isValid = true;

            //try
            //{
            //    Date date = new Date(xmlDoc.FirstChild);
            //    date.Interpret(DateTime.Today);
            //}
            //catch (DefinitionsParsingException ex)
            //{
            //    isValid = false;
            //}

            //Assert.IsFalse(isValid);
        }

    }
}
