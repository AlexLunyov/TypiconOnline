using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Events;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Domain.Tests.Typicon
{
    [TestFixture]
    class EventsTest
    {
        [Test]
        public void GetEventEntity_Test()
        {
            MenologyRule rule = new MenologyRule();

            rule.RuleDefinition = "new string";

            var evnt = rule.GetDomainEvents().First();

            Assert.IsTrue((evnt as RuleDefinitionChangedEvent).Entity is MenologyRule);
        }
    }
}
