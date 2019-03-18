using JetBrains.Annotations;
using Mapster;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class AllTypiconsQueryHandler : DbContextQueryBase, IDataQueryHandler<AllTypiconsQuery, IEnumerable<TypiconVersionDTO>>
    {
        public AllTypiconsQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public IEnumerable<TypiconVersionDTO> Handle([NotNull] AllTypiconsQuery query)
        {
            return DbContext.Set<TypiconVersion>().ProjectToType<TypiconVersionDTO>().AsEnumerable();
        }
    }
}
