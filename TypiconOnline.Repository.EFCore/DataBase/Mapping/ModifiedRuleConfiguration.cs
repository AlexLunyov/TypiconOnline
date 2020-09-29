using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon.Modifications;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class ModifiedRuleConfiguration : IEntityTypeConfiguration<ModifiedRule>
    {
        public void Configure(EntityTypeBuilder<ModifiedRule> builder)
        {
            builder.HasKey(c => c.Id);

            builder.OwnsOne(c => c.ShortName, d =>
            {
                d.OwnsMany(c => c.Items, a =>
                {
                    a.Property<int>("NameId");
                    a.WithOwner().HasForeignKey("NameId");
                    //a.Property<int>("Id");
                    a.HasKey("NameId", "Language");
                    a.ToTable("ModifiedRuleShortNameItems");
                })
                .ToTable("ModifiedRuleShortName");
            });

            builder.HasOne(c => c.DayRule).WithMany();//.OnDelete(DeleteBehavior.Cascade);
            builder.Ignore(c => c.DayWorships);
        }
    }
}
