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
    public class CommonRuleIndexedStringValueQueryHandler : QueryStrategyHandlerBase, IDataQueryHandler<CommonRuleIndexedStringValueQuery, string>
    {
        public CommonRuleIndexedStringValueQueryHandler(IUnitOfWork unitOfWork, IDataQueryProcessor queryProcessor)
            : base(unitOfWork, queryProcessor) { }

        public string Handle([NotNull] CommonRuleIndexedStringValueQuery query)
        {
            var collection = QueryProcessor.Process(new CommonRuleChildrenQuery(query.TypiconId, query.Name, query.RuleSerializer));

            string result = "";

            if (collection.ElementAtOrDefault(query.Index) is TextHolder t && t.Paragraphs?.Count > 0)
            {
                result = t.Paragraphs[0]?.FirstOrDefault(query.Language).Text;
            }

            return result;
        }
    }
}
