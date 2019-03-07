using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Implementations;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Implementation
{
    [TestFixture]
    public class TypiconFromEntityFacadeTest
    {
        [Test]
        public void TypiconFromEntityFacade_GetMenologyRule()
        {
            var facade = new TypiconFromEntityFacade(TypiconDbContextFactory.Create());

            var rule = facade.GetMenologyRule(1, new DateTime(2018, 1, 1));

            Assert.IsNotNull(rule.Template);
            Assert.IsNotNull(rule.TypiconVersion);
        }
    }
}
