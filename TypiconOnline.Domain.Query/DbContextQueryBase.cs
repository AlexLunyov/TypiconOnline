using JetBrains.Annotations;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query
{
    public abstract class DbContextQueryBase
    {
        protected TypiconDBContext DbContext { get; }

        protected DbContextQueryBase([NotNull] TypiconDBContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
