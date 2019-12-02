using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Typicon
{
    /// <summary>
    /// Возвращает Печатный шаблон седмицы из опубликованной версии Устава
    /// </summary>
    public class PrintWeekTemplateQueryHandler : DbContextQueryBase, IQueryHandler<PrintWeekTemplateQuery, PrintWeekTemplate>
    {
        public PrintWeekTemplateQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public PrintWeekTemplate Handle([NotNull] PrintWeekTemplateQuery query)
        {
            //возвращаем из опубликованной версии
            return DbContext.Set<PrintWeekTemplate>()
                .FirstOrDefault(c => c.TypiconVersion.TypiconId == query.TypiconId 
                    && c.TypiconVersion.BDate != null && c.TypiconVersion.EDate == null);
        }
    }
}
