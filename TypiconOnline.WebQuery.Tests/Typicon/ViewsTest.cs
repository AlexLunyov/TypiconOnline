using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.WebQuery.Books;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.WebQuery.Tests.Typicon
{
    public class ViewsTest
    {
        [Test]
        public void AllMenologyRulesQueryHandler_Test()
        {
            var queryProcessor = DataQueryProcessorFactory.Create();

            var result = queryProcessor.Process(new AllMenologyDaysQuery("cs-ru"));

            var list = result.Value.ToList();
            
            Assert.IsNotNull(list);
        }

        [Test]
        public void WebDbContext_MenologyRules_Test()
        {
            var dbContext = WebDbContextFactory.Create();

            var result = dbContext.MenologyRules.ToList();

            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void WebDbContext_TriodionRules_Test()
        {
            var dbContext = WebDbContextFactory.Create();

            var result = dbContext.TriodionRules.ToList();

            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void WebDbContext_MenologyDays_Test()
        {
            var dbContext = WebDbContextFactory.Create();

            var result = dbContext.MenologyDays.ToList();

            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void WebDbContext_TriodionDays_Test()
        {
            var dbContext = WebDbContextFactory.Create();

            var result = dbContext.TriodionDays.ToList();

            Assert.IsTrue(result.Count > 0);
        }
    }
}
