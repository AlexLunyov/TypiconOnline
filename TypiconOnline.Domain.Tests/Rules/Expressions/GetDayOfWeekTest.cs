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
            string xmlString = @"<getdayofweek name=""воскресенье""></getdayofweek>";

            var element = TestRuleSerializer.Deserialize<GetDayOfWeek>(xmlString);

            element.Interpret(new DateTime(1900, 05, 23), BypassHandler.Instance);

            Assert.AreEqual((DayOfWeek)element.ValueCalculated, DayOfWeek.Sunday);
        }

        [Test]
        public void GetDayOfWeek_FromDate()
        {
            string xmlString = "<getdayofweek><date>--05-20</date></getdayofweek>";

            var element = TestRuleSerializer.Deserialize<GetDayOfWeek>(xmlString);

            element.Interpret(new DateTime(2017, 05, 23), BypassHandler.Instance);

            Assert.AreEqual((DayOfWeek)element.ValueCalculated, DayOfWeek.Saturday);
        }

        [Test]
        public void GetDayOfWeek_FromGetClosestDay()
        {
            string xmlString = @"<getdayofweek><getclosestday dayofweek=""суббота"" weekcount=""0""><date>--05-18</date></getclosestday></getdayofweek>";

            var element = TestRuleSerializer.Deserialize<GetDayOfWeek>(xmlString);

            element.Interpret(new DateTime(2017, 05, 23), BypassHandler.Instance);

            Assert.AreEqual((DayOfWeek)element.ValueCalculated, DayOfWeek.Saturday);
        }
    }
}
