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
    public class TypiconQueryHandler : QueryStrategyHandlerBase, IQueryHandler<TypiconQuery, Result<TypiconEntityModel>>
    {
        public TypiconQueryHandler(TypiconDBContext dbContext, [NotNull] IQueryProcessor queryProcessor)
            : base(dbContext, queryProcessor)
        {
        }

        public Result<TypiconEntityModel> Handle([NotNull] TypiconQuery query)
        {
            var publishedVersion = QueryProcessor.Process(new TypiconPublishedVersionQuery(query.Id));

            if (publishedVersion.Success)
            {
                TypeAdapterConfig<TypiconVersion, TypiconEntityModel>
                    .NewConfig()
                    .Map(dest => dest.Id, src => src.TypiconId)
                    .Map(dest => dest.Name, src => src.Name.FirstOrDefault(query.Language));

                return Result.Ok(publishedVersion.Value.Adapt<TypiconEntityModel>());
            }
            else
            {
                return Result.Fail<TypiconEntityModel>(publishedVersion.Error);
            }
        }
    }
}
