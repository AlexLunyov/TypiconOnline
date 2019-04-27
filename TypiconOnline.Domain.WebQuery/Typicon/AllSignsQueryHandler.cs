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
    public class AllSignsQueryHandler : DbContextQueryBase, IDataQueryHandler<AllSignsQuery, Result<IQueryable<SignModel>>>
    {
        public AllSignsQueryHandler(TypiconDBContext dbContext) : base(dbContext)
        {
            
        }

        public Result<IQueryable<SignModel>> Handle([NotNull] AllSignsQuery query)
        {
            var draft = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId && c.IsDraft)
                            .FirstOrDefault();

            if (draft == null)
            {
                return Result.Fail<IQueryable<SignModel>>($"Черновик для Устава с Id={query.TypiconId} не был найден.");
            }

            var signs = DbContext.Set<Sign>()
                .Where(c => c.TypiconVersionId == draft.Id);

            if (query.ExceptSignId != null)
            {
                signs = signs.Where(c => c.Id != query.ExceptSignId.Value);
            }

            var result = signs.Select(c => new SignModel()
                {
                    Id = c.Id,
                    IsAddition = c.IsAddition,
                    Name = c.GetNameByLanguage(query.Language),
                    Number = c.Number,
                    Priority = c.Priority,
                    TemplateName = (c.Template != null) ? c.Template.GetNameByLanguage(query.Language) : string.Empty
                });

            //ужасная мера
            //result = result
            //    .ToList()
            //    .AsQueryable();


            return Result.Ok(result);
        }

        
    }
}
