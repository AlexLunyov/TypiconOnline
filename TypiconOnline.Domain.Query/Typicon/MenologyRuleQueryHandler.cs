using JetBrains.Annotations;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Typicon
{
    /// <summary>
    /// Возвращает dto-объект DayRuleDto по запросу Правила минеи.
    /// Используется в AppServices
    /// </summary>
    public class MenologyRuleQueryHandler : QueryStrategyHandlerBase
        , IQueryHandler<MenologyRuleQuery, MenologyRule>
    {
        public MenologyRuleQueryHandler(TypiconDBContext dbContext, [NotNull] IQueryProcessor queryProcessor) 
            : base(dbContext, queryProcessor)
        {
        }

        public MenologyRule Handle([NotNull] MenologyRuleQuery query)
        {
            var menologyRule = DbContext.Set<MenologyRule>().FirstOrDefault(GetExpression(query));

            return menologyRule;
        }


        /// <summary>
        /// Возвращает выражение для поиска MenologyRule
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Если поля Date или DateB пустые, вовращает пустое (минимальное) значение</returns>
        private Expression<Func<MenologyRule, bool>> GetExpression(MenologyRuleQuery query)
        {
            Expression<Func<MenologyRule, bool>> expression;

            if (DateTime.IsLeapYear(query.Date.Year))
            {
                expression = c => c.TypiconVersionId == query.TypiconVersionId 
                               && c.LeapDate.Day == query.Date.Day
                               && c.LeapDate.Month == query.Date.Month;
            }
            else
            {
                expression = c => c.TypiconVersionId == query.TypiconVersionId 
                               && c.Date.Day == query.Date.Day
                               && c.Date.Month == query.Date.Month;
            }

            return expression;
        }
    }
}
