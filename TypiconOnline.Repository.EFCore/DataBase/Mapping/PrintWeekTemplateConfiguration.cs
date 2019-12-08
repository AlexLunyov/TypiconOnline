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
    class PrintWeekTemplateConfiguration : IEntityTypeConfiguration<PrintWeekTemplate>
    {
        public void Configure(EntityTypeBuilder<PrintWeekTemplate> builder)
        {
            //builder.HasBaseType((Type)null);

            //builder.HasKey(c => c.Id);
        }
    }
}
