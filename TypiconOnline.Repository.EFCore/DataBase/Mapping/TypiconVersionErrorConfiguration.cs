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
    class TypiconVersionErrorConfiguration : IEntityTypeConfiguration<TypiconVersionError>
    {
        public void Configure(EntityTypeBuilder<TypiconVersionError> builder)
        {
            builder.HasOne(e => e.TypiconVersion)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(c => c.TypiconVersionId)
                .IsRequired(true); 

            //builder.Property(e => e.ConstraintDescription);
            //builder.Ignore(e => e.ConstraintPath);
        }
    }
}
