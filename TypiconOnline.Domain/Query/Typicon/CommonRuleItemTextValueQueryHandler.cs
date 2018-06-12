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
    public class CommonRuleItemTextValueQueryHandler : QueryStrategyHandlerBase, IDataQueryHandler<CommonRuleItemTextValueQuery, ItemText>
    {
        public CommonRuleItemTextValueQueryHandler(IUnitOfWork unitOfWork, IDataQueryProcessor queryProcessor)
            : base(unitOfWork, queryProcessor) { }

        public ItemText Handle([NotNull] CommonRuleItemTextValueQuery query)
        {
            var collection = QueryProcessor.Process(new CommonRuleChildrenQuery(query.TypiconId, query.Name, query.RuleSerializer));

            TextHolder textHolder = collection.FirstOrDefault() as TextHolder;

            ItemText result = null;

            if (textHolder?.Paragraphs.Count > 0)
            {
                result = textHolder.Paragraphs[0];
            }

            return result ?? new ItemText();
        }
    }
}
