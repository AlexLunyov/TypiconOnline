using System;
using NUnit.Framework;
using System.Xml;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Domain.Rules.Factories;
using TypiconOnline.Repository.EF;
using TypiconOnline.Domain.Easter;
using System.Linq;

namespace TypiconOnline.Domain.Tests.Rules.Expressions
{
    [TestFixture]
    public class ComparisonsTest
    {
        [Test]
        public void Rules_Expressions_More_Creature()
        {
            string xmlString = @"<more>
                                    <int>2</int>
                                    <int>1</int>
                                 </more>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            BooleanExpression element = RuleFactory.CreateBooleanExpression(xmlDoc.FirstChild);
            element.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void Rules_Expressions_More_True()
        {
            string xmlString = @"<more>
                                    <int>2</int>
                                    <int>1</int>
                                    <int>-11</int>
                                 </more>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            BooleanExpression element = RuleFactory.CreateBooleanExpression(xmlDoc.FirstChild);
            element.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.IsTrue((bool)element.ValueCalculated);
        }

        [Test]
        public void Rules_Expressions_More_False()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();
            EasterStorage.Instance.EasterDays = _unitOfWork.Repository<EasterItem>().GetAll().ToList();

            string xmlString = @"<more>
                                    <int>2</int>
                                    <int>1</int>
                                    <int>-11</int>
                                    <daysfromeaster><date>--04-15</date></daysfromeaster>
                                 </more>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            BooleanExpression element = RuleFactory.CreateBooleanExpression(xmlDoc.FirstChild);
            element.Interpret(new DateTime(2017, 4, 15), BypassHandler.Instance);

            Assert.IsFalse((bool)element.ValueCalculated);
        }

        [Test]
        public void Rules_Expressions_MoreEquals_True()
        {
            string xmlString = @"<moreequals>
                                    <int>2</int>
                                    <int>2</int>
                                    <int>2</int>
                                 </moreequals>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            BooleanExpression element = RuleFactory.CreateBooleanExpression(xmlDoc.FirstChild);
            element.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.IsTrue((bool)element.ValueCalculated);
        }

        [Test]
        public void Rules_Expressions_Less_True()
        {
            string xmlString = @"<less>
                                    <int>1</int>
                                    <int>2</int>
                                    <int>15</int>
                                 </less>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            BooleanExpression element = RuleFactory.CreateBooleanExpression(xmlDoc.FirstChild);
            element.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.IsTrue((bool)element.ValueCalculated);
        }

        [Test]
        public void Rules_Expressions_LessEquals_True()
        {
            string xmlString = @"<lessequals>
                                    <int>1</int>
                                    <int>1</int>
                                    <int>1</int>
                                 </lessequals>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            BooleanExpression element = RuleFactory.CreateBooleanExpression(xmlDoc.FirstChild);
            element.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.IsTrue((bool)element.ValueCalculated);
        }
    }
}
