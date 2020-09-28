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
    public class AllScheduleTriodionQueryHandler : DbContextQueryBase, IQueryHandler<AllScheduleTriodionQuery, Result<IQueryable<TriodionRuleGridModel>>>
    {
        public AllScheduleTriodionQueryHandler(TypiconDBContext dbContext) : base(dbContext)
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
                var s = from c in DbContext.Set<TriodionRule>()
                        join schedule in DbContext.Set<ModRuleEntitySchedule<TriodionRule>>()
                            on c.Id equals schedule.RuleId
                        where schedule.ScheduleSettingsId == settings.Id
                        select c;

                s = s
                .Include(c => c.DayRuleWorships)
                    .ThenInclude(c => c.DayWorship)
                        .ThenInclude(c => c.WorshipName)
                            .ThenInclude(c => c.Items)
                .Include(c => c.DayRuleWorships)
                    .ThenInclude(c => c.DayWorship)
                        .ThenInclude(c => c.WorshipShortName)
                            .ThenInclude(c => c.Items)
                .Include(c => c.Template)
                    .ThenInclude(c => c.SignName)
                        .ThenInclude(c => c.Items);

                var models = s.Select(c => new TriodionRuleGridModel()
                {
                    Id = c.Id,
                    Name = c.GetNameByLanguage(CommonConstants.DefaultLanguage),
                    DaysFromEaster = c.DaysFromEaster,
                    IsTransparent = c.IsTransparent,
                    HasModRuleDefinition = !string.IsNullOrEmpty(c.ModRuleDefinition),
                    HasRuleDefinition = !string.IsNullOrEmpty(c.RuleDefinition),
                    TemplateName = c.Template.GetNameByLanguage(CommonConstants.DefaultLanguage)
                });

                return Result.Ok(models);
            }

            return Result.Fail<IQueryable<TriodionRuleGridModel>>("Настройки не заданы");
        }
    }
}
