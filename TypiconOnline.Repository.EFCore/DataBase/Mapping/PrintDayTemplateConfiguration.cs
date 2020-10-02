using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypiconOnline.Domain.Typicon;
using TypiconOnline.Domain.Typicon.Print;

namespace TypiconOnline.Repository.EFCore.DataBase.Mapping
{
    class PrintDayTemplateConfiguration : IEntityTypeConfiguration<PrintDayTemplate>
    {
        public void Configure(EntityTypeBuilder<PrintDayTemplate> builder)
        {
            //builder.HasBaseType((Type)null);

            //builder.HasKey(c => c.Id);

            builder.HasOne(c => c.TypiconVersion)
               .WithMany(d => d.PrintDayTemplates)
               .HasForeignKey(d => d.TypiconVersionId);

            builder.HasMany(c => c.SignLinks).
                WithOne(d => d.PrintTemplate)
                .HasForeignKey(d => d.PrintTemplateId);
        }
    }
}
