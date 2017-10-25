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
            //builder. HasOptional(e => e.Template).
            //    WithMany();

            builder.HasMany(e => e.ModifiedYears).
                WithOne(m => m.TypiconEntity);
            //HasMany(e => e.ModifiedYears).
            //    WithRequired(m => m.TypiconEntity);

            builder.OwnsOne(e => e.Settings).
                Ignore(c => c.TemplateSunday);//.WithMany();

            builder.HasMany(c => c.CommonRules).WithOne(d => d.Owner);
            builder.HasMany(c => c.Signs).WithOne(d => d.Owner);
            builder.HasMany(c => c.MenologyRules).WithOne(d => d.Owner);
            builder.HasMany(c => c.TriodionRules).WithOne(d => d.Owner);

            //builder.HasOne(e => e.Settings).
            //    WithOne().IsRequired();

            //builder.ToTable("TypiconEntities");
            //ToTable("TypiconEntities");
        }
    }
}
