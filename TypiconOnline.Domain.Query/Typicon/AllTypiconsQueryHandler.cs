using JetBrains.Annotations;
using Mapster;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class AllTypiconsQueryHandler : UnitOfWorkHandlerBase, IDataQueryHandler<AllTypiconsQuery, IEnumerable<TypiconEntityDTO>>
    {
        public AllTypiconsQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public IEnumerable<TypiconEntityDTO> Handle([NotNull] AllTypiconsQuery query)
        {
            return UnitOfWork.Repository<TypiconEntity>().GetAll().ProjectToType<TypiconEntityDTO>().AsEnumerable();
        }
    }
}
