using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Psalter;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class KathismaConfiguration : IEntityTypeConfiguration<Kathisma>
    {
        public void Configure(EntityTypeBuilder<Kathisma> builder)
        {
            builder.HasKey(c => c.Id);

            builder.OwnsOne(c => c.NumberName);

            builder.HasMany(c => c.SlavaElements);

            builder.HasOne(c => c.TypiconEntity).WithMany(d => d.Kathismas);

            //builder.Ignore(c => c.IsTemplate);
        }
    }
}
