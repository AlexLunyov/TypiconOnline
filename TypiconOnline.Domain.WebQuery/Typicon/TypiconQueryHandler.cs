using JetBrains.Annotations;
using Mapster;
using System;
using System.Linq;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает Id и Name Устава
    /// </summary>
    public class TypiconQueryHandler : QueryStrategyHandlerBase, IDataQueryHandler<TypiconQuery, Result<TypiconDTO>>
    {
        public TypiconQueryHandler(TypiconDBContext dbContext, [NotNull] IDataQueryProcessor queryProcessor)
            : base(dbContext, queryProcessor)
        {
        }

        public Result<TypiconDTO> Handle([NotNull] TypiconQuery query)
        {
            var publishedVersion = QueryProcessor.Process(new TypiconPublishedVersionQuery(query.Id));

            if (publishedVersion.Success)
            {
                TypeAdapterConfig<TypiconVersion, TypiconDTO>
                    .NewConfig()
                    .Map(dest => dest.Id, src => src.TypiconId)
                    .Map(dest => dest.VersionId, src => src.Id)
                    .Map(dest => dest.Name, src => src.Name.FirstOrDefault(query.Language));

                return Result.Ok(publishedVersion.Value.Adapt<TypiconDTO>());
            }
            else
            {
                return Result.Fail<TypiconDTO>(publishedVersion.Error);
            }
        }
    }
}
