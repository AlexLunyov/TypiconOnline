using JetBrains.Annotations;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Books
{
    /// <summary>
    /// Возвращает День Октоиха по заданной дате
    /// </summary>
    public class PsalmQueryHandler : UnitOfWorkHandlerBase, IDataQueryHandler<PsalmQuery, Psalm>
    {
        public PsalmQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public Psalm Handle([NotNull] PsalmQuery query)
        {
            return UnitOfWork.Repository<Psalm>().Get(c => c.Number == query.Number);
        }
    }
}
