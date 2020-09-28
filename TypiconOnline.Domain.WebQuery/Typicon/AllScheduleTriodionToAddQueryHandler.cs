using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TypiconOnline.Domain.Common;
using TypiconOnline.Domain.Identity;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает все Правила Триоди для добавления в график богослужений
    /// </summary>
    public class AllScheduleTriodionToAddQueryHandler : DbContextQueryBase, IQueryHandler<AllScheduleTriodionToAddQuery, Result<IQueryable<TriodionRuleGridModel>>>
    {
        public AllScheduleTriodionToAddQueryHandler(TypiconDBContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<TriodionRuleGridModel>> Handle([NotNull] AllScheduleTriodionToAddQuery query)
        {
            var settings = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId)
                            .Where(TypiconVersion.IsDraft)
                            .Select(c => c.ScheduleSettings)
                            .FirstOrDefault();

             var rules = from rule in DbContext.Set<TriodionRule>()
                        join typicon in DbContext.Set<TypiconVersion>()
                            on rule.TypiconVersionId equals typicon.Id
                        where typicon.TypiconId == query.TypiconId
                        where typicon.BDate == null && typicon.EDate == null
                        select rule;

            if (query.DaysFormEaster != null)
            {
                rules = rules.Where(c => c.DaysFromEaster == query.DaysFormEaster);
            }

            rules = rules
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

            if (settings != null)
            {
                //выбираем те знаки, которых нет в графике
                //rules = rules.Except(settings.MenologyRules.Select(c => c.Rule));
            }

            var result = rules.Select(c => new TriodionRuleGridModel()
            {
                Id = c.Id,
                Name = c.GetNameByLanguage(CommonConstants.DefaultLanguage),
                DaysFromEaster = c.DaysFromEaster,
                IsTransparent = c.IsTransparent,
                HasModRuleDefinition = !string.IsNullOrEmpty(c.ModRuleDefinition),
                HasRuleDefinition = !string.IsNullOrEmpty(c.RuleDefinition),
                TemplateName = c.Template.GetNameByLanguage(CommonConstants.DefaultLanguage)
            });

            return Result.Ok(result);
        }
    }
}
