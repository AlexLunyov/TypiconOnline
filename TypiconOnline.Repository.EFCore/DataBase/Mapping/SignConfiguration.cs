using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class SignConfiguration : IEntityTypeConfiguration<Sign>
    {
        public void Configure(EntityTypeBuilder<Sign> builder)
        {
            builder.HasBaseType((Type)null);

            builder.HasKey(c => c.Id);

            //builder.Property(c => c.Number).IsRequired();

            builder.Property(c => c.Priority).IsRequired();

            

            builder.OwnsOne(c => c.SignName, k => k.Ignore(d => d.Items));

            builder.HasOne(e => e.Template).
                WithMany()
                .OnDelete(DeleteBehavior.SetNull)
                .HasForeignKey(c => c.TemplateId)
                .IsRequired(false)
                ;

            //builder.Ignore(c => c.IsTemplate);
        }
    }
}
