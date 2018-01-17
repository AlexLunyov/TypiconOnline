using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TypiconOnline.Repository.EFSQlite.DataBase.Mapping
{
    class TypiconEntityConfiguration : IEntityTypeConfiguration<TypiconEntity>
    {
        public void Configure(EntityTypeBuilder<TypiconEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).HasMaxLength(200);

            builder.HasOne(e => e.Template).
                WithMany().IsRequired(false);

            builder.HasMany(e => e.ModifiedYears).
                WithOne(m => m.TypiconEntity).HasForeignKey(c => c.TypiconEntityId);

            builder.HasMany(c => c.CommonRules).WithOne(d => d.Owner).HasForeignKey(c => c.OwnerId);
            builder.HasMany(c => c.Signs).WithOne(d => d.Owner).HasForeignKey(c => c.OwnerId);
            builder.HasMany(c => c.MenologyRules).WithOne(d => d.Owner).HasForeignKey(c => c.OwnerId);
            builder.HasMany(c => c.TriodionRules).WithOne(d => d.Owner).HasForeignKey(c => c.OwnerId);
            builder.HasMany(c => c.Kathismas).WithOne(d => d.TypiconEntity);

            builder.HasOne(e => e.Settings).
                WithOne().HasForeignKey<TypiconSettings>("TypiconEntity.Id");

            //builder.ToTable("TypiconEntities");
            //ToTable("TypiconEntities");
        }
    }
}
