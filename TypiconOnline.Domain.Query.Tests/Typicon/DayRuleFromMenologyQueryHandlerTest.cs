using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TypiconOnline.Domain.Query.Typicon;

namespace TypiconOnline.Domain.Query.Tests.Typicon
{
    /// <summary>
    /// Summary description for DayRuleFromMenologyQueryHandlerTest
    /// </summary>
    [TestClass]
    public class DayRuleFromMenologyQueryHandlerTest : QueryTestBase
    {

        [TestMethod]
        public void DayRuleFromMenologyQueryHandler_Get()
        {
            var rule = Processor.Process(new MenologyRuleQuery(1, new DateTime(2018, 1, 1)));

            Assert.IsNotNull(rule);
            Assert.IsTrue(rule.DayWorships.Count > 0);
            Assert.IsNotNull(rule.Template);
        }
    }
}
