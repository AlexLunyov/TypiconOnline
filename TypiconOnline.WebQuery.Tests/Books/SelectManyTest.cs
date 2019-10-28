using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Tests.Common;

namespace TypiconOnline.WebQuery.Tests.Books
{
    class SelectManyTest
    {
        [TestCase("cs-ru")]
        public void SelectMany_Test(string language)
        {
            var dbContext = TypiconDbContextFactory.Create();

            var entities = dbContext.Set<MenologyDay>()
                .Include(c => c.DayWorships)
                        .ThenInclude(c => c.WorshipName)
                            .ThenInclude(c => c.Items)
                .Include(c => c.DayWorships)
                        .ThenInclude(c => c.WorshipShortName)
                            .ThenInclude(c => c.Items);
            //.ToList();

            var result = entities.SelectMany(q => q.DayWorships.Select(c => new MenologyDayModel()
            {
                Id = c.Id,
                //Date = (!q.Date.IsEmpty) ? q.Date.ToString() : string.Empty,
                //LeapDate = (!q.LeapDate.IsEmpty) ? q.LeapDate.ToString() : string.Empty,
                Name = c.WorshipName.FirstOrDefault(language).ToString(),

                //ShortName = (c.WorshipShortName != null) ? c.WorshipShortName.FirstOrDefault(language).ToString() : string.Empty,
                IsCelebrating = c.IsCelebrating
            }))
            .ToList();

            Assert.IsNotNull(result);
        }
    }
}
