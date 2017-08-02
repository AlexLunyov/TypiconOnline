using System;
using NUnit.Framework;
using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules.Handlers;

namespace TypiconOnline.Domain.Tests.Rules.Expressions
{
    [TestFixture]
    public class OrTest
    {
        [Test]
        public void Rules_Expressions_Or_Valid()
        {
            string xmlString = @"<or>
		                            <equals><int>1</int><int>1</int></equals>
		                            <more><int>2</int><int>1</int></more>
	                            </or>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            And element = new And(xmlDoc.FirstChild);
            element.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void Rules_Expressions_Or_True()
        {
            string xmlString = @"<or>
		                            <equals><int>12</int><int>1</int></equals>
		                            <more><int>-22</int><int>1</int></more>
                                    <less><int>2</int><int>15</int></less>
	                            </or>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            Or element = new Or(xmlDoc.FirstChild);
            element.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.IsTrue((bool)element.ValueCalculated);
        }

        [Test]
        public void Rules_Expressions_Or_False()
        {
            string xmlString = @"<or>
		                            <equals><int>1</int><int>11</int></equals>
		                            <more><int>2</int><int>12</int></more>
                                    <less><int>22</int><int>15</int></less>
	                            </or>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            And element = new And(xmlDoc.FirstChild);
            element.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.IsFalse((bool)element.ValueCalculated);
        }
    }
}
