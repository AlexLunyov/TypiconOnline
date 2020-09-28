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
    public class AllScheduleMenologyQueryHandler : DbContextQueryBase, IQueryHandler<AllScheduleMenologyQuery, Result<IQueryable<MenologyRuleGridModel>>>
    {
        public AllScheduleMenologyQueryHandler(TypiconDBContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<MenologyRuleGridModel>> Handle([NotNull] AllScheduleMenologyQuery query)
        {
            var settings = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId)
                            .Where(TypiconVersion.IsDraft)
                            .Select(c => c.ScheduleSettings)
                            .FirstOrDefault();

            if (settings != null)
            {
                var s = from c in DbContext.Set<MenologyRule>()
                        join schedule in DbContext.Set<ModRuleEntitySchedule<MenologyRule>>()
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

                var models = s.Select(c => new MenologyRuleGridModel()
                {
                    Id = c.Id,
                    IsAddition = c.IsAddition,
                    Name = c.GetNameByLanguage(CommonConstants.DefaultLanguage),
                    Date = (!c.Date.IsEmpty) ? c.Date.ToString() : string.Empty,
                    LeapDate = (!c.LeapDate.IsEmpty) ? c.LeapDate.ToString() : string.Empty,
                    HasModRuleDefinition = !string.IsNullOrEmpty(c.ModRuleDefinition),
                    HasRuleDefinition = !string.IsNullOrEmpty(c.RuleDefinition),
                    TemplateName = c.Template.GetNameByLanguage(CommonConstants.DefaultLanguage)
                });

                return Result.Ok(models);
            }

            return Result.Fail<IQueryable<MenologyRuleGridModel>>("Настройки не заданы");
        }
    }
}
