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
    public class AllTriodionRulesQueryHandler : QueryStrategyHandlerBase, IDataQueryHandler<AllTriodionRulesQuery, IEnumerable<TriodionRule>>
    {
        public AllTriodionRulesQueryHandler(TypiconDBContext dbContext, [NotNull] IDataQueryProcessor queryProcessor)
            : base(dbContext, queryProcessor) { }

        public IEnumerable<TriodionRule> Handle([NotNull] AllTriodionRulesQuery query)
        {
            return DbContext.Set<TriodionRule>().Where(c => c.TypiconVersionId == query.TypiconId);
        }
    }
}
