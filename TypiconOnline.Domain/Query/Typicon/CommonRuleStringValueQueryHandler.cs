using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Rules.Schedule;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Typicon
{
    /// <summary>
    /// Возвращает строку из системного Общего правила, где определен только один элемент ItemText
    /// </summary>
    public class CommonRuleStringValueQueryHandler : QueryStrategyHandlerBase, IDataQueryHandler<CommonRuleStringValueQuery, string>
    {
        public CommonRuleStringValueQueryHandler(IUnitOfWork unitOfWork, IDataQueryProcessor queryProcessor)
            : base(unitOfWork, queryProcessor) { }

        public string Handle([NotNull] CommonRuleStringValueQuery query)
        {
            var itemText = QueryProcessor.Process(new CommonRuleItemTextValueQuery(query.TypiconId, query.Name, query.RuleSerializer));

            return itemText.FirstOrDefault(query.Language)?.Text ?? string.Empty;
        }
    }
}
