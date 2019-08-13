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
    /// Возвращает ExplicitAddRule model
    /// </summary>
    public class ExplicitAddRuleEditQueryHandler : DbContextQueryBase, IQueryHandler<ExplicitAddRuleEditQuery, Result<ExplicitAddRuleEditModel>>
    {
        public ExplicitAddRuleEditQueryHandler(TypiconDBContext dbContext)
            : base(dbContext)
        {
        }

        public Result<ExplicitAddRuleEditModel> Handle([NotNull] ExplicitAddRuleEditQuery query)
        {
            var found = DbContext.Set<ExplicitAddRule>().FirstOrDefault(c => c.Id == query.Id);

            if (found != null)
            {
                return Result.Ok(new ExplicitAddRuleEditModel()
                {
                    Id = found.Id,
                    Date = found.Date,
                    RuleDefinition = found.RuleDefinition
                });
            }
            else
            {
                return Result.Fail<ExplicitAddRuleEditModel>($"Явное правило с Id={query.Id} не найдено.");
            }
        }
    }
}
