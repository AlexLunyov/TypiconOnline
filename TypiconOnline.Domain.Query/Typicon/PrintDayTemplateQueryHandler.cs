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
    /// Возвращает Печатный шаблон дня из опубликованной версии Устава
    /// </summary>
    public class PrintDayTemplateQueryHandler : DbContextQueryBase, IQueryHandler<PrintDayTemplateQuery, PrintDayTemplate>
    {
        public PrintDayTemplateQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public PrintDayTemplate Handle([NotNull] PrintDayTemplateQuery query)
        {
            //возвращаем из опубликованной версии
            return DbContext.Set<PrintDayTemplate>()
                .FirstOrDefault(c => c.TypiconVersion.TypiconId == query.TypiconId 
                    && c.TypiconVersion.BDate != null && c.TypiconVersion.EDate == null
                    && c.Number == query.Number);
        }
    }
}
