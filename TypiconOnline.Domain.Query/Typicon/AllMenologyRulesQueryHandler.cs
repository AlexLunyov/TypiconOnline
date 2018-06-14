using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class AllMenologyRulesQueryHandler : UnitOfWorkHandlerBase, IDataQueryHandler<AllMenologyRulesQuery, IEnumerable<MenologyRule>>
    {
        public AllMenologyRulesQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        private readonly IncludeOptions Includes = new IncludeOptions()
        {
            Includes = new string[]
            {
                "Date",
                "DateB",
                "Template.Template.Template",
                "DayRuleWorships.DayWorship.WorshipName",
                "DayRuleWorships.DayWorship.WorshipShortName"
            }
        };

        public IEnumerable<MenologyRule> Handle([NotNull] AllMenologyRulesQuery query)
        {
            return UnitOfWork.Repository<MenologyRule>().GetAll(c => c.OwnerId == query.TypiconId, Includes).ToList();
        }
    }
}
