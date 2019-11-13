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
    /// Возвращает все Явные правила у черновика Сущности Устава
    /// </summary>
    public class AllExplicitAddRulesQueryHandler : DbContextQueryBase, IQueryHandler<AllExplicitAddRulesQuery, Result<IQueryable<ExplicitAddRuleGridModel>>>
    {
        public AllExplicitAddRulesQueryHandler(TypiconDBContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<ExplicitAddRuleGridModel>> Handle([NotNull] AllExplicitAddRulesQuery query)
        {
            var draft = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId && c.BDate == null && c.EDate == null)
                            .FirstOrDefault();

            if (draft == null)
            {
                return Result.Fail<IQueryable<ExplicitAddRuleGridModel>>($"Черновик для Устава с Id={query.TypiconId} не был найден.");
            }

            var found = DbContext.Set<ExplicitAddRule>()
                .Where(c => c.TypiconVersionId == draft.Id);

            var result = found.Select(c => new ExplicitAddRuleGridModel()
            {
                Id = c.Id,
                Date = c.Date.ToString("dd-MM-yyyy")
            });

            //ужасная мера
            //result = result
            //    .ToList()
            //    .AsQueryable();


            return Result.Ok(result);
        }

        
    }
}
