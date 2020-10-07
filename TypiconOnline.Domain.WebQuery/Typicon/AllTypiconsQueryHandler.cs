using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.WebQuery.Models;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllTypiconsQueryHandler : DbContextQueryBase, IQueryHandler<AllTypiconsQuery, IEnumerable<TypiconEntityModel>>
    {
        public AllTypiconsQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public IEnumerable<TypiconEntityModel> Handle([NotNull] AllTypiconsQuery query)
        {
            var versions = DbContext.Set<TypiconVersion>()
                .AsNoTracking()
                .Where(TypiconVersion.IsPublished);

            if (!query.WithTemplates)
            {
                versions = versions.Where(c => !c.IsTemplate);
            }

            return versions.Select(c => new TypiconEntityModel()
            {
                Id = c.TypiconId,
                Name = c.Name.FirstOrDefault(query.Language).Text,
                SystemName = c.Typicon.SystemName
            });
        }
    }
}
