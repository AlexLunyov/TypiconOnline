﻿using JetBrains.Annotations;
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
    public class AllMenologyRulesQueryHandler : QueryStrategyHandlerBase, IDataQueryHandler<AllMenologyRulesQuery, IEnumerable<MenologyRule>>
    {
        public AllMenologyRulesQueryHandler(TypiconDBContext dbContext, [NotNull] IDataQueryProcessor queryProcessor)
            : base(dbContext, queryProcessor) { }

        //private readonly IncludeOptions Includes = new IncludeOptions()
        //{
        //    Includes = new string[]
        //    {
        //        "Date",
        //        "DateB",
        //        "DayRuleWorships.DayWorship.WorshipName.Items",
        //        "DayRuleWorships.DayWorship.WorshipShortName.Items"
        //    }
        //};

        public IEnumerable<MenologyRule> Handle([NotNull] AllMenologyRulesQuery query)
        {
            return DbContext.Set<MenologyRule>().Where(c => c.TypiconEntityId == query.TypiconId);
        }
    }
}
