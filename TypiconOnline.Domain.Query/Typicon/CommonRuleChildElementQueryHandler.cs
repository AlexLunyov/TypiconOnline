using JetBrains.Annotations;
using System.Collections.Generic;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Typicon
{
    /// <summary>
    /// Возвращает коллекцию RuleElement запрашиваемого общего правила.
    /// </summary>
    public class CommonRuleChildElementQueryHandler<T> : QueryStrategyHandlerBase, IQueryHandler<CommonRuleChildElementQuery<T>, T> 
        where T: class, IRuleElement
    {
        public CommonRuleChildElementQueryHandler(TypiconDBContext dbContext, IQueryProcessor queryProcessor)
            : base(dbContext, queryProcessor) { }

        public T Handle([NotNull] CommonRuleChildElementQuery<T> query)
        {
            var commonRule = QueryProcessor.Process(new CommonRuleQuery(query.TypiconId, query.Name));

            return commonRule?.GetRule<T>(query.RuleSerializer);
        }
    }
}
