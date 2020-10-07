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
    /// Возвращает все Знаки служб для добавления в график богослужений
    /// </summary>
    public class AllScheduleMenologyToAddQueryHandler : WebDbContextQueryBase, IQueryHandler<AllScheduleMenologyToAddQuery, Result<IQueryable<MenologyRuleGridModel>>>
    {
        public AllScheduleMenologyToAddQueryHandler(WebDbContext dbContext) : base(dbContext)
        {
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

            var result = DbContext.MenologyRuleModels
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
        }
    }
}
