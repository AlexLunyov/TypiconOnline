using JetBrains.Annotations;
using Mapster;
using System;
using System.Linq;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает Id и Name Устава
    /// </summary>
    public class CommonRuleEditQueryHandler : DbContextQueryBase, IQueryHandler<CommonRuleEditQuery, Result<CommonRuleEditModel>>
    {
        public CommonRuleEditQueryHandler(TypiconDBContext dbContext)
            : base(dbContext)
        {
        }

        public Result<CommonRuleEditModel> Handle([NotNull] CommonRuleEditQuery query)
        {
            var found = DbContext.Set<CommonRule>().FirstOrDefault(c => c.Id == query.Id);

            if (found != null)
            {
                return Result.Ok(new CommonRuleEditModel()
                {
                    Id = found.Id,
                    Name = found.Name,
                    RuleDefinition = found.RuleDefinition
                });
            }
            else
            {
                return Result.Fail<CommonRuleEditModel>($"Знак службы с Id={query.Id} не найден.");
            }
        }
    }
}
