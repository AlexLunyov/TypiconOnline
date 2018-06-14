using JetBrains.Annotations;
using TypiconOnline.Domain.Query.Books;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Domain;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class TriodionRuleQueryHandler : QueryStrategyHandlerBase, IDataQueryHandler<TriodionRuleQuery, TriodionRule>
    {
        public TriodionRuleQueryHandler(IUnitOfWork unitOfWork, IDataQueryProcessor queryProcessor)
            : base(unitOfWork, queryProcessor) { }

        private readonly IncludeOptions Includes = new IncludeOptions()
        {
            Includes = new string[]
            {
                "Template.Template.Template",
                "DayRuleWorships.DayWorship.WorshipName",
                "DayRuleWorships.DayWorship.WorshipShortName"
            }
        };

        public TriodionRule Handle([NotNull] TriodionRuleQuery query)
        {
            var currentEaster = QueryProcessor.Process(new CurrentEasterQuery(query.Date.Year));

            int daysFromEaster = query.Date.Date.Subtract(currentEaster.Date.Date).Days;

            return UnitOfWork.Repository<TriodionRule>()
                .Get(c => c.OwnerId == query.TypiconId && c.DaysFromEaster == daysFromEaster, Includes);
        }
    }
}
