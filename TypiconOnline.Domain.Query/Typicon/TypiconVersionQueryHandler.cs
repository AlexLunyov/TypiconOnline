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
    public class TypiconVersionQueryHandler : DbContextQueryBase, IDataQueryHandler<TypiconVersionQuery, TypiconVersion>
    {
        public TypiconVersionQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public TypiconVersion Handle([NotNull] TypiconVersionQuery query)
        {
            return DbContext.Set<TypiconVersion>().FirstOrDefault(c => c.Id == query.TypiconVersionId);
        }
    }
}
