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
                .Where(c => c.BDate != null && c.EDate == null)
                .Where(c => !c.IsTemplate)
                //.Where(c => c.IsPublished)
                //.AsEnumerable()
                .Select(c => new TypiconEntityModel()
                {
                    Id = c.TypiconId,
                    Name = c.Name.FirstOrDefault(query.Language).Text,
                    SystemName = c.Typicon.SystemName
                });

            //var result = new List<TypiconEntityModel>();

            //foreach (var vrs in versions)
            //{
            //    var dto = new TypiconEntityModel()
            //    {
            //        Id = vrs.TypiconId,
            //        Name = vrs.Typicon.Name.FirstOrDefault(query.Language).Text,
            //    };

            //    result.Add(dto);
            //}

            return versions.AsEnumerable();

            //TypeAdapterConfig<TypiconVersion, TypiconDTO>
            //        .NewConfig()
            //        .Map(dest => dest.Id, src => src.TypiconId)
            //        .Map(dest => dest.Name, src => src.Name.FirstOrDefault(query.Language).Text);
            //return versions.ProjectToType<TypiconDTO>().AsEnumerable();
        }
    }
}
