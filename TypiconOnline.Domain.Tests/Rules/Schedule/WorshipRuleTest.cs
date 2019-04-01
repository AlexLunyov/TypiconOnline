using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.Domain.Tests.Rules.Schedule
{
    [TestFixture]
    public class WorshipRuleTest
    {
        [Test]
        public void Deserialize_Empty()
        {
            string xmlString = $@"<worship id=""moleben"" asaddition=""remove""/>";

            var element = TestRuleSerializer.Deserialize<WorshipRule>(xmlString);

            Assert.IsTrue(element.IsValid);
        }
    }
}
