using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Psalter;

namespace TypiconOnline.Repository.EFSQlite.DataBase.Mapping
{
    class SlavaElementConfiguration : IEntityTypeConfiguration<SlavaElement>
    {
        public void Configure(EntityTypeBuilder<SlavaElement> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.PsalmLinks);
        }
    }
}
