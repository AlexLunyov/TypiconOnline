using JetBrains.Annotations;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Command
{
    public abstract class DbContextCommandBase
    {
        protected TypiconDBContext DbContext { get; }

        protected DbContextCommandBase(TypiconDBContext dbContext)
        {
            DbContext = dbContext ?? throw new System.ArgumentNullException(nameof(dbContext));
        }
    }
}
