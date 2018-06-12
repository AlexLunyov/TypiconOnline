using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules;
using TypiconOnline.Domain.Rules.Executables;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Typicon
{
    /// <summary>
    /// Возвращает коллекцию RuleElement запрашиваемого общего правила.
    /// </summary>
    public class CommonRuleChildrenQueryHandler : QueryStrategyHandlerBase, IDataQueryHandler<CommonRuleChildrenQuery, IEnumerable<RuleElement>>
    {
        public CommonRuleChildrenQueryHandler(IUnitOfWork unitOfWork, IDataQueryProcessor queryProcessor)
            : base(unitOfWork, queryProcessor) { }

        public IEnumerable<RuleElement> Handle([NotNull] CommonRuleChildrenQuery query)
        {
            var commonRule = QueryProcessor.Process(new CommonRuleQuery(query.TypiconId, query.Name));

            IEnumerable<RuleElement> result = null;

            if (commonRule?.IsValid == true)
            {
                var container = commonRule.GetRule<ExecContainer>(query.RuleSerializer);

                if (container != null)
                {
                    result = container.ChildElements;
                }
            }

            return result ?? new List<RuleElement>();
        }
    }
}
