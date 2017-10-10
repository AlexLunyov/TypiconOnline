using System;
using NUnit.Framework;
using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Tests.Rules.Expressions
{
    [TestFixture]
    public class IntTests
    {
        [Test]
        public void Rules_Expressions_Int_WrongNumber()
        {
            string xmlString = "<int>--13-06</int>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            Int intElement = new Int(xmlDoc.FirstChild);
            //intElement.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.IsFalse(intElement.IsValid);
        }

        [Test]
        public void Rules_Expressions_Int_RightNumber()
        {
            string xmlString = "<int>-15</int>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            Int intElement = new Int(xmlDoc.FirstChild);
            intElement.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.AreEqual(-15, intElement.ValueCalculated);
        }
    }
}
