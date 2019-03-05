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
    public class DayRuleFromTriodionQueryHandler : QueryStrategyHandlerBase
        , IDataQueryHandler<DayRuleFromTriodionQuery, DayRuleDto>
    {
        public DayRuleFromTriodionQueryHandler(TypiconDBContext dbContext, [NotNull] IDataQueryProcessor queryProcessor)
            : base(dbContext, queryProcessor)
        {
        }

        //private readonly IncludeOptions Includes = new IncludeOptions()
        //{
        //    Includes = new string[]
        //    {
        //            "Date",
        //            "DateB",
        //            //"Template.Template.Template",
        //            "DayRuleWorships.DayWorship.WorshipName.Items",
        //            "DayRuleWorships.DayWorship.WorshipShortName.Items",
        //    }
        //};

        public DayRuleDto Handle([NotNull] DayRuleFromTriodionQuery query)
        {
            var currentEaster = QueryProcessor.Process(new CurrentEasterQuery(query.Date.Year));

            int daysFromEaster = query.Date.Date.Subtract(currentEaster.Date).Days;

            var rule = DbContext.Set<TriodionRule>()
                .FirstOrDefault(c => c.TypiconEntityId == query.TypiconId && c.DaysFromEaster == daysFromEaster);

            var dayWorships = rule.DayWorships.Adapt<IReadOnlyList<DayWorshipDto>>();

            var sign = QueryProcessor.Process(new SignQuery(rule.TemplateId));

            return new DayRuleDto()
            {
                Id = rule.Id,
                DayWorships = dayWorships,
                IsAddition = rule.IsAddition,
                RuleDefinition = rule.RuleDefinition,
                Template = sign
            };
        }
    }
}
