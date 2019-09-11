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


            builder.OwnsOne(c => c.Name, name =>
            {
                name.OwnsMany(c => c.Items, items =>
                {
                    items.Property<int>("NameId");
                    items.HasForeignKey("NameId");
                    items.Property<int>("Id");
                    items.HasKey("Id");
                    items.ToTable("OutputDayNameItems");
                });
                //.ToTable("SignName");
            });

            builder.HasOne(c => c.PredefinedSign)
                .WithMany()
                .HasForeignKey(c => c.PredefinedSignId);

            builder.Ignore(c => c.DayWorshipLinks);
        }
    }
}
