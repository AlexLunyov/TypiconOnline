using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class DayWorshipConfiguration : IEntityTypeConfiguration<DayWorship>
    {
        public void Configure(EntityTypeBuilder<DayWorship> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property<int>("WorshipNameId");
            builder.HasOne(e => e.WorshipName)
                .WithMany()
                .HasForeignKey("WorshipNameId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property<int?>("WorshipShortNameId");
            builder.HasOne(e => e.WorshipShortName)
                .WithMany()
                .HasForeignKey("WorshipShortNameId")
                //.IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Parent).
                WithMany();
        }
    }
}
