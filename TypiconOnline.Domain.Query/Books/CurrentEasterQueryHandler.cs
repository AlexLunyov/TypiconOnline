using JetBrains.Annotations;
using System;
using System.Linq;
using TypiconOnline.Domain.Books.Easter;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Books
{
    public class CurrentEasterQueryHandler : DbContextQueryBase, IQueryHandler<CurrentEasterQuery, DateTime>
    {
        public CurrentEasterQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public DateTime Handle([NotNull] CurrentEasterQuery query)
        {
            EasterItem easter = DbContext.Set<EasterItem>().FirstOrDefault(c => c.Date.Year == query.Year);

            if (easter == null)
                throw new NullReferenceException("День празднования Пасхи не определен для года " + query.Year);

            return easter.Date;
        }
    }
}
