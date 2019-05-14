using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Mapster;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;
using System.Linq;

namespace TypiconOnline.Domain.Query.Typicon
{
    /// <summary>
    /// Возвращает dto-объект DayRuleDto по запросу Правила минеи.
    /// Используется в AppServices
    /// </summary>
    public class TriodionRuleQueryHandler : QueryStrategyHandlerBase
        , IQueryHandler<TriodionRuleQuery, TriodionRule>
    {
        public TriodionRuleQueryHandler(TypiconDBContext dbContext, [NotNull] IQueryProcessor queryProcessor)
            : base(dbContext, queryProcessor)
        {
        }

        public TriodionRule Handle([NotNull] TriodionRuleQuery query)
        {
            var currentEaster = QueryProcessor.Process(new CurrentEasterQuery(query.Date.Year));

            int daysFromEaster = query.Date.Date.Subtract(currentEaster.Date).Days;

            return DbContext.Set<TriodionRule>()
                .FirstOrDefault(c => c.TypiconVersionId == query.TypiconVersionId && c.DaysFromEaster == daysFromEaster);
        }
    }
}
