using System;
using System.Linq;
using JetBrains.Annotations;
using TypiconOnline.Domain.Books.Psalter;
using TypiconOnline.Domain.Interfaces;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Books
{
    /// <summary>
    /// Возвращает Псалм
    /// </summary>
    public class PsalmQueryHandler : DbContextHandlerBase, IDataQueryHandler<PsalmQuery, PsalmDto>
    {
        readonly ITypiconSerializer serializer;

        public PsalmQueryHandler(TypiconDBContext dbContext, ITypiconSerializer serializer) : base(dbContext)
        {
            this.serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
;        }

        public PsalmDto Handle([NotNull] PsalmQuery query)
        {
            var psalm = DbContext.Set<Psalm>().FirstOrDefault(c => c.Number == query.Number);

            PsalmDto result = null;

            if (psalm != null)
            {
                result = new PsalmDto()
                {
                    Number = psalm.Number,
                    Reading = psalm.GetElement(serializer)
                };
            }

            return result;
        }
    }
}
