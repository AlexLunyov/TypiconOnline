using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class TypiconClaimConfiguration : IEntityTypeConfiguration<TypiconClaim>
    {
        public void Configure(EntityTypeBuilder<TypiconClaim> builder)
        {
            builder.HasKey(c => c.Id);

            builder.OwnsOne(c => c.Name, d =>
            {
                d.OwnsMany(c => c.Items, a =>
                {
                    a.Property<int>("NameId");
                    a.WithOwner().HasForeignKey("NameId");
                    //a.Property<int>("Id");
                    a.HasKey("NameId", "Language");
                    a.ToTable("TypiconClaimNameItems");
                })
                .Ignore("Discriminator");
                //.ToTable("TypiconVersionName");
            });
            builder.OwnsOne(c => c.Description, d =>
            {
                d.OwnsMany(c => c.Items, a =>
                {
                    a.Property<int>("DescriptionId");
                    a.WithOwner().HasForeignKey("DescriptionId");
                    //a.Property<int>("Id");
                    a.HasKey("DescriptionId", "Language");
                    a.ToTable("TypiconClaimDescriptionItems");
                })
                .Ignore("Discriminator");
                //.ToTable("TypiconVersionName");
            });
        }
    }
}
