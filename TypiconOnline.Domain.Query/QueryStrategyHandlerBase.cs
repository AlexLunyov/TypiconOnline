using JetBrains.Annotations;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query
{
    public class QueryStrategyHandlerBase : UnitOfWorkHandlerBase
    {
        protected IDataQueryProcessor QueryProcessor { get; }

        public QueryStrategyHandlerBase(IUnitOfWork unitOfWork, [NotNull] IDataQueryProcessor queryProcessor) : base(unitOfWork)
        {
            QueryProcessor = queryProcessor;
        }
    }
}
