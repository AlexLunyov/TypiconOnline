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
    class OutputWorshipConfiguration : IEntityTypeConfiguration<OutputWorship>
    {
        public void Configure(EntityTypeBuilder<OutputWorship> builder)
        {
            builder.HasKey(c => c.Id);

            //builder.HasOne(c => c.OutputDay)
            //    .WithMany()
            //    .HasForeignKey(c => c.OutputDayId);

            builder.OwnsOne(c => c.Name, name =>
            {
                name.OwnsMany(c => c.Items, items =>
                {
                    items.Property<int>("NameId");
                    items.WithOwner().HasForeignKey("NameId");
                    items.Property<int>("Id");
                    items.HasKey("Id");
                    items.ToTable("OutputWorshipNameItems");
                });
            });

            builder.OwnsOne(c => c.AdditionalName, name =>
            {
                name.OwnsMany(c => c.Items, items =>
                {
                    items.Property<int>("AddNameId");
                    items.WithOwner().HasForeignKey("AddNameId");
                    items.Property<int>("Id");
                    items.HasKey("Id");
                    items.ToTable("OutputWorshipAddNameItems");
                })
                .ToTable("OutputWorshipAddName"); ;
            });
        }
    }
}
