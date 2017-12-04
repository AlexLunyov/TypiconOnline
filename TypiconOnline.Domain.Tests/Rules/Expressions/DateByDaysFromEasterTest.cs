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
            string xmlString = "<datebydaysfromeaster><int>-1</int></datebydaysfromeaster>";

            var element = TestRuleSerializer.Deserialize<DateByDaysFromEaster>(xmlString);

            element.Interpret(BypassHandler.GetInstance(new DateTime(2017, 05, 23)));

            string result = ((DateTime)element.ValueCalculated).ToString("dd-MM-yyyy");

            Assert.AreEqual(result, "15-04-2017");
        }

        [Test]
        public void DateByDaysFromEaster_Circle()
        {
            string xmlString = "<datebydaysfromeaster><daysfromeaster><date>--04-07</date></daysfromeaster></datebydaysfromeaster>";

            var element = TestRuleSerializer.Deserialize<DateByDaysFromEaster>(xmlString);

            element.Interpret(BypassHandler.GetInstance(new DateTime(2017, 05, 23)));

            string result = ((DateTime)element.ValueCalculated).ToString("dd-MM-yyyy");

            Assert.AreEqual(result, "07-04-2017");
        }
    }
}
