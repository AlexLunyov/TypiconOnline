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
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает все Знаки служб у черновика Сущности Устава
    /// </summary>
    public class AllVariablesQueryHandler : DbContextQueryBase, IQueryHandler<AllVariablesQuery, Result<IQueryable<TypiconVariableModel>>>
    {
        public AllVariablesQueryHandler(TypiconDBContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<TypiconVariableModel>> Handle([NotNull] AllVariablesQuery query)
        {
            var draft = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId && c.BDate == null && c.EDate == null)
                            .FirstOrDefault();

            if (draft == null)
            {
                return Result.Fail<IQueryable<TypiconVariableModel>>($"Черновик для Устава с Id={query.TypiconId} не был найден.");
            }

            var signs = DbContext.Set<TypiconVariable>()
                .Where(c => c.TypiconVersionId == draft.Id);

            var result = signs.Select(c => new TypiconVariableModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Type = c.Type,
                    //TODO: Count
                });

            //ужасная мера
            //result = result
            //    .ToList()
            //    .AsQueryable();


            return Result.Ok(result);
        }

        
    }
}
