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
    public class TypiconEntityByCommonRuleQueryHandler : TypiconEntityByChildQueryHandlerBase<CommonRule>, IQueryHandler<TypiconEntityByCommonRuleQuery, Result<TypiconEntity>>
    {
        public TypiconEntityByCommonRuleQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public Result<TypiconEntity> Handle([NotNull] TypiconEntityByCommonRuleQuery query)
        {
            return base.Handle(query);
        }
    }
}
