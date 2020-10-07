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

namespace TypiconOnline.Domain.WebQuery.Grid
{
    /// <summary>
    /// Возвращает все Правила Триоди для добавления в график богослужений
    /// </summary>
    public class AllScheduleTriodionToAddQueryHandler : WebDbContextQueryBase, IQueryHandler<AllScheduleTriodionToAddQuery, Result<IQueryable<TriodionRuleGridModel>>>
    {
        public AllScheduleTriodionToAddQueryHandler(WebDbContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<TriodionRuleGridModel>> Handle([NotNull] AllScheduleTriodionToAddQuery query)
        {
            var draft = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId)
                            .Where(TypiconVersion.IsDraft)
                            .FirstOrDefault();

            if (draft == null)
            {
                return Result.Fail<IQueryable<TriodionRuleGridModel>>($"Черновик для Устава с Id={query.TypiconId} не был найден.");
            }

            var rules = from rule in DbContext.TriodionRuleModels
                        join typicon in DbContext.Set<TypiconVersion>()
                            on rule.TypiconVersionId equals typicon.Id
                        where typicon.TypiconId == query.TypiconId
                        where typicon.BDate == null && typicon.EDate == null
                        select rule;

            if (query.DaysFormEaster != null)
            {
                rules = rules.Where(c => c.DaysFromEaster == query.DaysFormEaster);
            }

            if (draft.ScheduleSettings is ScheduleSettings settings)
            {
                //выбираем те знаки, которых нет в графике
                foreach (var id in settings.TriodionRules.Select(c => c.RuleId))
                {
                    rules = rules.Where(c => c.Id != id);
                }
            }

            return Result.Ok(rules);
        }
    }
}
