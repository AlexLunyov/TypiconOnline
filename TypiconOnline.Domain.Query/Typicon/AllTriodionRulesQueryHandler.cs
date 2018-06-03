using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class AllTriodionRulesQueryHandler : UnitOfWorkHandlerBase, IDataQueryHandler<AllTriodionRulesQuery, IEnumerable<TriodionRule>>
    {
        public AllTriodionRulesQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        private readonly IncludeOptions Includes = new IncludeOptions()
        {
            Includes = new string[]
            {
                    "Template.Template.Template",
                    "DayRuleWorships.DayWorship.WorshipName",
                    "DayRuleWorships.DayWorship.WorshipShortName",
            }
        };

        public IEnumerable<TriodionRule> Handle([NotNull] AllTriodionRulesQuery query)
        {
            return UnitOfWork.Repository<TriodionRule>().GetAll(c => c.OwnerId == query.TypiconId, Includes).ToList();
        }
    }
}
