using JetBrains.Annotations;
using Mapster;
using System;
using System.Collections.Generic;
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
    /// Возвращает Версии Устава
    /// </summary>
    public class TypiconVersionsQueryHandler : DbContextQueryBase, IQueryHandler<TypiconVersionsQuery, Result<IEnumerable<TypiconVersionsModel>>>
    {
        public TypiconVersionsQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public Result<IEnumerable<TypiconVersionsModel>> Handle([NotNull] TypiconVersionsQuery query)
        {
            var versions = DbContext.Set<TypiconVersion>()
                .Where(c => c.TypiconId == query.Id)
                .OrderBy(c => c.VersionNumber);

            if (versions != null)
            {
                var result = new List<TypiconVersionsModel>();
                foreach (var v in versions)
                {
                    string name = "Черновик";
                    if (v.BDate != null && v.EDate == null)
                    {
                        name = "Опубликована";
                    }
                    else if (v.BDate != null && v.EDate != null)
                    {
                        name = "Архив";
                    }

                    result.Add(new TypiconVersionsModel()
                    {
                        Id = v.Id,
                        Name = $"{v.VersionNumber} ({name} - {v.CDate.ToShortDateString()})"
                    });
                }
                return Result.Ok(result.AsEnumerable());
            }
            else
            {
                return Result.Fail<IEnumerable<TypiconVersionsModel>>($"Версии для Устава с Id={query.Id} не были найдены.");
            }
        }
    }
}
