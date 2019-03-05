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
    public class AllTypiconsQueryHandler : DbContextHandlerBase, IDataQueryHandler<AllTypiconsQuery, IEnumerable<TypiconEntityDTO>>
    {
        public AllTypiconsQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public IEnumerable<TypiconEntityDTO> Handle([NotNull] AllTypiconsQuery query)
        {
            return DbContext.Set<TypiconEntity>().ProjectToType<TypiconEntityDTO>().AsEnumerable();
        }
    }
}
