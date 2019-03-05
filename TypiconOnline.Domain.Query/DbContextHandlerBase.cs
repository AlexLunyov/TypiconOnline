using JetBrains.Annotations;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query
{
    public abstract class DbContextHandlerBase
    {
        protected TypiconDBContext DbContext { get; }

        protected DbContextHandlerBase([NotNull] TypiconDBContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
