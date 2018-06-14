using JetBrains.Annotations;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query
{
    public abstract class UnitOfWorkHandlerBase
    {
        protected IUnitOfWork UnitOfWork { get; }

        public UnitOfWorkHandlerBase([NotNull] IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
