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
    public class AllScheduleMenologyQueryHandler : WebDbContextQueryBase, IQueryHandler<AllScheduleMenologyQuery, Result<IQueryable<MenologyRuleGridModel>>>
    {
        public AllScheduleMenologyQueryHandler(WebDbContext dbContext) : base(dbContext)
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
                var models = from c in DbContext.MenologyRuleModels
                        join schedule in DbContext.Set<ModRuleEntitySchedule<MenologyRule>>()
                            on c.Id equals schedule.RuleId
                        where schedule.ScheduleSettingsId == settings.Id
                        select c;

                //s = s
                //.Include(c => c.DayRuleWorships)
                //    .ThenInclude(c => c.DayWorship)
                //        .ThenInclude(c => c.WorshipName)
                //            .ThenInclude(c => c.Items)
                //.Include(c => c.DayRuleWorships)
                //    .ThenInclude(c => c.DayWorship)
                //        .ThenInclude(c => c.WorshipShortName)
                //            .ThenInclude(c => c.Items)
                //.Include(c => c.Template)
                //    .ThenInclude(c => c.SignName)
                //        .ThenInclude(c => c.Items);

                //var models = s.Select(c => new MenologyRuleGridModel()
                //{
                //    Id = c.Id,
                //    IsAddition = c.IsAddition,
                //    Name = c.Name,
                //    Date = c.Date.ToString() : string.Empty,
                //    LeapDate = (!c.LeapDate.IsEmpty) ? c.LeapDate.ToString() : string.Empty,
                //    HasModRuleDefinition = !string.IsNullOrEmpty(c.ModRuleDefinition),
                //    HasRuleDefinition = !string.IsNullOrEmpty(c.RuleDefinition),
                //    TemplateName = c.Template.GetNameByLanguage(CommonConstants.DefaultLanguage)
                //});

                return Result.Ok(models);
            }

            return Result.Fail<IQueryable<MenologyRuleGridModel>>("Настройки не заданы");
        }

        //public Result<IQueryable<MenologyRuleGridModel>> Handle([NotNull] AllScheduleMenologyQuery query)
        //{
        //    var draft = DbContext.Set<TypiconVersion>()
        //                    .Where(c => c.TypiconId == query.TypiconId)
        //                    .Where(TypiconVersion.IsDraft)
        //                    .FirstOrDefault();

        //    if (draft == null || draft.ScheduleSettings == null)
        //    {
        //        return Result.Fail<IQueryable<MenologyRuleGridModel>>($"Черновик для Устава или настройки графика богослужений с Id={query.TypiconId} не был найден.");
        //    }

        //    var links = draft.ScheduleSettings
        //        .MenologyRules
        //        .Select(c => c.RuleId)
        //        .ToArray();

        //    var menologies = _webDbContext.MenologyRules
        //        .Where(c => c.TypiconVersionId == draft.Id);

        //    var rules = from c in menologies
        //                join link in links
        //                on c.Id equals link
        //                select c;

        //    return Result.Ok(rules);
        //}
    }
}
