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
    public class SignEditQueryHandler : DbContextQueryBase, IQueryHandler<SignEditQuery, Result<SignCreateEditModel>>
    {
        public SignEditQueryHandler(TypiconDBContext dbContext)
            : base(dbContext)
        {
        }

        public Result<SignCreateEditModel> Handle([NotNull] SignEditQuery query)
        {
            var found = DbContext.Set<Sign>().FirstOrDefault(c => c.Id == query.Id);

            if (found != null)
            {
                return Result.Ok(new SignCreateEditModel()
                {
                    Id = found.Id,
                    Name = found.SignName,
                    IsAddition = found.IsAddition,
                    PrintTemplateId = found.PrintTemplateId,
                    Priority = found.Priority,
                    TemplateId = found.TemplateId.HasValue ? found.TemplateId.Value : 0,
                    ModRuleDefinition = found.ModRuleDefinition,
                    RuleDefinition = found.RuleDefinition
                });
            }
            else
            {
                return Result.Fail<SignCreateEditModel>($"Знак службы с Id={query.Id} не найден.");
            }
        }
    }
}
