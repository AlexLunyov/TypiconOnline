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
    class OutputFormConfiguration : IEntityTypeConfiguration<OutputForm>
    {
        public void Configure(EntityTypeBuilder<OutputForm> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Typicon)
                .WithMany()
                .HasForeignKey(c => c.TypiconId);

            builder.Ignore(c => c.DayWorships);
        }
    }
}
