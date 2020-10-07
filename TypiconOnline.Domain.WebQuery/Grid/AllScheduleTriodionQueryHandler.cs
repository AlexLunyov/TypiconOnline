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
using TypiconOnline.Domain.WebQuery.Context;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Grid
{
    /// <summary>
    /// 
    /// </summary>
    public class AllScheduleTriodionQueryHandler : WebDbContextQueryBase, IQueryHandler<AllScheduleTriodionQuery, Result<IQueryable<TriodionRuleGridModel>>>
    {
        public AllScheduleTriodionQueryHandler(WebDbContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<TriodionRuleGridModel>> Handle([NotNull] AllScheduleTriodionQuery query)
        {
            var settings = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId)
                            .Where(TypiconVersion.IsDraft)
                            .Select(c => c.ScheduleSettings)
                            .FirstOrDefault();

            if (settings != null)
            {
                var models = from c in DbContext.TriodionRuleModels
                        join schedule in DbContext.Set<ModRuleEntitySchedule<TriodionRule>>()
                            on c.Id equals schedule.RuleId
                        where schedule.ScheduleSettingsId == settings.Id
                        select c;

                return Result.Ok(models);
            }

            return Result.Fail<IQueryable<TriodionRuleGridModel>>("Настройки не заданы");
        }
    }
}
