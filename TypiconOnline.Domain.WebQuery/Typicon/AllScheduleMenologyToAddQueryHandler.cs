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
    /// Возвращает все Знаки служб для добавления в график богослужений
    /// </summary>
    public class AllScheduleMenologyToAddQueryHandler : DbContextQueryBase, IQueryHandler<AllScheduleMenologyToAddQuery, Result<IQueryable<MenologyRuleGridModel>>>
    {
        public AllScheduleMenologyToAddQueryHandler(TypiconDBContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<MenologyRuleGridModel>> Handle([NotNull] AllScheduleMenologyToAddQuery query)
        {
            var settings = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId)
                            .Where(TypiconVersion.IsDraft)
                            .Select(c => c.ScheduleSettings)
                            .FirstOrDefault();

            var date = (query.Date != null)
                                ? new ItemDate(query.Date.Value.Month, query.Date.Value.Day)
                                : new ItemDate();

             var rules = from rule in DbContext.Set<MenologyRule>()
                        join typicon in DbContext.Set<TypiconVersion>()
                            on rule.TypiconVersionId equals typicon.Id
                        where typicon.TypiconId == query.TypiconId
                        where typicon.BDate == null && typicon.EDate == null
                        where rule.LeapDate.Month == date.Month && rule.LeapDate.Day == date.Day
                        select rule;

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
            //else
            //{
            //    signs = from sign in DbContext.Set<Sign>()
            //            join typicon in DbContext.Set<TypiconVersion>()
            //                on sign.TypiconVersionId equals typicon.Id
            //            where typicon.TypiconId == query.TypiconId
            //            where typicon.BDate == null && typicon.EDate == null
            //            select sign;
            //}

            var result = rules.Select(c => new MenologyRuleGridModel()
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

            return Result.Ok(result);

            //return Result.Fail<IQueryable<SignGridModel>>("Настройки не заданы");

            //ужасная мера
            //result = result
            //    .ToList()
            //    .AsQueryable();
        }
    }
}
