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
    /// 
    /// </summary>
    public class AllTriodionRulesWebQueryHandler : DbContextQueryBase, IQueryHandler<AllTriodionRulesWebQuery, Result<IQueryable<TriodionRuleGridModel>>>
    {
        public AllTriodionRulesWebQueryHandler(TypiconDBContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<TriodionRuleGridModel>> Handle([NotNull] AllTriodionRulesWebQuery query)
        {
            var draft = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId 
                                    && c.BDate == null && c.EDate == null)
                            .FirstOrDefault();

            if (draft == null)
            {
                return Result.Fail<IQueryable<TriodionRuleGridModel>>($"Черновик для Устава с Id={query.TypiconId} не был найден.");
            }

            var entities = DbContext.Set<TriodionRule>()
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

            var result = entities.Select(c => new TriodionRuleGridModel()
            {
                Id = c.Id,
                Name = c.GetNameByLanguage(query.Language),
                DaysFromEaster = c.DaysFromEaster,
                IsTransparent = c.IsTransparent,
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
