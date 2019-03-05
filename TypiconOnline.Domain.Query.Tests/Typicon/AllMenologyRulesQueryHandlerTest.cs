using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypiconOnline.Domain.Query.Typicon;

namespace TypiconOnline.Domain.Query.Tests.Typicon
{
    [TestClass]
    public class AllMenologyRulesQueryHandlerTest : QueryTestBase
    {

        [TestMethod]
        public void AllMenologyRulesQueryHandler_Get()
        {
            var rules = Processor.Process(new AllMenologyRulesQuery(1)).ToList();

            Assert.IsNotNull(rules);
            Assert.IsTrue(rules.Any());
        }
    }
}
