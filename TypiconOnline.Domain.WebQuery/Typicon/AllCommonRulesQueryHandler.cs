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
    /// Возвращает все Общие правила у черновика Сущности Устава
    /// </summary>
    public class AllCommonRulesQueryHandler : DbContextQueryBase, IQueryHandler<AllCommonRulesQuery, Result<IQueryable<CommonRuleModel>>>
    {
        public AllCommonRulesQueryHandler(TypiconDBContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<CommonRuleModel>> Handle([NotNull] AllCommonRulesQuery query)
        {
            var draft = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId && c.IsDraft)
                            .FirstOrDefault();

            if (draft == null)
            {
                return Result.Fail<IQueryable<CommonRuleModel>>($"Черновик для Устава с Id={query.TypiconId} не был найден.");
            }

            var CommonRules = DbContext.Set<CommonRule>()
                .Where(c => c.TypiconVersionId == draft.Id);

            var result = CommonRules.Select(c => new CommonRuleModel()
                {
                    Id = c.Id,
                    Name = c.Name
                });

            //ужасная мера
            //result = result
            //    .ToList()
            //    .AsQueryable();


            return Result.Ok(result);
        }

        
    }
}
