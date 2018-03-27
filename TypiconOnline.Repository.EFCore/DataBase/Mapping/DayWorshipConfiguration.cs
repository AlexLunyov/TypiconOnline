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
            //builder.HasOne(c => c.WorshipName)
            //    .WithMany()
            //    .HasForeignKey("WorshipNameId");

            //builder.HasOne(c => c.WorshipShortName)
            //    .WithMany()
            //    .HasForeignKey("WorshipShortNameId");

            builder.HasKey(c => c.Id);

            builder.OwnsOne(c => c.WorshipName, k => k.Ignore(d => d.Style));
            builder.OwnsOne(c => c.WorshipShortName, k => k.Ignore(d => d.Style));

            builder.HasOne(e => e.Parent).
                WithMany();
            //HasRequired(e => e.Parent).
            //    WithMany();

            //builder.ToTable("DayServices");
            //ToTable("DayServices");
        }
    }
}
