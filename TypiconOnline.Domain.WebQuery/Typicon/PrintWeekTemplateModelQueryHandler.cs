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
    public class PrintWeekTemplateModelQueryHandler : QueryStrategyHandlerBase, IQueryHandler<PrintWeekTemplateModelQuery, Result<PrintWeekTemplateModel>>
    {
        public PrintWeekTemplateModelQueryHandler(TypiconDBContext dbContext, [NotNull] IQueryProcessor queryProcessor)
            : base(dbContext, queryProcessor)
        {
        }

        public Result<PrintWeekTemplateModel> Handle([NotNull] PrintWeekTemplateModelQuery query)
        {
            var draft = DbContext.Set<TypiconVersion>()
                            .Where(c => c.TypiconId == query.TypiconId && c.BDate == null && c.EDate == null)
                            .FirstOrDefault();

            if (draft == null)
            {
                return Result.Fail<PrintWeekTemplateModel>($"Черновик для Устава с Id={query.TypiconId} не был найден.");
            }

            var found = DbContext.Set<PrintWeekTemplate>()
                .FirstOrDefault(c => c.TypiconVersionId == draft.Id);

            return (found != null)
                ? Result.Ok(new PrintWeekTemplateModel()
                {
                    Id = found.Id,
                    TypiconVersionId = found.TypiconVersionId,
                    PrintFileName = found.PrintFileName,
                    DaysPerPage = found.DaysPerPage,
                    HasFile = found.PrintFile.Length > 0
                })
                : Result.Fail<PrintWeekTemplateModel>($"Печатный шаблон седмицы для Устава Id={query.TypiconId} не был найден.");
        }
    }
}
