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
using TypiconOnline.Domain.WebQuery.Context;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает все Знаки служб у черновика Сущности Устава
    /// </summary>
    public class AllSignsQueryHandler : DbContextQueryBase, IQueryHandler<AllSignsQuery, Result<IQueryable<SignGridModel>>>
    {
        private readonly WebDbContext _webDbContext;
        public AllSignsQueryHandler(TypiconDBContext dbContext, WebDbContext webDbContext) : base(dbContext)
        {
            _webDbContext = webDbContext ?? throw new ArgumentNullException(nameof(webDbContext));
        }

        public Result<IQueryable<SignGridModel>> Handle([NotNull] AllSignsQuery query)
        {
            var draft = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId && c.BDate == null && c.EDate == null)
                            .FirstOrDefault();

            if (draft == null)
            {
                return Result.Fail<IQueryable<SignGridModel>>($"Черновик для Устава с Id={query.TypiconId} не был найден.");
            }

            var result = _webDbContext.Signs
                .Where(c => c.TypiconVersionId == draft.Id);

            if (query.ExceptSignId != null)
            {
                result = result.Where(c => c.Id != query.ExceptSignId.Value);
            }

            return Result.Ok(result);
        }

        
    }
}
