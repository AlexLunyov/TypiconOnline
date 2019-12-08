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
    /// 
    /// </summary>
    public class TypiconEntityByPrintWeekTemplateQueryHandler : TypiconEntityByChildQueryHandlerBase<PrintWeekTemplate>, IQueryHandler<TypiconEntityByPrintWeekTemplateQuery, Result<TypiconEntity>>
    {
        public TypiconEntityByPrintWeekTemplateQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public Result<TypiconEntity> Handle([NotNull] TypiconEntityByPrintWeekTemplateQuery query)
        {
            return base.Handle(query);
        }
    }
}
