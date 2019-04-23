using JetBrains.Annotations;
using System;
using System.Linq;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Typicon
{
    /// <summary>
    /// Возвращает Id и Name Устава
    /// </summary>
    public class TypiconEntityBySignQueryHandler : TypiconEntityByChildQueryHandlerBase<Sign>, IDataQueryHandler<TypiconEntityBySignQuery, Result<TypiconEntity>>
    {
        public TypiconEntityBySignQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public Result<TypiconEntity> Handle([NotNull] TypiconEntityBySignQuery query)
        {
            return base.Handle(query);
        }
    }
}
