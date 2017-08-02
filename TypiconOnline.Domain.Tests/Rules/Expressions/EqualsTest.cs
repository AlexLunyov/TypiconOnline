﻿using System;
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
    public class EqualsTest
    {
        [Test]
        public void Rules_Expressions_Equals_Creature()
        {
            string xmlString = @"<equals>
                                    <int>1</int>
                                    <int>2</int>
                                 </equals>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            BooleanExpression element = RuleFactory.CreateBooleanExpression(xmlDoc.FirstChild);
            element.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.IsTrue(element.IsValid);
        }

        [Test]
        public void Rules_Expressions_Equals_NotEnoughChildren()
        {
            string xmlString = @"<equals>
                                    <int>1</int>
                                 </equals>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            BooleanExpression element = RuleFactory.CreateBooleanExpression(xmlDoc.FirstChild);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void Rules_Expressions_Equals_ChildrenRequired()
        {
            string xmlString = @"<equals>
                                    
                                 </equals>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            BooleanExpression element = RuleFactory.CreateBooleanExpression(xmlDoc.FirstChild);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void Rules_Expressions_Equals_InvalidChild()
        {
            string xmlString = @"<equals>
                                    <case>11</case>
                                 </equals>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            BooleanExpression element = RuleFactory.CreateBooleanExpression(xmlDoc.FirstChild);

            Assert.IsFalse(element.IsValid);
        }

        [Test]
        public void Rules_Expressions_Equals_TypeMismatch()
        {
            string xmlString = @"<equals>
                                    <int>1</int>
                                    <getclosestday dayofweek=""saturday"" weekcount=""-2""><date>--11-08</date></getclosestday>
                                   </equals>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            BooleanExpression element = RuleFactory.CreateBooleanExpression(xmlDoc.FirstChild);

            Assert.IsFalse(element.IsValid);
        }
        

       [Test]
        public void Rules_Expressions_And_Calculating_Correct()
        {
            EFUnitOfWork _unitOfWork = new EFUnitOfWork();

            EasterStorage.Instance.EasterDays = _unitOfWork.Repository<EasterItem>().GetAll().ToList();

            string xmlString = @"<equals>
                                    <int>-1</int>
                                    <daysfromeaster><date>--04-15</date></daysfromeaster>
                                 </equals>";

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(xmlString);

            BooleanExpression element = RuleFactory.CreateBooleanExpression(xmlDoc.FirstChild);
            element.Interpret(new DateTime(2017, 4, 15), BypassHandler.Instance);

            Assert.IsTrue((bool)element.ValueCalculated);
        }
    }
}
