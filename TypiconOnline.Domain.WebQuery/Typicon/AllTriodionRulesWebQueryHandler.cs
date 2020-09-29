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
    /// 
    /// </summary>
    public class AllTriodionRulesWebQueryHandler : DbContextQueryBase, IQueryHandler<AllTriodionRulesWebQuery, Result<IQueryable<TriodionRuleGridModel>>>
    {
        private readonly WebDbContext _webDbContext;

        public AllTriodionRulesWebQueryHandler(TypiconDBContext dbContext, WebDbContext webDbContext) : base(dbContext)
        {
            _webDbContext = webDbContext ?? throw new ArgumentNullException(nameof(webDbContext));
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

            var result = _webDbContext.TriodionRules
                .Where(c => c.TypiconVersionId == draft.Id);

            return Result.Ok(result);
        }
    }
}
