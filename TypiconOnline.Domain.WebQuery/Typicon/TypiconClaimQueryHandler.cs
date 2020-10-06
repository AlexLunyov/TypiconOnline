using JetBrains.Annotations;
using Mapster;
using System;
using System.Linq;
using TypiconOnline.Domain.Common;
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
    public class TypiconClaimQueryHandler : QueryStrategyHandlerBase, IQueryHandler<TypiconClaimQuery, Result<TypiconClaimModel>>
    {
        const string DefaultLanguage = "cs-ru";

        public TypiconClaimQueryHandler(TypiconDBContext dbContext, [NotNull] IQueryProcessor queryProcessor)
            : base(dbContext, queryProcessor)
        {
        }

        public Result<TypiconClaimModel> Handle([NotNull] TypiconClaimQuery query)
        {
            var found = DbContext.Set<TypiconClaim>().FirstOrDefault(c => c.Id == query.Id);

            if (found == null)
            {
                return Result.Fail<TypiconClaimModel>($"Заявка на создание Устава с Id={query.Id} не найдена.");
            }

            var pubVersion = DbContext.Set<TypiconVersion>()
                .FirstOrDefault(c => c.TypiconId == found.TemplateId
                    && c.BDate != null && c.EDate == null);

            if (pubVersion == null)
            {
                return Result.Fail<TypiconClaimModel>($"Отсутствует опубликованная версия Устава с Id={found.TemplateId}.");
            }

            return Result.Ok(new TypiconClaimModel()
            {
                Id = found.Id,
                Name = found.Name?.FirstOrDefault(CommonConstants.DefaultLanguage)?.Text,
                Description = found.Description?.FirstOrDefault(CommonConstants.DefaultLanguage)?.Text,
                SystemName = found.SystemName,
                DefaultLanguage = found.DefaultLanguage,
                Template = pubVersion.Name.FirstOrDefault(DefaultLanguage)?.Text
            });
        }
    }
}
