using System;
using NUnit.Framework;
using TypiconOnline.Domain.Rules.Expressions;
using TypiconOnline.Domain.Rules.Handlers;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Expressions
{
    [TestFixture]
    class DateTests
    {
        [Test]
        public void Rules_Expressions_Date_Creature()
        {
            string xmlString = "<date>--04-06</date>";

            var element = TestRuleSerializer.Deserialize<Date>(xmlString);

            element.Interpret(BypassHandler.GetInstance(new DateTime(1900, 05, 23)));

            string result = ((DateTime)element.ValueCalculated).ToString("dd-MM-yyyy");

            Assert.AreEqual("06-04-1900", result);
        }
        
        [Test]
        public void Rules_Expressions_Date_WrongInputDate()
        {
            string xmlString = "<date>--13-06</date>";

            var element = TestRuleSerializer.Deserialize<Date>(xmlString);

            Assert.IsFalse(element.IsValid);

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

            var element = TestRuleSerializer.Deserialize<Date>(xmlString);
            //date.Interpret(DateTime.Today, BypassHandler.Instance);

            Assert.IsFalse(element.IsValid);

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
