using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using TypiconOnline.Domain.Query;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Infrastructure.Common.Query;
using TypiconOnline.Repository.EFCore.DataBase;

namespace TypiconOnline.Domain.WebQuery.Typicon
{
    public class AllTypiconsQueryHandler : DbContextQueryBase, IDataQueryHandler<AllTypiconsQuery, IEnumerable<TypiconDTO>>
    {
        public AllTypiconsQueryHandler(TypiconDBContext dbContext) : base(dbContext) { }

        public IEnumerable<TypiconDTO> Handle([NotNull] AllTypiconsQuery query)
        {
            var versions = DbContext.Set<TypiconVersion>()
                .Where(c => c.IsPublished);

            var result = new List<TypiconDTO>();

            foreach (var vrs in versions)
            {
                var dto = new TypiconDTO()
                {
                    Id = vrs.TypiconId,
                    VersionId = vrs.Id,
                    Name = vrs.Name.FirstOrDefault(query.Language).Text
                };

                result.Add(dto);
            }

            return result.AsEnumerable();

            //TypeAdapterConfig<TypiconVersion, TypiconDTO>
            //        .NewConfig()
            //        .Map(dest => dest.Id, src => src.TypiconId)
            //        .Map(dest => dest.Name, src => src.Name.FirstOrDefault(query.Language).Text);
            //return versions.ProjectToType<TypiconDTO>().AsEnumerable();
        }
    }
}
