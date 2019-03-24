using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.ErrorHandling;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.Query.Typicon
{
    public class TypiconPublishedVersionQueryHandler : DbContextQueryBase, IDataQueryHandler<TypiconPublishedVersionQuery, Result<TypiconVersion>>
    {
        public TypiconPublishedVersionQueryHandler([NotNull] TypiconDBContext dbContext) : base(dbContext)
        {
        }

        public Result<TypiconVersion> Handle(TypiconPublishedVersionQuery query)
        {
            var version = DbContext.Set<TypiconVersion>()
                .FirstOrDefault(c => c.TypiconId == query.TypiconId
                                    && c.BDate != null
                                    && c.EDate == null);

            return (version != null)
                ? Result.Ok(version)
                : Result.Fail<TypiconVersion>("Указанный Устав либо не существует, либо не существует его опубликованная версия.");
        }
    }
}
