using JetBrains.Annotations;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query
{
    public abstract class QueryStrategyHandlerBase : DbContextHandlerBase
    {
        protected IDataQueryProcessor QueryProcessor { get; }

        protected QueryStrategyHandlerBase(TypiconDBContext dbContext, [NotNull] IDataQueryProcessor queryProcessor) : base(dbContext)
        {
            QueryProcessor = queryProcessor;
        }
    }
}
