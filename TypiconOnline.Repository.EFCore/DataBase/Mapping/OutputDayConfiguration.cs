using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TypiconOnline.Domain.Typicon.Output;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class OutputDayConfiguration : IEntityTypeConfiguration<OutputDay>
    {
        public void Configure(EntityTypeBuilder<OutputDay> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Typicon)
                .WithMany()
                .HasForeignKey(c => c.TypiconId);

            builder.HasOne(c => c.Header)
                .WithOne()
                .HasPrincipalKey<OutputDay>(c => c.Id)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(c => c.DayWorshipLinks);
        }
    }
}
