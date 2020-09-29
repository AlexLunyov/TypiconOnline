using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.AppServices.Extensions;
using TypiconOnline.Domain.Extensions;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.AppServices.Tests.Extensions
{
    [TestFixture]
    public class CloneTypiconVersionTest
    {
        [Test]
        public void CloneTypiconVersion_CommonRules()
        {
            var dbContext = TypiconDbContextFactory.Create();

            var version = dbContext.Set<TypiconVersion>()
                    .Include(c => c.CommonRules)
                              .AsNoTracking()
                              .FirstOrDefault();

            int countBefore = 1;// dbContext.Set<TypiconVersion>().Count();

            var clone = version.Clone(deep: true);
            clone.TypiconId = 1;

            dbContext.Set<TypiconVersion>().Add(clone);
            dbContext.SaveChanges();

            int countAfter = dbContext.Set<TypiconVersion>().Count();

            Assert.AreEqual(countAfter, countBefore + 1);
        }

        //[Test]
        //public void CloneTypiconVersion_Signs()
        //{
        //    var dbContext = TypiconDbContextFactory.Create();

        //    var version = dbContext.Set<TypiconVersion>()
        //            .Include(c => c.Signs)
        //                      //.AsNoTracking()
        //                      .FirstOrDefault();

        //    int countBefore = dbContext.Set<Sign>().Count();

        //    var clone = version.Clone(deep: true);
        //    clone.TypiconId = 1;

        //    dbContext.Set<TypiconVersion>().Add(clone);
        //    dbContext.SaveChanges();

        //    int countAfter = dbContext.Set<Sign>().Count();

        //    Assert.AreEqual(countAfter, countBefore + clone.Signs.Count);
        //    Assert.Greater(clone.Signs.Count, 0);
        //}
    }
}
