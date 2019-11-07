using JetBrains.Annotations;
using System;
using System.Linq;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Variable;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Typicon
{
    /// <summary>
    /// Возвращает TypiconEntity
    /// </summary>
    public class TypiconEntityByVariableQueryHandler : TypiconEntityByChildQueryHandlerBase<TypiconVariable>, IQueryHandler<TypiconEntityByVariableQuery, Result<TypiconEntity>>
    {
        public TypiconEntityByVariableQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public Result<TypiconEntity> Handle([NotNull] TypiconEntityByVariableQuery query)
        {
            return base.Handle(query);
        }
    }
}
