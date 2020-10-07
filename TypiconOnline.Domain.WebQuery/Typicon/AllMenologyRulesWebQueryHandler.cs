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
    public class AllMenologyRulesWebQueryHandler : DbContextQueryBase, IQueryHandler<AllMenologyRulesWebQuery, Result<IQueryable<MenologyRuleGridModel>>>
    {
        private readonly WebDbContext _webDbContext;

        public AllMenologyRulesWebQueryHandler(TypiconDBContext dbContext, WebDbContext webDbContext) : base(dbContext)
        {
            _webDbContext = webDbContext ?? throw new ArgumentNullException(nameof(webDbContext));
        }

        public Result<IQueryable<MenologyRuleGridModel>> Handle([NotNull] AllMenologyRulesWebQuery query)
        {
            var draft = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId)
                            .Where(TypiconVersion.IsDraft)
                            .FirstOrDefault();

            if (draft == null)
            {
                return Result.Fail<IQueryable<MenologyRuleGridModel>>($"Черновик для Устава с Id={query.TypiconId} не был найден.");
            }

            var result = _webDbContext.MenologyRuleModels
                .Where(c => c.TypiconVersionId == draft.Id);

            return Result.Ok(result);
        }
    }
}
