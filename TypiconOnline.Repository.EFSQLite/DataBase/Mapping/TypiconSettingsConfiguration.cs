using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TypiconOnline.Repository.EFSQlite.DataBase.Mapping
{
    class TypiconSettingsConfiguration : IEntityTypeConfiguration<TypiconSettings>
    {
        public void Configure(EntityTypeBuilder<TypiconSettings> builder)
        {

            builder.HasOne(c => c.TemplateSunday).WithOne().HasForeignKey<Sign>("OwnerId");//.WithMany();

        }
    }
}
