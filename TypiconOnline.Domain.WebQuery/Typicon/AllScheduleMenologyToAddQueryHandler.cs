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
using TypiconOnline.Domain.WebQuery.Context;
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
        private readonly WebDbContext _webDbContext;

        public AllScheduleMenologyToAddQueryHandler(TypiconDBContext dbContext, WebDbContext webDbContext) : base(dbContext)
        {
            _webDbContext = webDbContext ?? throw new ArgumentNullException(nameof(webDbContext));
        }

        public Result<IQueryable<MenologyRuleGridModel>> Handle([NotNull] AllScheduleMenologyToAddQuery query)
        {
            var draft = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId)
                            .Where(TypiconVersion.IsDraft)
                            .FirstOrDefault();

            if (draft == null)
            {
                return Result.Fail<IQueryable<MenologyRuleGridModel>>($"Черновик для Устава с Id={query.TypiconId} не был найден.");
            }

            var date = (query.Date != null)
                                ? query.Date.Value.ToString("MM-dd")
                                : string.Empty;

            var result = _webDbContext.MenologyRules
                .Where(c => c.TypiconVersionId == draft.Id)
                .Where(c => c.LeapDate == date);

            if (draft.ScheduleSettings is ScheduleSettings settings)
            {
                //выбираем те знаки, которых нет в графике
                foreach (var id in settings.MenologyRules.Select(c => c.RuleId))
                {
                    result = result.Where(c => c.Id != id);
                }
            }

            return Result.Ok(result);

            //var rules = from rule in DbContext.Set<MenologyRule>()
            //            join typicon in DbContext.Set<TypiconVersion>()
            //                on rule.TypiconVersionId equals typicon.Id
            //            where typicon.TypiconId == query.TypiconId
            //            where typicon.BDate == null && typicon.EDate == null
            //            where rule.LeapDate.Month == date.Month && rule.LeapDate.Day == date.Day
            //            select rule;

            //rules = rules
            //    .Include(c => c.DayRuleWorships)
            //        .ThenInclude(c => c.DayWorship)
            //            .ThenInclude(c => c.WorshipName)
            //                .ThenInclude(c => c.Items)
            //    .Include(c => c.DayRuleWorships)
            //        .ThenInclude(c => c.DayWorship)
            //            .ThenInclude(c => c.WorshipShortName)
            //                .ThenInclude(c => c.Items)
            //    .Include(c => c.Template)
            //        .ThenInclude(c => c.SignName)
            //            .ThenInclude(c => c.Items);


            ////else
            ////{
            ////    signs = from sign in DbContext.Set<Sign>()
            ////            join typicon in DbContext.Set<TypiconVersion>()
            ////                on sign.TypiconVersionId equals typicon.Id
            ////            where typicon.TypiconId == query.TypiconId
            ////            where typicon.BDate == null && typicon.EDate == null
            ////            select sign;
            ////}

            //var result = rules.Select(c => new MenologyRuleGridModel()
            //{
            //    Id = c.Id,
            //    IsAddition = c.IsAddition,
            //    Name = c.GetNameByLanguage(CommonConstants.DefaultLanguage),
            //    Date = (!c.Date.IsEmpty) ? c.Date.ToString() : string.Empty,
            //    LeapDate = (!c.LeapDate.IsEmpty) ? c.LeapDate.ToString() : string.Empty,
            //    HasModRuleDefinition = !string.IsNullOrEmpty(c.ModRuleDefinition),
            //    HasRuleDefinition = !string.IsNullOrEmpty(c.RuleDefinition),
            //    TemplateName = c.Template.GetNameByLanguage(CommonConstants.DefaultLanguage)
            //});

            //return Result.Ok(result);

            //return Result.Fail<IQueryable<SignGridModel>>("Настройки не заданы");

            //ужасная мера
            //result = result
            //    .ToList()
            //    .AsQueryable();
        }
    }
}
