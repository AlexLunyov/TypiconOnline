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
    class OutputDayHeaderConfiguration : IEntityTypeConfiguration<OutputDayHeader>
    {
        public void Configure(EntityTypeBuilder<OutputDayHeader> builder)
        {
            builder.Property<int>("Id");
            builder.HasKey("Id");

            builder.OwnsOne(c => c.Name, name =>
            {
                name.OwnsMany(c => c.Items, items =>
                {
                    items.Property<int>("NameId");
                    items.WithOwner().HasForeignKey("NameId");
                    items.Property<int>("Id");
                    items.HasKey("Id");
                    items.ToTable("OutputDayHeaderNameItems");
                });
                //.ToTable("SignName");
            });

            builder.HasOne(c => c.PredefinedSign)
                .WithMany()
                .HasForeignKey(c => c.PredefinedSignId);
        }
    }
}
