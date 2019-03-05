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

            builder.Property<int>("WorshipNameId");
            builder.HasOne(e => e.WorshipName)
                .WithMany()
                .HasForeignKey("WorshipNameId")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property<int>("WorshipShortNameId");
            builder.HasOne(e => e.WorshipShortName)
                .WithMany()
                .HasForeignKey("WorshipShortNameId")
                .OnDelete(DeleteBehavior.Cascade);

            //builder.OwnsOne(c => c.WorshipName, k =>
            //{
            //    k.Ignore(d => d.Items);
            //    k.Ignore(d => d.IsBold);
            //    k.Ignore(d => d.IsRed);
            //    k.Ignore(d => d.IsItalic);
            //});
            //builder.OwnsOne(c => c.WorshipShortName, k =>
            //{
            //    k.Ignore(d => d.Items);
            //    k.Ignore(d => d.IsBold);
            //    k.Ignore(d => d.IsRed);
            //    k.Ignore(d => d.IsItalic);
            //});

            builder.HasOne(e => e.Parent).
                WithMany();
            //HasRequired(e => e.Parent).
            //    WithMany();

            //builder.ToTable("DayServices");
            //ToTable("DayServices");
        }
    }
}
