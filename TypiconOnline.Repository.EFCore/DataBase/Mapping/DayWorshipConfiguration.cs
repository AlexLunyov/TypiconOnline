using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Days;
using TypiconOnline.Domain.ItemTypes;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class DayWorshipConfiguration : IEntityTypeConfiguration<DayWorship>
    {
        public void Configure(EntityTypeBuilder<DayWorship> builder)
        {
            builder.HasKey(c => c.Id);

            builder.OwnsOne(c => c.WorshipName, d =>
            {
                d.OwnsMany(c => c.Items, a =>
                {
                    a.Property<int>("NameId");
                    a.WithOwner().HasForeignKey("NameId");
                    a.Property<int>("Id");
                    a.HasKey("Id");
                    a.ToTable("DayWorshipNameItems");
                });
                //.ToTable("DayWorshipName");
            });

            builder.OwnsOne(c => c.WorshipShortName, d =>
            {
                d.OwnsMany(c => c.Items, a =>
                {
                    a.Property<int>("NameId");
                    a.WithOwner().HasForeignKey("NameId");
                    a.Property<int>("Id");
                    a.HasKey("Id");
                    a.ToTable("DayWorshipShortNameItems");
                })
                .ToTable("DayWorshipShortName");
            });

            builder.HasOne(e => e.Parent).
                WithMany();
        }
    }
}
