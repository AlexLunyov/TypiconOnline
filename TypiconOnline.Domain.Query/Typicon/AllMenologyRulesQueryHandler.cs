using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using Mapster;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class AllMenologyRulesQueryHandler : QueryStrategyHandlerBase, IDataQueryHandler<AllMenologyRulesQuery, IEnumerable<MenologyRule>>
    {
        public AllMenologyRulesQueryHandler(TypiconDBContext dbContext, [NotNull] IDataQueryProcessor queryProcessor)
            : base(dbContext, queryProcessor) { }

        public IEnumerable<MenologyRule> Handle([NotNull] AllMenologyRulesQuery query)
        {
            return DbContext.Set<MenologyRule>().Where(c => c.TypiconVersionId == query.TypiconId).ToList();
        }
    }
}
