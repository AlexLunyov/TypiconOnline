using JetBrains.Annotations;
using System;
using System.Linq;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Typicon
{
    /// <summary>
    /// Возвращает Id и Name Устава
    /// </summary>
    public class TypiconEntityQueryHandler : DbContextQueryBase, IDataQueryHandler<TypiconEntityQuery, TypiconEntity>
    {
        public TypiconEntityQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public TypiconEntity Handle([NotNull] TypiconEntityQuery query)
        {
            return DbContext.Set<TypiconEntity>().FirstOrDefault(c => c.Id == query.TypiconId);
        }
    }
}
