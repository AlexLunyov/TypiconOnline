using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TypiconOnline.Domain.WebQuery.Books;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Domain.WebQuery.Typicon;
using TypiconOnline.Tests.Common;
using TypiconOnline.Web.Extensions;

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

            var result = dbContext.MenologyRuleModels.ToList();

            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void WebDbContext_TriodionRules_Test()
        {
            var dbContext = WebDbContextFactory.Create();

            var result = dbContext.TriodionRuleModels.ToList();

            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void WebDbContext_MenologyDays_Test()
        {
            var dbContext = WebDbContextFactory.Create();

            var result = dbContext.MenologyDayModels.ToList();

            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void WebDbContext_TriodionDays_Test()
        {
            var dbContext = WebDbContextFactory.Create();

            var result = dbContext.TriodionDayModels.ToList();

            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void WebDbContext_Like_Test()
        {
            var dbContext = WebDbContextFactory.Create();

            var searchValue = "%С%";

            //var filters = new Expression<Func<MenologyRuleGridModel, bool>>[]
            //{
            //    m => EF.Functions.Like(m.Name, searchValue),
            //    m => EF.Functions.Like(m.DaysFromEaster, searchValue),
            //};

            var query = new AllMenologyRulesWebQuery(1, "");

            var result = dbContext.MenologyRuleModels.WhereAny(query.Search(searchValue));

            Assert.IsTrue(result.Count() > 0);
        }
    }
}
