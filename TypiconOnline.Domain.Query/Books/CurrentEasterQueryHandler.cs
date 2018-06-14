using JetBrains.Annotations;
using System;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Books
{
    public class CurrentEasterQueryHandler : UnitOfWorkHandlerBase, IDataQueryHandler<CurrentEasterQuery, DateTime>
    {
        public CurrentEasterQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public DateTime Handle([NotNull] CurrentEasterQuery query)
        {
            EasterItem easter = UnitOfWork.Repository<EasterItem>().Get(c => c.Date.Year == query.Year);

            if (easter == null)
                throw new NullReferenceException("День празднования Пасхи не определен для года " + query.Year);

            return easter.Date;
        }
    }
}
