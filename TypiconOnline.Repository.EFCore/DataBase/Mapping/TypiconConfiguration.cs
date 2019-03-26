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
    class TypiconConfiguration : IEntityTypeConfiguration<TypiconEntity>
    {
        public void Configure(EntityTypeBuilder<TypiconEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(e => e.Template)
                .WithMany()
                .HasForeignKey(c => c.TemplateId)
                .IsRequired(false);

            builder.HasOne(e => e.Owner)
                .WithMany(c => c.OwnedTypicons)
                .IsRequired(true)
                .HasForeignKey(c => c.OwnerId);

            builder.HasMany(e => e.Versions)
                .WithOne(m => m.Typicon)
                .HasForeignKey(c => c.TypiconId);
        }
    }
}
