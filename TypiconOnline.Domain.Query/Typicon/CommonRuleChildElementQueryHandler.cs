using JetBrains.Annotations;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Typicon
{
    /// <summary>
    /// Возвращает коллекцию RuleElement запрашиваемого общего правила.
    /// </summary>
    public class CommonRuleChildElementQueryHandler<T> : QueryStrategyHandlerBase, IDataQueryHandler<CommonRuleChildElementQuery<T>, T> 
        where T: class, IRuleElement
    {
        public CommonRuleChildElementQueryHandler(IUnitOfWork unitOfWork, IDataQueryProcessor queryProcessor)
            : base(unitOfWork, queryProcessor) { }

        public T Handle([NotNull] CommonRuleChildElementQuery<T> query)
        {
            var commonRule = QueryProcessor.Process(new CommonRuleQuery(query.TypiconId, query.Name));

            return commonRule?.GetRule<T>(query.RuleSerializer);
        }
    }
}
