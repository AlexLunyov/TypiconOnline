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

namespace TypiconOnline.Domain.WebQuery.Grid
{
    /// <summary>
    /// Возвращает все Знаки служб для добавления в график богослужений
    /// </summary>
    public class AllScheduleSignsToLoadQueryHandler : DbContextQueryBase, IQueryHandler<AllScheduleSignsToLoadQuery, Result<IQueryable<SignGridModel>>>
    {
        public AllScheduleSignsToLoadQueryHandler(TypiconDBContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<SignGridModel>> Handle([NotNull] AllScheduleSignsToLoadQuery query)
        {
            var draft = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId)
                            .Where(TypiconVersion.IsDraft)
                            .FirstOrDefault();

            if (draft == null)
            {
                return Result.Fail<IQueryable<SignGridModel>>($"Черновик для Устава с Id={query.TypiconId} не был найден.");
            }

            var signs = from sign in DbContext.Set<Sign>()
                        join typicon in DbContext.Set<TypiconVersion>()
                            on sign.TypiconVersionId equals typicon.Id
                        where typicon.TypiconId == query.TypiconId
                        where typicon.BDate == null && typicon.EDate == null
                        select sign;

            var result = signs.Select(sign => new SignGridModel()
            {
                Id = sign.Id,
                IsAddition = sign.IsAddition,
                Name = sign.SignName.Items.Single(d => d.Language == CommonConstants.DefaultLanguage).Text,
                Number = (sign.PrintTemplate != null) ? sign.PrintTemplate.Number : default,
                Priority = sign.Priority,
                TemplateName = (sign.Template != null) ? sign.Template.SignName.Items.Single(d => d.Language == CommonConstants.DefaultLanguage).Text : string.Empty
            });

            if (draft.ScheduleSettings is ScheduleSettings settings)
            {
                //выбираем те знаки, которых нет в графике
                foreach (var id in settings.Signs.Select(c => c.RuleId))
                {
                    result = result.Where(c => c.Id != id);
                }
            }

            return Result.Ok(result);
        }
    }
}
