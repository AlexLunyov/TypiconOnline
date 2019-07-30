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

            builder.OwnsOne(c => c.NumberName, d =>
            {
                d.OwnsMany(c => c.Items, a =>
                {
                    a.Property<int>("NameId");
                    a.HasForeignKey("NameId");
                    a.Property<int>("Id");
                    a.HasKey("Id");
                    a.ToTable("KathismaNumberNameItems");
                });
                //.ToTable("KathismaNumberName");
            });

            builder.HasMany(c => c.SlavaElements)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.TypiconVersion).WithMany(d => d.Kathismas);

            //builder.Ignore(c => c.IsTemplate);
        }
    }
}
