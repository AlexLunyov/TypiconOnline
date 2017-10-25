using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.Repository.EFSQlite.DataBase.Mapping
{
    class ModifiedYearConfiguration : IEntityTypeConfiguration<ModifiedYear>
    {
        public void Configure(EntityTypeBuilder<ModifiedYear> builder)
        {
            builder.Property(c => c.Year).IsRequired();

            builder.HasOne(c => c.TypiconEntity).
                WithMany(c => c.ModifiedYears);

            builder.HasMany(c => c.ModifiedRules);
        }
    }
}
