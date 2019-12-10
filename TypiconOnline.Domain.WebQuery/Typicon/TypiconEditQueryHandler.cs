using JetBrains.Annotations;
using Mapster;
using System;
using System.Linq;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Query.Typicon;
using TypiconOnline.Domain.Typicon;
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
    public class TypiconEditQueryHandler : QueryStrategyHandlerBase, IQueryHandler<TypiconEditQuery, Result<TypiconEntityEditModel>>
    {
        public TypiconEditQueryHandler(TypiconDBContext dbContext, [NotNull] IQueryProcessor queryProcessor)
            : base(dbContext, queryProcessor)
        {
        }

        public Result<TypiconEntityEditModel> Handle([NotNull] TypiconEditQuery query)
        {
            var typicon = DbContext.Set<TypiconEntity>().FirstOrDefault(c => c.Id == query.Id);

            var draft = DbContext.Set<TypiconVersion>().FirstOrDefault(c => c.TypiconId == query.Id && c.BDate == null && c.EDate == null);

            if (typicon != null && draft != null)
            {
                return Result.Ok(new TypiconEntityEditModel()
                {
                    Id = typicon.Id,
                    Name = typicon.Name,
                    DefaultLanguage = typicon.DefaultLanguage,
                    IsModified =  draft.IsModified,
                    IsTemplate = draft.IsTemplate,
                    HasVariables = draft.TypiconVariables.Any(),
                    HasEmptyPrintTemplates = draft.PrintDayTemplates
                                                    .Any(c => c.PrintFile == null || c.PrintFile.Length == 0),
                    Editors = typicon.Editors.Select(c => (c.Id, c.FullName))
                });
            }
            else
            {
                return Result.Fail<TypiconEntityEditModel>($"Устав с Id={query.Id} не найден.");
            }
        }
    }
}
