using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books.Apostol
{
    public class ApostolContext : BookServiceBase, IApostolContext
    {
        public ApostolContext(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
