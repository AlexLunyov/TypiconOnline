using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Books.OldTestament
{
    public class OldTestamentContext : BookServiceBase, IOldTestamentContext
    {
        public OldTestamentContext(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
