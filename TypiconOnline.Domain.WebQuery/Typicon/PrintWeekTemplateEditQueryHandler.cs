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
    /// 
    /// </summary>
    public class PrintWeekTemplateEditQueryHandler : DbContextQueryBase, IQueryHandler<PrintWeekTemplateEditQuery, Result<PrintWeekTemplateEditModel>>
    {
        public PrintWeekTemplateEditQueryHandler(TypiconDBContext dbContext)
            : base(dbContext)
        {
        }

        public Result<PrintWeekTemplateEditModel> Handle([NotNull] PrintWeekTemplateEditQuery query)
        {
            var found = DbContext.Set<PrintWeekTemplate>().FirstOrDefault(c => c.Id == query.Id);

            if (found != null)
            {
                return Result.Ok(new PrintWeekTemplateEditModel()
                {
                    Id = found.Id,
                    DaysPerPage = found.DaysPerPage,
                    OldFileName = found.PrintFileName
                });
            }
            else
            {
                return Result.Fail<PrintWeekTemplateEditModel>($"Печатный шаблон седмицы с Id={query.Id} не найден.");
            }
        }
    }
}
