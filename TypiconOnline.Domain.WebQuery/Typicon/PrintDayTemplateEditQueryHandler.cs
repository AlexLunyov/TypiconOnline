using JetBrains.Annotations;
using Mapster;
using System;
using System.Linq;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Print;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Infrastructure.Common.UnitOfWork;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    /// <summary>
    /// Возвращает Id и Name Устава
    /// </summary>
    public class PrintDayTemplateEditQueryHandler : DbContextQueryBase, IQueryHandler<PrintDayTemplateEditQuery, Result<PrintDayTemplateEditModel>>
    {
        public PrintDayTemplateEditQueryHandler(TypiconDBContext dbContext)
            : base(dbContext)
        {
        }

        public Result<PrintDayTemplateEditModel> Handle([NotNull] PrintDayTemplateEditQuery query)
        {
            var found = DbContext.Set<PrintDayTemplate>().FirstOrDefault(c => c.Id == query.Id);

            if (found != null)
            {
                return Result.Ok(new PrintDayTemplateEditModel()
                {
                    Id = found.Id,
                    Number = found.Number,
                    OldFileName = found.PrintFileName,
                    Name = found.Name,
                    Icon = found.Icon,
                    IsRed = found.IsRed
                });
            }
            else
            {
                return Result.Fail<PrintDayTemplateEditModel>($"Печатный шаблон дня с Id={query.Id} не найден.");
            }
        }
    }
}
