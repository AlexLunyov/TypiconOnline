using JetBrains.Annotations;
using System;
using System.Linq;
using TypiconOnline.Domain.ItemTypes;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Typicon
{
    /// <summary>
    /// Возвращает Id и Name Устава
    /// </summary>
    public class TypiconEntityByPrintDayTemplateQueryHandler : TypiconEntityByChildQueryHandlerBase<PrintDayTemplate>, IQueryHandler<TypiconEntityByPrintDayTemplateQuery, Result<TypiconEntity>>
    {
        public TypiconEntityByPrintDayTemplateQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public Result<TypiconEntity> Handle([NotNull] TypiconEntityByPrintDayTemplateQuery query)
        {
            return base.Handle(query);
        }
    }
}
