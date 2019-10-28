using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    /// Возвращает все Знаки служб у черновика Сущности Устава
    /// </summary>
    public class AllMenologyRulesQueryHandler : DbContextQueryBase, IQueryHandler<AllMenologyDaysQuery, Result<IQueryable<MenologyRuleModel>>>
    {
        public AllMenologyRulesQueryHandler(TypiconDBContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<MenologyRuleModel>> Handle([NotNull] AllMenologyDaysQuery query)
        {
            var draft = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId 
                                    && c.BDate == null && c.EDate == null)
                            .FirstOrDefault();

            if (draft == null)
            {
                return Result.Fail<IQueryable<MenologyRuleModel>>($"Черновик для Устава с Id={query.TypiconId} не был найден.");
            }

            var entities = DbContext.Set<MenologyRule>()
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
                        .ThenInclude(c => c.Items)
                .Where(c => c.TypiconVersionId == draft.Id);
                //.ToList();

            var result = entities.Select(c => new MenologyRuleModel()
                {
                    Id = c.Id,
                    IsAddition = c.IsAddition,
                    Name = c.GetNameByLanguage(query.Language),
                    Date = (!c.Date.IsEmpty) ? c.Date.ToString() : string.Empty,
                    LeapDate = (!c.LeapDate.IsEmpty) ? c.LeapDate.ToString() : string.Empty,
                    HasModRuleDefinition = !string.IsNullOrEmpty(c.ModRuleDefinition),
                    HasRuleDefinition = !string.IsNullOrEmpty(c.RuleDefinition),
                    TemplateName = c.Template.GetNameByLanguage(query.Language)
                });

            //ужасная мера
            //result = result
            //    .ToList()
            //    .AsQueryable();

            return Result.Ok(result);
        }

        
    }
}
