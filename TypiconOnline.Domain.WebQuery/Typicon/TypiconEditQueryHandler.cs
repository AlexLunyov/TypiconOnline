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
    public class TypiconEditQueryHandler : QueryStrategyHandlerBase, IDataQueryHandler<TypiconEditQuery, Result<TypiconEntityEditModel>>
    {
        public TypiconEditQueryHandler(TypiconDBContext dbContext, [NotNull] IDataQueryProcessor queryProcessor)
            : base(dbContext, queryProcessor)
        {
        }

        public Result<TypiconEntityEditModel> Handle([NotNull] TypiconEditQuery query)
        {
            var typicon = DbContext.Set<TypiconEntity>().FirstOrDefault(c => c.Id == query.Id);

            if (typicon != null)
            {
                return Result.Ok(new TypiconEntityEditModel()
                {
                    Id = typicon.Id,
                    Name = typicon.Name,
                    DefaultLanguage = typicon.DefaultLanguage
                });
            }
            else
            {
                return Result.Fail<TypiconEntityEditModel>($"Устав с Id={query.Id} не найден.");
            }
        }
    }
}
