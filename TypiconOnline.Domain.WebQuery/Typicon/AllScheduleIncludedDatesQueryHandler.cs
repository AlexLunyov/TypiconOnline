using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// 
    /// </summary>
    public class AllScheduleIncludedDatesQueryHandler : DbContextQueryBase, IQueryHandler<AllScheduleIncludedDatesQuery, Result<IQueryable<DateGridItem>>>
    {
        public AllScheduleIncludedDatesQueryHandler(TypiconDBContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<DateGridItem>> Handle([NotNull] AllScheduleIncludedDatesQuery query)
        {
            var settings = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId)
                            .Where(TypiconVersion.IsDraft)
                            .Select(c => c.ScheduleSettings)
                            .FirstOrDefault();

            if (settings != null)
            {
                var dates = settings.IncludedDates
                    .Select(c => new DateGridItem()
                    {
                        Date = c
                    })
                    .AsQueryable();

                return Result.Ok(dates);
            }

            return Result.Fail<IQueryable<DateGridItem>>("Настройки не заданы");
        }
    }
}
