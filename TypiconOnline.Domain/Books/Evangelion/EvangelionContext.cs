using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books.Evangelion
{
    public class EvangelionContext : BookServiceBase, IEvangelionContext
    {
        public EvangelionContext(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
