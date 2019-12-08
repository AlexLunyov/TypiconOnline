using JetBrains.Annotations;
using Mapster;
using System;
using System.Linq;
using TypiconOnline.AppServices.Messaging.Common;
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
    public class DownloadPrintDayTemplateQueryHandler : DbContextQueryBase, IQueryHandler<DownloadPrintDayTemplateQuery, Result<FileDownloadResponse>>
    {
        public DownloadPrintDayTemplateQueryHandler(TypiconDBContext dbContext)
            : base(dbContext)
        {
        }

        public Result<FileDownloadResponse> Handle([NotNull] DownloadPrintDayTemplateQuery query)
        {
            var found = DbContext.Set<PrintDayTemplate>().FirstOrDefault(c => c.Id == query.Id);

            if (found != null)
            {
                return Result.Ok(new FileDownloadResponse(
                    content: found.PrintFile,
                    fileDownloadName: found.PrintFileName,
                    contentType: "application/vnd.openxmlformats-officedocument.wordprocessingml.document"));
            }
            else
            {
                return Result.Fail<FileDownloadResponse>($"Печатный шаблон дня с Id={query.Id} не найден.");
            }
        }
    }
}
