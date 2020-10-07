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
    /// Возвращает все Знаки служб у черновика Сущности Устава
    /// </summary>
    public class AllScheduleSignsQueryHandler : DbContextQueryBase, IQueryHandler<AllScheduleSignsQuery, Result<IQueryable<SignGridModel>>>
    {
        public AllScheduleSignsQueryHandler(TypiconDBContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<SignGridModel>> Handle([NotNull] AllScheduleSignsQuery query)
        {
            var settings = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId)
                            .Where(TypiconVersion.IsDraft)
                            .Select(c => c.ScheduleSettings)
                            .FirstOrDefault();

            if (settings != null)
            {
                var s = from sign in DbContext.Set<Sign>()
                        join schedule in DbContext.Set<ModRuleEntitySchedule<Sign>>()
                            on sign.Id equals schedule.RuleId
                        where schedule.ScheduleSettingsId == settings.Id
                        select new SignGridModel()
                        {
                            Id = sign.Id,
                            IsAddition = sign.IsAddition,
                            Name = sign.SignName.Items.Single(d => d.Language == CommonConstants.DefaultLanguage).Text,
                            Number = (sign.PrintTemplate != null) ? sign.PrintTemplate.Number : default,
                            Priority = sign.Priority,
                            TemplateName = (sign.Template != null) ? sign.Template.SignName.Items.Single(d => d.Language == CommonConstants.DefaultLanguage).Text : string.Empty
                        };

                return Result.Ok(s);
            }

            return Result.Fail<IQueryable<SignGridModel>>("Настройки не заданы");

            //ужасная мера
            //result = result
            //    .ToList()
            //    .AsQueryable();
        }
    }
}
